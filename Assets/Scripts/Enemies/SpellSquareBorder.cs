using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSquareBorder : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public Transform left;
    public Transform right;
    Vector3 pos1;
    Vector3 pos2;
    Vector3 pos3;
    Vector3 pos4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos1 = top.position;
        pos2 = top.position;
        pos3 = top.position;
        pos4 = top.position;

        if (pos1.x < -32f)
        {
            pos1.x = -32f;
            top.position = pos1;
        }
        if(pos2.x < -95f)
        {
            pos2.x = -95f;
            top.position = pos2;
        }
        if (pos3.y < -8f)
        {
            pos3.x = -8f;
            top.position = pos3;
        }
        if (pos4.y < 30f)
        {
            pos4.x = 30f;
            top.position = pos4;
        }

    }
}
