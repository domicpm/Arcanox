using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAoE : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawnAreaPrefab;
    public PlayerMovement player;
    Animator animator;
    public bool damage = false;
    public static bool isScaled = false;
    public static bool scaleNext = false;
    private int spawnAmount = 20;
    Vector2 spawnPosSquare;
    Vector2 spawnPosTest;
    private float timer { get; set; } = 0f;
    private float interval { get; set; } = 5f;
    SpellSquareBorder areaBorder;
    List<AoeAnimation> aoeList = new List<AoeAnimation>();
    private Vector3 saveScale;

    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        spawnPosTest = player.transform.position;
        GameObject aoeGO = Instantiate(spawnAreaPrefab, spawnPosTest, Quaternion.identity);
         areaBorder = aoeGO.GetComponent<SpellSquareBorder>();

        spawn();

        StartCoroutine(Delay2());
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval && !player.getDead() && EnemyManager.bossSpawned && !Enemy.bossDead)
        {
            StartCoroutine(Delay());
            timer = 0f;
        }
        GameObject boss = GameObject.FindWithTag("Boss");
        if (boss != null && areaBorder != null)
        {
            Enemy bossHp = boss.GetComponent<Enemy>();
            if (bossHp.hp < bossHp.maxhp / 2f)
            {
                if (!isScaled)
                {
                    scaleNext = true;
                }
                isScaled = true;
            }
        }
    }
    private void spawn()
    {
        areaBorder.gameObject.SetActive(true);
        aoeList.Clear();
        if (scaleNext)
        {
            areaBorder.gameObject.transform.localScale *= 1.4f;
            spawnAmount = 60;
        }
        else
        {
            areaBorder.gameObject.transform.localScale /= 1.4f;
            spawnAmount = 20;
        }
        for (int i = 0; i < spawnAmount; i++)
        {
            float x = Random.Range(areaBorder.left.position.x, areaBorder.right.position.x);
            float y = Random.Range(areaBorder.bottom.position.y, areaBorder.top.position.y);
            spawnPosSquare = new Vector3(x, y, 0f);
            GameObject enemyGO = Instantiate(prefab, spawnPosSquare, Quaternion.identity);

            AoeAnimation aoeComp = enemyGO.GetComponent<AoeAnimation>();
            if (aoeComp != null)
                aoeList.Add(aoeComp);

            Enemy enemy = enemyGO.GetComponent<Enemy>();
        }
    }

    IEnumerator Delay2()
    {
        yield return new WaitForSeconds(1f);
        damage = true;

        if (aoeList.Count > 0)
        {
            foreach (var aoe in aoeList)
            {
                aoe.startAnimation();

                DamageFromCircle dfc = aoe.GetComponent<DamageFromCircle>();
                if (dfc != null)
                    dfc.ExplosionDamage(aoe.transform.position);
            }
        }
        StartCoroutine(Destroy());

    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        foreach (var aoe in aoeList)
        {
            Destroy(aoe.gameObject);
        }
        areaBorder.gameObject.SetActive(false);
    }

}

