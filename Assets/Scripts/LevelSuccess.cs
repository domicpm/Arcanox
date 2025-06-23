using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSuccess : MonoBehaviour
{
    public static LevelSuccess Instance;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);   
    }
    private void Awake()
    {
       Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    public void setAct()
    {
        gameObject.SetActive(true);

    }

}
