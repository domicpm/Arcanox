using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();
    public GameObject leftClick; //Auto Attack
    public GameObject rightClick; //Spell
    // Start is called before the first frame update
    void Start()
    {
        CreatePool(leftClick, 30);
        CreatePool(rightClick, 20);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Pooling:
    private void CreatePool(GameObject prefab, int sizeOfPool)
    {
        Queue<GameObject> newPool = new Queue<GameObject>();
        for (int i = 0; i < sizeOfPool; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.GetComponent<PrefabPooling>().SetPrefab(prefab);
            obj.SetActive(false);
            newPool.Enqueue(obj);
        }
        pool[prefab] = newPool;
    }
    public GameObject ActivateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Queue<GameObject> newPool = pool[prefab];
        if (pool.Count > 0)
        {
            GameObject obj = newPool.Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
        else
        {
            //GameObject newObj = Instantiate(prefab, transform);
            //newObj.GetComponent<PrefabPooling>().SetPrefab(prefab);
            //newObj.SetActive(true);
            //newObj.transform.position = position;
            //newObj.transform.rotation = rotation;
            //return newObj;
        }
        return null;
    }
    public void RemoveObject(GameObject obj)
    {
        obj.SetActive(false);
        pool[obj.GetComponent<PrefabPooling>().prefab].Enqueue(obj);
    }
}
