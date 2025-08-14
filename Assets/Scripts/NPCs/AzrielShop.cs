using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AzrielShop : MonoBehaviour
{
    public PlayerMovement player;
    public Bullets bullet;
    public Button Upgrade1;
    public Button Upgrade2;
    public Button Upgrade3;
    public LevelSuccess ls;
    public Inventory item;
    public float hpboost = 150;
    public float cooldown = 0f;
    public static int type;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Upgrade1.onClick.AddListener(onUpgrade1clicked);
        Upgrade2.onClick.AddListener(onUpgrade2clicked);
        Upgrade3.onClick.AddListener(onUpgrade3clicked);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onUpgrade1clicked(){
        player.newhp += hpboost;
        player.maxhp += hpboost;
        player.speed += 2;
        type = 1;
        gameObject.SetActive(false);
    }
    public void onUpgrade2clicked()
    {
        Bullets.mindamage += 20;
        Bullets.maxdamage += 20;
        Bullets.accuracy += 5; 
        type = 2;
        gameObject.SetActive(false);
    }
    public void onUpgrade3clicked()
    {
        cooldown = 0.2f;
        Bullets.mindamageSpell += 50;
        Bullets.maxdamageSpell += 50;
        PlayerAttackSpawn.fireCooldownSpell *= cooldown; // problematisch bei Mehrfachaufruf
        type = 3;
        item.addSpellToInventory(item.blueItem);
        gameObject.SetActive(false);
    }


}
