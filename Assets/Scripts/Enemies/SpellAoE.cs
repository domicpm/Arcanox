using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAoE : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawnAreaPrefab;
    public PlayerMovement player;
    Animator animator;

    Vector2 spawnPosSquare;
    Vector2 spawnPosTest;

    List<AoeAnimation> aoeList = new List<AoeAnimation>();

    void Start()
    {
        StartCoroutine(Delay());
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

        if (aoeList.Count > 0)
        {
            foreach (var aoe in aoeList)
            {
                aoe.startAnimation();
            }
        }
        else
        {
            Debug.Log("Keine AoeAnimation-Komponenten gefunden.");
        }
    }
}
