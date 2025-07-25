using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTree : MonoBehaviour
{
    // Start is called before the first frame update
    private Button redPathButton;
    private Button bluePathButton;
    private Button greenPathButton;
    void Start()
    {
        gameObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter()
    {
        Debug.Log("Hover successful");
    }

}
