using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrops : MonoBehaviour
{
    public GameObject heal;
    public GameObject attackBoost;
    public GameObject chest;


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

       
    }
    public void spawn(Vector3 enemypos, int type)
    {
        GameObject newHeal;
        switch (type)
        {
            case 1:
              newHeal = Instantiate(attackBoost, enemypos, Quaternion.identity);
        gameObject.transform.position = enemypos;
                break;
            case 2:
                 newHeal = Instantiate(heal, enemypos, Quaternion.identity);
                gameObject.transform.position = enemypos;
                break;
            case 3:
                 newHeal = Instantiate(chest, enemypos, Quaternion.identity);
                gameObject.transform.position = enemypos;
                break;
        }
    }
   
    
}
