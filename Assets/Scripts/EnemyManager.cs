using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyManager.cs
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public PlayerMovement player;
    public Bullets bulletPrefab;
    private int maxEnemies = 5;
    private float baseHP = 1000;
    private float baseSpeed = 7f;
    private int damageBoost = 2;
    public void InitializeLevel(int level)
    {
        int enemyCount = level + 4;

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-36f, 30f), Random.Range(-10f, 30f));
            GameObject enemyGO = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemy.maxhp = baseHP + (level - 1) * 700;
            enemy.speed = baseSpeed + (level - 1); 
            enemy.p = player;
            enemy.healthbar.setMaxHealth(enemy.maxhp);
            enemy.hpEnemy.text = enemy.maxhp.ToString();
            enemy.fb = enemyGO.GetComponentInChildren<fireball>();
            enemy.fb.player = enemy.p.transform;
            enemy.fb.p = enemy.p;
            enemy.bullet = bulletPrefab;
            player.damageFromEnemy = player.damageFromEnemy + damageBoost;

        }
        LevelSuccess.Instance.setAct();
    }
}


