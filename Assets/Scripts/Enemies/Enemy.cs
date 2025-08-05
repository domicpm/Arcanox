using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float maxhp;
    public float hp;
    public Transform sprite;
    public HealthBar healthbar;
    public char tierTag;
    public Bullets bullet;
    public  bool isBoss = false;
    public bool isHit = false;
    public Vector2 enemydeathpos;
    public ItemDrops itemType;
    public ItemDrops item;
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
    public EnemyTier et;
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
    private Color originalColor;
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

            Transform sr = transform.Find("SpriteWraith") ?? transform.Find("SpriteGolem") ?? transform.Find("SpriteEnemy");
            if (sr != null)
                originalColor = sr.GetComponent<SpriteRenderer>().color;       
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
        if (et.isShiny) FlashShiny();
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
        if (collision.CompareTag("Bullet"))
        {
            if (SkillTree.isHeal)
            {
                int randomLifesteal = Random.Range(1, 101);
                if (randomLifesteal <= p.lifesteal && p.newhp < p.maxhp) p.newhp += 1;
                else if (randomLifesteal <= 5 && p.newhp < p.maxhp) p.newhp += 5;
            }
            var bulletComponent = collision.GetComponent<Bullets>();
            if (bulletComponent != null)
            {
                isHit = true;
                hp -= bulletComponent.getDmg();
                healthbar.setHealth(hp);
                hpEnemy.text = hp.ToString();

                Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
                Vector3 spawnPos = dmgtextSpawnLocation.position + offset;

                DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);
                dmgText.SetDamage(bulletComponent.getDmg());

                if (hp <= 0)
                {
                    deathHandler();
                }
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
            item.spawnItemsWithEffects(enemydeathpos);
            bulletSpawned = true;
        }
        else if (dropChance <= 10 && !CompareTag("S-Tier-Enemy"))
        {
            itemType.spawnItems(enemydeathpos, 1);
        }
        else if (dropChance <= 30 && !CompareTag("S-Tier-Enemy"))
        {
            itemType.spawnItems(enemydeathpos, 2);
        }
        else if(dropChance <= 10 && !CompareTag("S-Tier-Enemy"))
        {
        }
        if (CompareTag("S-Tier-Enemy"))
        {
            itemType.spawnItems(enemydeathpos, 3);
            p.experience += 50;   // if enemy S Tier, gain additional xp
        }
        destroyObj();

        if(isBoss) {
             bossDead = true;
             StartCoroutine(Delay());
                     }
        else if (EnemyManager.bossSpawned == false){
                    StartCoroutine(Delay());
        }
        if (CompareTag("Boss"))
        {
            p.experience += 30;
            bossDead = true;
        }
        else if(!CompareTag("S-Tier-Enemy"))
        {
            if(p.experience <= p.maxExperience)
            p.experience += 10;
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        LevelSuccess.Instance.setAct();
        //allCleared = true;
    }
    void FlashShiny()
    {
        Transform shinyChild = transform.Find("SpriteWraith") ?? transform.Find("SpriteGolem") ?? transform.Find("SpriteEnemy");
        SpriteRenderer sr = shinyChild.GetComponent<SpriteRenderer>();

        if (shinyChild == null)
        {
            Debug.LogWarning("Kein passendes Child gefunden!");      
        }
        sr.color = new Color(1f, 1f, 0.3f); // shiny
    }

}
