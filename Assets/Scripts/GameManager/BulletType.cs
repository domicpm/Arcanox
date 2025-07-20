using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletType : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Items.looted == true)
        {
            var renderer = bullet.GetComponent<Image>();
            renderer.color = new Color32(0xA2, 0xCF, 0x00, 0xFF);

        }

    }
}
