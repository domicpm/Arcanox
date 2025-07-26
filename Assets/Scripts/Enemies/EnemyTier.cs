using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTier : MonoBehaviour
{
    public EnemyManager em;
    public GameObject deathEyePrefab;

    public PlayerMovement player;
    public Bullets bulletPrefab;
    public Fireball deathEyeBulletPrefab;
    public Fireball wraithBulletPrefab;
    private int fbsize = 2;
    public float tierHpBonus = 0;
    public float tierScaleBonus = 1f;
    bool isScaled = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    public void golemType(int type)
    {
        switch (type)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            default:
                break;
        }

    }
    public char tierDecider(int type)
    {
        char tier;
       if(type <= 3)
        {
            tier = 'S';
        }
       else if(type <= 15)
        {
            tier = 'A';
        }
        else if(type <= 30)
        {
            tier = 'B';
        }
        else if(type <= 60)
        {
            tier = 'C';
        }
        else
        {
            tier = 'D';
        }
        return tier;
    }
   
    public EnemyTierStats enemyChanger(char tier)
    {
        switch (tier)
        {
            case 'S':
                return new EnemyTierStats(Random.Range(13, 18), 3f, 4000f, 4);
            case 'A':
                return new EnemyTierStats(Random.Range(10, 13), 1.5f, 800, 2);
            case 'B':
                return new EnemyTierStats(Random.Range(10, 12), 1.2f, 700f, 2);
            case 'C':
                return new EnemyTierStats(Random.Range(9, 11), 1.2f, 300f, 2);
            case 'D':
                return new EnemyTierStats(Random.Range(8, 10), 1f, 0f, 2);
            default:
                return new EnemyTierStats(10, 1f, 0f, 2);
        }
    }
}
public struct EnemyTierStats
{
    public int speed;
    public float scale;
    public float hpBonus;
    public int fireballSize;

    public EnemyTierStats(int speed, float scale, float hpBonus, int fbSize)
    {
        this.speed = speed;
        this.scale = scale;
        this.hpBonus = hpBonus;
        this.fireballSize = fbSize;
    }
}
