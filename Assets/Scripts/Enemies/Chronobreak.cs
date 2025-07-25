using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronobreak : MonoBehaviour
{
    public Vector2 position;
    public float time;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Chronobreak(float t, Vector2 pos)
    {
        time = t;
        position = pos;
        Debug.Log("" + time + position);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
