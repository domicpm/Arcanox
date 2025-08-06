using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPosition : MonoBehaviour
{
    public RotatePlayerSprite player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if (mouseX <= player.transform.position.x)
        {
            player.rotate(false);
        }
        else
        {        
            player.rotate(true);
        }
    }
}
