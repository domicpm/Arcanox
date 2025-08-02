using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject prefabGreen;
    public GameObject prefabBlue;


    public ItemData greenBullet;
    public ItemData blueBullet;

    public static bool looted = false;
    public static int type = 0;
    public static Items Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    public void spawn(Vector3 enemypos)
    {
        int spawnChance = Random.Range(1, 101);
        if (spawnChance <= 20)
        {
            GameObject newBullet = Instantiate(prefabGreen, enemypos, Quaternion.identity);
            gameObject.transform.position = enemypos;
            type = 1;
        }else if(spawnChance > 20)
        {
            GameObject newBullet = Instantiate(prefabBlue, enemypos, Quaternion.identity);
            gameObject.transform.position = enemypos;
            type = 2;
        }
        
    }
    public void addToInventory(int type)
    {

        switch (type)
        {
            case 1:
                Inventory.Instance.addItemToInventory(greenBullet);
                break;
            case 2:
                Inventory.Instance.addItemToInventory(blueBullet);
                break;
            case 3:
            case 4:
            case 5:
            default:break;      
        }
    }

}
