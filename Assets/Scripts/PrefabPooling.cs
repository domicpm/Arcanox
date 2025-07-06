using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPooling : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    public void SetPrefab(GameObject _prefab)
    {
        prefab = _prefab;
    }
}
