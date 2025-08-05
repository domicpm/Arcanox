using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletType : MonoBehaviour
{
    // FOR UI
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject spell;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Items.looted == true && ItemDrops.type == 1)
        {
            var renderer = bullet.GetComponent<Image>();
            renderer.color = new Color32(0xA2, 0xCF, 0x00, 0xFF);
        }
        else if(Items.looted == true && ItemDrops.type == 2)
        {
            var renderer = bullet.GetComponent<Image>();
            renderer.color = new Color32(0x5C, 0x5F, 0x98, 0xFF);
        }
        else if (Items.looted == true && ItemDrops.type == 3)
        {
            var renderer = spell.GetComponent<Image>();
            renderer.color = new Color32(0xF2, 0x67, 0x67, 0xFF); 
        }

    }
}
