using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float maxhp;
    public HealthBar healthbar;
    public Bullets bullet;
    public static bool isBoss = false;
    public bool isHit = false;
    public Heal heal;
    public Vector2 enemydeathpos;
    public AttackBoost ab;
    public PlayerMovement p;
    public Vector3 randomPosition;
    public bool isSpawned = false;
    public Text hpEnemy;
    public Transform dmgtextSpawnLocation;
    private int maxEnemies = 3;
    public int spawnAfterKill = 1;
    public static int enemyCount = 0;
    public AttackRangeCircle atc;
    public float speed = 7f;
    public DamageText damageTextPrefab;
    public GameObject enemyprefab;
    public Fireball fb;
    private bool isDead = false;
    public RotateEnemySprite res;
    public int fireballSizeMultiplier = 1;
    public float fireballInterval = 0.7f;
    public float fireballSpeed = 8f;
    public static bool allCleared = false;
    public static bool isDummy = false;
    private void Start()
    {
        healthbar.setMaxHealth(maxhp);
        hpEnemy.text = maxhp.ToString();
        atc.inRange = false;
        
          
        

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
        fb.gameObject.SetActive(false);
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
            Vector2 dir = (p.transform.position - transform.position).normalized;

            transform.position += (Vector3)(dir * speed * Time.deltaTime);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;  // keine Collision , wenn tot
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            maxhp -= bullet.getDmg();
            healthbar.setHealth(maxhp);
            hpEnemy.text = maxhp.ToString();

            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
            Vector3 spawnPos = dmgtextSpawnLocation.transform.position + offset; 

             DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);
           
            dmgText.SetDamage(bullet.getDmg());


            if (maxhp <= 0)
            {
                deathHandler();
            }
        }
        if (collision.gameObject.CompareTag("Spell"))
        {
            isHit = true;
            maxhp -= bullet.getDmgSpell();
            healthbar.setHealth(maxhp);
            hpEnemy.text = maxhp.ToString();

            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
            Vector3 spawnPos = dmgtextSpawnLocation.transform.position + offset;

            DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);

            dmgText.SetDamage(bullet.getDmgSpell());


            if (maxhp <= 0)
            {
                deathHandler();
            }
        }
    }
    void deathHandler()
    {
        enemydeathpos = transform.position;
        int random = Random.Range(1, 101);
        if (random <= 30) heal.spawn(enemydeathpos);
        else if(random <= 50) ab.spawn(enemydeathpos);
        else if(random <= 10)
        {
            Debug.Log("lucky LOOT");
        }
        destroyObj();

        if(isBoss) {
            {
                StartCoroutine(Delay());
            }
        }

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        LevelSuccess.Instance.setAct();
        //allCleared = true;
    }

}
