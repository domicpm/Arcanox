using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawn : MonoBehaviour
{
    public GameObject gras;
    public GameObject skeleton;
    public GameObject dragonSkeleton;
    Vector3 spawnpos;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 35; i++)
        {
            spawnpos = new Vector3(Random.Range(-94f, -32f), Random.Range(-5f, 30f), 0);
            Instantiate(gras, spawnpos, Quaternion.identity);
        }
        for(int i = 0; i < 10; i++)
        {
            spawnpos = new Vector3(Random.Range(-94f, -32f), Random.Range(-5f, 30f), 0);
            Instantiate(skeleton, spawnpos, Quaternion.identity);
            spawnpos = new Vector3(Random.Range(-94f, -32f), Random.Range(-5f, 30f), 0);
            Instantiate(dragonSkeleton, spawnpos, Quaternion.identity);
        }
    }          

    // Update is called once per frame
    void Update()
    {
        
    }
}
