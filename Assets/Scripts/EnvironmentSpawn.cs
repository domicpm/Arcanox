using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawn : MonoBehaviour
{
    public GameObject gras;
    Vector3 spawnpos;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 30; i++)
        {
            spawnpos = new Vector3(Random.Range(-100f, -32f), Random.Range(-5f, 30f), 0);
            Instantiate(gras, spawnpos, Quaternion.identity);
        }
    }          

    // Update is called once per frame
    void Update()
    {
        
    }
}
