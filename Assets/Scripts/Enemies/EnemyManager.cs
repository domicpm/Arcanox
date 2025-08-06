using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyManager.cs
public class EnemyManager : MonoBehaviour
{
    public GameObject bossPrefab;

    public static EnemyManager Instance;
    public PlayerMovement player;
    public Bullets bulletPrefab;
    public PlayerHealthBar hpBar;

    public GameObject deathEyePrefab;
    public GameObject wraithPrefab;
    public GameObject golemPrefab;

    public Fireball deathEyeBulletPrefab;
    public Fireball wraithBulletPrefab;

    public EnemyTier et;

    public Rarity rarity;
    public float baseHP = 1500;
    public static bool bossSpawned = false;
    private float baseSpeed = 7f;
    private int damageBoost = 1;
    private int spawnAfterKill = 2;
    public static int fireMultiplier = 1;
    private float spawnInterval = 2f; // alle 2 Sekunden
    private float spawnTimer = 0f;
    public int maxEnemies = 5;
    private int enemyCount = 0;
    public int counter = 0;
    private int enemiesInScene = 3;
    public int level = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Singleton
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBoss(level);
        }
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && enemyCount < maxEnemies)
        {

            InitializeLevel(1); //spawnt Gegner
            enemyCount++;
            spawnTimer = 0f;
        }

        else if (bossSpawned == false && Enemy.killCount == maxEnemies && level % 2 != 0)
        {
            SpawnBoss(level);
        }
        if (Enemy.killCount > maxEnemies && enemyCount >= maxEnemies)
        {
            //LevelSuccess.Instance.setAct();
        }
    }
    public void InitializeLevel(int level)
    {
        int enemyType = Random.Range(1, 4);
        Vector3 spawnPos = new Vector3(Random.Range(-100f, -36f), Random.Range(-5f, 30f));
        int spawnType = Random.Range(1, 101);
        GameObject enemyGO;


        switch (enemyType)
        {
            case 1:
                enemyGO = Instantiate(deathEyePrefab, spawnPos, Quaternion.identity);
                enemyType = 1;
                break;

            case 2:
                enemyGO = Instantiate(wraithPrefab, spawnPos, Quaternion.identity);
                enemyType = 2;
                break;

            case 3:
                enemyGO = Instantiate(golemPrefab, spawnPos, Quaternion.identity);
                enemyType = 2;
                break;
            default:
                enemyGO = null;

                enemyType = 0;
                break;
        }
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        char tier = et.tierDecider();
        EnemyTierStats stats = et.enemyChanger(tier);
        Transform rarityTransform = enemyGO.transform.Find("RarityParticles");
        Rarity rarityComponent = rarityTransform.GetComponent<Rarity>();

        if (tier == 'S')
        {
            rarityComponent.setRarity();
            enemy.tag = "S-Tier-Enemy";
        } 
        enemy.maxhp = baseHP + (level * 100) + stats.hpBonus;
        enemy.speed = stats.speed;
        enemy.p = player;
        enemy.healthbar.setMaxHealth(enemy.hp);
        enemy.hpEnemy.text = enemy.hp.ToString();
        enemy.fb = enemyGO.GetComponentInChildren<Fireball>();
        enemy.fb.player = enemy.p.transform;
        enemy.fb.p = enemy.p;
        enemy.bullet = bulletPrefab;
        enemy.fb.gameObject.SetActive(true);
        enemy.fireballSizeMultiplier = stats.fireballSize;
        enemy.fb = setEnemyBullet(enemyType);
        enemy.tierTag = tier;

        spawnAfterKill += level;
        if (player.godmode == false)
        {
            player.damageFromEnemy = player.damageFromEnemy + level;
        }
    }


    public void InitializeLevel(int level, bool a)
    {
        enemyCount = 0;
        if (a == false)
        {
            Enemy.enemyCount = 0;
        }
        else
        {
            Enemy.enemyCount = 1;
        }
        maxEnemies += 4;
        if (player.godmode == false)
        {
            player.damageFromEnemy += 6;
        }
        bossSpawned = false;
        Enemy.allCleared = false;
        //Enemy.isBoss = false;
    }

    public Fireball setEnemyBullet(int enemyType)
    {
        if (enemyType == 1)
        {
            return deathEyeBulletPrefab;
        }
        else if (enemyType == 2)
        {
            return wraithBulletPrefab;
        }
        return null;
    }
    public void SpawnBoss(int level)
    {
        SpellAoE.scaleNext = false;
        SpellAoE.isScaled = false;
        Vector3 spawnPos = new Vector3(Random.Range(-75f, -50f), Random.Range(3f, 22f));
        GameObject bossGO = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        Enemy boss = bossGO.GetComponent<Enemy>();

        boss.maxhp = (baseHP + level * 400) * 4f;
        boss.hp = boss.maxhp;
        boss.p = player;
        boss.healthbar.setMaxHealth(boss.maxhp);
        boss.hpEnemy.text = boss.hp.ToString();
        boss.fb = bossGO.GetComponentInChildren<Fireball>();
        boss.fb.player = boss.p.transform;
        boss.fb.p = boss.p;
        boss.bullet = bulletPrefab;

        // BOSS SETUP
        boss.fireballSizeMultiplier = 2;
        boss.fireballInterval = 0.07f;
        boss.fireballSpeed = 40f;
        //Enemy.isBoss = true; 
        bossSpawned = true;
    }
}