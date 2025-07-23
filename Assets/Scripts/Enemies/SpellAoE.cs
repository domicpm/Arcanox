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
    Vector2 spawnPosSquare;
    Vector2 spawnPosTest;
    private float timer { get; set; } = 0f;
    private float interval { get; set; } = 5f;

    List<AoeAnimation> aoeList = new List<AoeAnimation>();

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        spawnPosTest = player.transform.position;
        GameObject aoeGO = Instantiate(spawnAreaPrefab, spawnPosTest, Quaternion.identity);

        spawn();

        StartCoroutine(Delay2());
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval && !player.getDead() && EnemyManager.bossSpawned)
        {
            StartCoroutine(Delay());
            timer = 0f;
        }
    }
    private void spawn()
    {
        aoeList.Clear();

        for (int i = 0; i < 20; i++)
        {
            spawnPosSquare = new Vector3(Random.Range(spawnPosTest.x - 12, spawnPosTest.x + 12), Random.Range(spawnPosTest.y - 8.5f, spawnPosTest.y + 6));
            GameObject enemyGO = Instantiate(prefab, spawnPosSquare, Quaternion.identity);

            AoeAnimation aoeComp = enemyGO.GetComponent<AoeAnimation>();
            if (aoeComp != null)
                aoeList.Add(aoeComp);

            Enemy enemy = enemyGO.GetComponent<Enemy>();
        }
    }

    IEnumerator Delay2()
    {
        yield return new WaitForSeconds(2f);
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
    }

}

