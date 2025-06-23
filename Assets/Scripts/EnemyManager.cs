using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyManager.cs
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public PlayerMovement player;
    public Bullets bulletPrefab;
    private float baseHP = 1500;
    private float baseSpeed = 7f;
    private int damageBoost = 1;
    private int spawnAfterKill = 2;
    public static int fireMultiplier = 1;
    private float spawnInterval = 2f; // alle 2 Sekunden
    private float spawnTimer = 0f;
    private int maxEnemies = 3;
    private int enemyCount = 0;
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        Debug.Log("Enemy Count" + enemyCount);
        Debug.Log("Enemy Count von Enemy" + Enemy.enemyCount);

        if (spawnTimer >= spawnInterval &&  enemyCount < maxEnemies)
        {
            InitializeLevel(10);
            enemyCount++;
            spawnTimer = 0f;
        }
        if (Enemy.enemyCount > maxEnemies && enemyCount >= maxEnemies)
        {
            LevelSuccess.Instance.setAct();
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
        if (enemyCount == maxEnemies - 1)
        {
            enemyGO.transform.localScale *= 1.5f;

            enemy.maxhp *= 2f;
            enemyGO.GetComponent<SpriteRenderer>().color = Color.red;
            fireMultiplier = 2;
            player.damageFromEnemy *= 5;

        }
    }
    public void InitializeLevel(int level, bool a)
    {
        enemyCount = 0;
        Enemy.enemyCount = 1;
        maxEnemies *= 2;
        fireMultiplier = 1;
        player.damageFromEnemy /= 5;
    }

}
