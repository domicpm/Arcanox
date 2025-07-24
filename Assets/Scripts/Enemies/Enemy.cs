using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float maxhp;
    public float hp;
    public Transform sprite;
    public HealthBar healthbar;
    public Bullets bullet;
    public  bool isBoss = false;
    public bool isHit = false;
    public Heal heal;
    public Vector2 enemydeathpos;
    public AttackBoost ab;
    public Items item;
    public PlayerMovement p;
    public Vector3 randomPosition;
    public bool isSpawned = false;
    public static bool bossDead = false;
    public Text hpEnemy;
    public Transform dmgtextSpawnLocation;
    public int spawnAfterKill = 1;
    public static int enemyCount = 0;
    public AttackRangeCircle atc;
    public float speed = 7f;
    public DamageText damageTextPrefab;
    public GameObject enemyprefab;
    public Fireball fb;
    public bool isDead = false;
    public RotateEnemySprite res;
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public int fireballSizeMultiplier = 1;
    [HideInInspector]public float fireballInterval = 0.7f;
    [HideInInspector] public float fireballSpeed = 8f;
    public static bool allCleared = false;
    public static bool isDummy = false;
    private Vector3 baseScale;
    private static bool bulletSpawned = false;
    public static int killCount = 0;
    private Vector3 personalOffset;
    public bool isGolem = false;
    private void Start()
    {
        healthbar.setMaxHealth(maxhp);
        hpEnemy.text = maxhp.ToString();
        hp = maxhp;
        atc.inRange = false;
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        baseScale = sprite.localScale; // Ausgangsskalierung speichern

        personalOffset = new Vector3(Random.Range(-3f, 3f), Random.Range(-2f, 2f), 0);



    }
    public void Awake()
    {
        //Instance = this;
    }
    public void destroyObj()
    {
        enemyCount++;
        this.isDead = true;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
        healthbar.gameObject.SetActive(false);
        hpEnemy.gameObject.SetActive(false);
        //fb.gameObject.SetActive(false);
        res.setDeadAnimation();
        StartCoroutine(DestroyAfterDelay(2f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


    public Vector3 getCurrentEnemyPos()
    {
        return randomPosition;
    }

    void Update()
    {
        if (isDead || LevelSuccess.isInLootRoom == true || isDummy == true) return;  // kein Movement, wenn tot oder im Loot Raum oder wenn Dummy aktiv
        if (atc.inRange == false)
        {
            res.setWalkingAnimation(true);
            //Vector2 dir = (p.transform.position - transform.position).normalized;
            //transform.position += (Vector3)(dir * speed * Time.deltaTime);
            Vector3 targetPos = p.transform.position + personalOffset;
            Vector2 dir = (targetPos - transform.position).normalized;
            transform.position += (Vector3)(dir * speed * Time.deltaTime);

        }
        else
        {
            res.setWalkingAnimation(false);
        }
        FlipSprite();

        //if(gameObject.transform.position.x > p.transform.position.x)
        //{
        //    sprite.localScale = new Vector3(-1, 1, 1); // Rechts

        //}else
        //{
        //    sprite.localScale = new Vector3(1, 1, 1); // Rechts

        //}
    }
    // Annahme: 'sprite' ist der Transform deines Sprite-GameObjects
    // und 'p' ist das Ziel (z.B. Spieler)

    void FlipSprite()
    {
        Vector3 scale = baseScale;
        if (transform.position.x > p.transform.position.x)
            scale.x = -Mathf.Abs(scale.x);  // nach links flippen
        else
            scale.x = Mathf.Abs(scale.x);   // nach rechts flippen

        sprite.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;  // keine Collision , wenn tot
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            hp -= bullet.getDmg();
            healthbar.setHealth(hp);
            hpEnemy.text = hp.ToString();

            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
            Vector3 spawnPos = dmgtextSpawnLocation.transform.position + offset; 

             DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);
           
            dmgText.SetDamage(bullet.getDmg());


            if (hp <= 0)
            {
                deathHandler();
            }
        }
        if (collision.gameObject.CompareTag("Spell"))
        {
            isHit = true;
            hp -= bullet.getDmgSpell();
            healthbar.setHealth(hp);
            hpEnemy.text = hp.ToString();

            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
            Vector3 spawnPos = dmgtextSpawnLocation.transform.position + offset;

            DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);

            dmgText.SetDamage(bullet.getDmgSpell());


            if (hp <= 0)
            {
                deathHandler();
            }
        }
    }
    void deathHandler()
    {
        enemydeathpos = transform.position;
        killCount++;
        int dropChance = Random.Range(1, 101);
        if (dropChance <= 100 && !bulletSpawned && isBoss)
        {
            item.spawn(enemydeathpos);
            bulletSpawned = true;
        }
        else if (dropChance <= 10)
        {
            ab.spawn(enemydeathpos);
        }
        else if (dropChance <= 30)
        {
            heal.spawn(enemydeathpos);
        }
        else
        {

        }
        destroyObj();

        if(isBoss) {
             bossDead = true;
             StartCoroutine(Delay());
                     }
        else if (EnemyManager.bossSpawned == false){
                    StartCoroutine(Delay());
        }
        else
        {
            Debug.Log("fail");
        }
        if (CompareTag("Boss"))
        {
            bossDead = true;
        }

        
           

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        LevelSuccess.Instance.setAct();
        //allCleared = true;
    }

}
