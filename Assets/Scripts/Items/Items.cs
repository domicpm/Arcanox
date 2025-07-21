using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public static bool looted = false;
    public static int type = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            looted = true;
            Destroy(gameObject);
        }
    }
    public void spawn(Vector3 enemypos)
    {
        int spawnChance = Random.Range(1, 101);
        if (spawnChance <= 20)
        {
            GameObject newBullet = Instantiate(prefab1, enemypos, Quaternion.identity);
            gameObject.transform.position = enemypos;
            type = 1;
        }else if(spawnChance > 20)
        {
            GameObject newBullet = Instantiate(prefab2, enemypos, Quaternion.identity);
            gameObject.transform.position = enemypos;
            type = 2;
        }
        
    }

}
