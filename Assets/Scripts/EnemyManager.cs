using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyManager.cs
public class EnemyManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public GameObject enemyPrefab;
    public static EnemyManager Instance;
    public PlayerMovement player;
    public Bullets bulletPrefab;
    public PlayerHealthBar hpBar;

    private float baseHP = 1500;
    public bool bossSpawned = false;
    private float baseSpeed = 7f;
    private int damageBoost = 1;
    private int spawnAfterKill = 2;
    public static int fireMultiplier = 1;
    private float spawnInterval = 2f; // alle 2 Sekunden
    private float spawnTimer = 0f;
    private int maxEnemies = 2;
    private int enemyCount = 0;
    public int counter = 0;
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
        spawnTimer += Time.deltaTime;
      

        if (spawnTimer >= spawnInterval &&  enemyCount < maxEnemies)
        {
           InitializeLevel(1); //spawnt Gegner
            enemyCount++;
            spawnTimer = 0f;
        }
        if (Enemy.enemyCount > maxEnemies && enemyCount >= maxEnemies)
        {
            //LevelSuccess.Instance.setAct();
        }       
    }

    public void InitializeLevel(int level)
    {

        Vector3 spawnPos = new Vector3(Random.Range(-36f, 30f), Random.Range(-10f, 30f));
        GameObject enemyGO = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.maxhp = baseHP + (level * 100);
        enemy.speed = baseSpeed + (0.2f * level);
        player.damageFromEnemy = player.damageFromEnemy + level;
        spawnAfterKill += level;
        enemy.p = player;
        enemy.healthbar.setMaxHealth(enemy.maxhp);
        enemy.hpEnemy.text = enemy.maxhp.ToString();
        enemy.fb = enemyGO.GetComponentInChildren<fireball>();
        enemy.fb.player = enemy.p.transform;
        enemy.fb.p = enemy.p;
        enemy.bullet = bulletPrefab;
        if (enemyCount == maxEnemies - 1 && bossSpawned == false)
        {
            //enemyGO.transform.localScale *= 1.5f;

            //enemy.maxhp *= 2f;
            //enemyGO.transform.Find("SpriteEnemy").GetComponent<SpriteRenderer>().color = Color.red;
            //enemy.fireballSizeMultiplier = 2;
            //player.damageFromEnemy *= 5;
            //enemy.fireballInterval = 0.2f;
            //enemy.fireballSpeed = 12f;
            SpawnBoss(level);
        }
        else
        {
            enemy.fireballSizeMultiplier = 1;
            enemy.fireballInterval = 0.7f;
            enemy.fireballSpeed = 8f;
        }
    }
    public void InitializeLevel(int level, bool a)
    {
        enemyCount = 0;
        if(a == false) {        Enemy.enemyCount = 0;
        }
        else
        {
            Enemy.enemyCount = 1;
        }
        maxEnemies *= 2;
        player.damageFromEnemy += 4;
        bossSpawned = false;
        Enemy.allCleared = false;
        Enemy.isBoss = false;
    }
    public void SpawnBoss(int level)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-36f, 30f), Random.Range(-10f, 30f));
        GameObject bossGO = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        Enemy boss = bossGO.GetComponent<Enemy>();

        boss.maxhp = (baseHP + level * 100) * 2f;
        boss.speed = baseSpeed + (0.2f * level);
        boss.p = player;
        boss.healthbar.setMaxHealth(boss.maxhp);
        boss.hpEnemy.text = boss.maxhp.ToString();
        boss.fb = bossGO.GetComponentInChildren<fireball>();
        boss.fb.player = boss.p.transform;
        boss.fb.p = boss.p;
        boss.bullet = bulletPrefab;

        // BOSS SETUP
        boss.transform.localScale *= 1f;
        boss.fireballSizeMultiplier = 2;
        boss.fireballInterval = 0.2f;
        boss.fireballSpeed = 12f;
        Enemy.isBoss = true; 
        bossSpawned = true;
    }

}
