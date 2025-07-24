using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DamageFromCircle : MonoBehaviour
{
    public float radius;
    private int damageFromExplosion = 20;
    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        radius = circle.radius * transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExplosionDamage(Vector3 center)
    {
        PlayerMovement p = FindObjectOfType<PlayerMovement>(); 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                p.damageInc(damageFromExplosion);
            }
        }
    }
}
