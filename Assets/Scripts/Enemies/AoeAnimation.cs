using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAnimation : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on AoeAnimation GameObject!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void startAnimation() {
        if(animator != null)
        animator.SetTrigger("isTriggered");
        else
        {
            Debug.Log("animator is null");
        }
    }
    
}
