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
    public float hpboost = 150;
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
        gameObject.SetActive(false);
        ls.continueLevelButtoninRoom.gameObject.SetActive(true);
    }
    public void onUpgrade2clicked()
    {
        bullet.mindamage += 20;
        bullet.maxdamage += 20;
        bullet.accuracy += 5;
        gameObject.SetActive(false);
        ls.continueLevelButtoninRoom.gameObject.SetActive(true);
    }
    public void onUpgrade3clicked()
    {
        bullet.mindamageSpell += 50;
        bullet.maxdamageSpell += 50;
        PlayerAttackSpawn.fireCooldownSpell *= 0.90f; // problematisch bei Mehrfachaufruf
        gameObject.SetActive(false);
        ls.continueLevelButtoninRoom.gameObject.SetActive(true);

    }


}
