using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform player;
    public int ppos = 175;
    private float moveSpeed = 8f;
    private float angle;
    Vector3 direction;
    public GameObject Prefab;
    bool hasLaunched = false;
    float timer = 0f;
    public float interval = 0.5f;
    public float intervalMelee = 0.2f;
    public EnemyAttackSpawn enemy;
    private Vector3 originalScale;
    public PlayerMovement p;
    private bool isOriginal = true; // Flag to identify the original prefab
    public Animator animator;
    public AttackRangeCircle arc;
    public Enemy boss;
    public RotateEnemySprite res;
    public bool isGolem = false;
    public bool isAttacking = false;
    private void Start()
    {
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
        gameObject.SetActive(true);
        // Only the clones should move, not the original prefab
        if (!isOriginal)
        {
            CalculateInitialDirection();
            Launch();
        }
    }

    void CalculateInitialDirection()
    {
        // Calculate direction only once when created
        if (player != null)
        {
            direction = player.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (arc != null && !arc.getInRange())
        {
            isAttacking = false;
            res.setAttackAnimation(isAttacking);
        }

        if (p.getDead() == false && isGolem == false)
        {
            // Original prefab spawns new Fireballs
            if (isOriginal)
            {
                timer += Time.deltaTime;
                if (timer >= interval && p != null && !p.getDead() && isGolem == false)
                {
                    spawn();
                    timer = 0f;
                }
                else if (timer >= intervalMelee && p != null && !p.getDead() && isGolem == true)
                {
                    spawn();
                    timer = 0f;
                }
            }
            // Clones move in their set direction
            else if (hasLaunched)
            {
                // WICHTIGE ÄNDERUNG: "transform.right" statt "- transform.right"
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
          //  gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOriginal && (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player")))
        {
            Destroy(gameObject);
        }
    }

    public void spawn()
    {
        if (p != null && p.getDead() == false && enemy != null && arc.getInRange() == true && boss.isDead == false)
            
        {
            isAttacking = true;
            res.setCastAnimation();
            res.setAttackAnimation(isAttacking);
            GameObject newFireball = Instantiate(Prefab, enemy.gameObject.transform.position, Quaternion.identity);
            newFireball.transform.localScale *= boss.fireballSizeMultiplier;
            
            // Setze Referenzen für den geklonten Feuerball
            Fireball newFireballScript = newFireball.GetComponent<Fireball>();
            newFireballScript.interval = boss.fireballInterval;
            newFireballScript.moveSpeed = boss.fireballSpeed;
            if (newFireballScript != null)
            {
                newFireballScript.isOriginal = false; // Markiere als Klon
                newFireballScript.player = player;
                //newFireballScript.moveSpeed = moveSpeed;
                newFireballScript.p = p;

                // Berechne Richtung zum Spieler
                Vector3 fireballDirection = (player.position - newFireball.transform.position).normalized;

                // Setze die Rotation des Feuerballs so, dass er in die richtige Richtung fliegt
                //float fireballAngle = Mathf.Atan2(fireballDirection.y, fireballDirection.x) * Mathf.Rad2Deg;
                //newFireball.transform.rotation = Quaternion.Euler(0, 0, fireballAngle);

                newFireball.transform.localScale = new Vector3(Mathf.Abs(newFireball.transform.localScale.x), newFireball.transform.localScale.y, newFireball.transform.localScale.z);
               


                // Setze den Rigidbody für die Bewegung
                Rigidbody2D rb = newFireball.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = fireballDirection * moveSpeed; // Geschwindigkeit anwenden
                    rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                }
            }
        }
    }





    public void Launch()
    {
        hasLaunched = true;
    }
}