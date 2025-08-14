using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPosition : MonoBehaviour
{
    public RotatePlayerSprite player;

    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (mouseX <= player.transform.position.x || horizontalInput < 0)
        {
            player.rotate(false);
        }
        else
        {        
            player.rotate(true);
        }
    }
}
