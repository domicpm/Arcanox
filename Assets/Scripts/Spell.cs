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
        if (objectPooling == null) return; // Safety check

        if (collision.gameObject.CompareTag("Wall"))
        {
            objectPooling.RemoveObject(gameObject);
            transform.localScale = originalScale;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;

            StartCoroutine(MySequence());
        }
    }

    IEnumerator MySequence()
    {
        transform.localScale = originalScale * 5.5f;
        yield return new WaitForSeconds(0.05f);

        objectPooling.RemoveObject(gameObject);
        transform.localScale = originalScale; // Reset Scale after returning to pool
    }

}
