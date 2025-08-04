using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    
    public ItemData greenBullet;
    public ItemData blueBullet;
    public static bool looted = false;
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
   
    public void addToInventory(int type)
    {

        switch (type)
        {
            case 1:
                Inventory.Instance.addSpellToInventory(greenBullet);
                break;
            case 2:
                Inventory.Instance.addSpellToInventory(blueBullet);
                break;
            case 3:
            case 4:
            case 5:
            default:break;      
        }
    }

}
