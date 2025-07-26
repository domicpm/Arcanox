using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectPooling objectPooling;
    private Vector3 originalScale;
    private void Awake()
    {
        objectPooling = FindObjectOfType<ObjectPooling>();
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectPooling == null) return;

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Dummy"))
        {
            // Direkte Rückgabe ohne Animation
            objectPooling.RemoveObject(gameObject);
            transform.localScale = originalScale;
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            // Coroutine startet, und erst am Ende wird das Objekt deaktiviert!
            StartCoroutine(MySequence());
        }
    }

    IEnumerator MySequence()
    {
        Debug.Log("Scaling up!");
        transform.localScale = originalScale * 5.5f;

        yield return new WaitForSeconds(0.05f);

        Debug.Log("Scaling back down");
        transform.localScale = originalScale;

        // Jetzt erst zurück in den Pool (deaktiviert das GameObject)
        objectPooling.RemoveObject(gameObject);
    }

}
