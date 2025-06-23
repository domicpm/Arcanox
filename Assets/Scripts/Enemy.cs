using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxhp;
    public HealthBar healthbar;
    public Bullets bullet;
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
    public fireball fb;
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
        Destroy(gameObject);     
    }

    public Vector3 getCurrentEnemyPos()
    {
        return randomPosition;
    }

    void Update()
    {

        if (atc.inRange == false)
        {
            Vector2 dir = (p.transform.position - transform.position).normalized;

            transform.position += (Vector3)(dir * speed * Time.deltaTime);
        }
    }


    public void spawn()
    {
        maxhp = 1500;
        randomPosition = new Vector3(Random.Range(-36f, 30f), Random.Range(-10f, 30f));

        GameObject enemyObj = Instantiate(enemyprefab, randomPosition, Quaternion.identity);
        Enemy enemyScript = enemyObj.GetComponent<Enemy>();
        enemyScript.p = p;
        enemyScript.bullet = this.bullet;
        enemyScript.fb = enemyObj.GetComponentInChildren<fireball>();

        // Fireball-Ziel setzen
        if (enemyScript.fb != null)
        {
            enemyScript.fb.player = p.transform;
            enemyScript.fb.p = p;
        }
        else
        {
            Debug.LogWarning("fireball-Komponente im EnemyPrefab nicht gefunden!");
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
                enemydeathpos = transform.position;
                int random = Random.Range(1, 101);
                if (random <= 50) heal.spawn(enemydeathpos);
                else ab.spawn(enemydeathpos);

                for (int i = 0; i < spawnAfterKill; i++)
                {

                    if (enemyCount <= maxEnemies)
                    {
                        //spawn();
                    }
                }
                destroyObj();
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
                enemydeathpos = transform.position;
                int random = Random.Range(1, 101);
                if (random <= 50) heal.spawn(enemydeathpos);
                else ab.spawn(enemydeathpos);
                for (int i = 0; i < spawnAfterKill; i++)
                {
                    if (enemyCount <= maxEnemies)
                    {
                        //spawn();
                    }
                }
                destroyObj();
            }
        }
    }

}
