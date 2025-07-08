using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject UI;
    private bool isInRange = false;

    // Start is called before the first frame update
    void Awake()
    {
        UI.SetActive(false);
    }

    // Update is called once per frame

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.I))
        {
            UI.SetActive(true);
            Debug.Log("Interacted with NPC");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Npc"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Npc"))
        {
            isInRange = false;
        }
    }

}
