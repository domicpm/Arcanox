using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro damageText;

    void Start()
    {
        Destroy(gameObject, 0.8f);
    }

    void Update()
    {
        transform.position += Vector3.up * 2.5f * Time.deltaTime;
    }

    public void SetDamage(float dmg)
    {
        if (dmg != 0)
        {
            damageText.text = dmg.ToString();
        }
        else
        {
            damageText.fontStyle = FontStyles.Bold;
            damageText.text = "MISS";
        }

        // Style-Regeln direkt hier:
        if (dmg > 250) // Beispiel: starker Schaden
        {
            damageText.fontSize = 12f;
            damageText.fontStyle = FontStyles.Bold | FontStyles.Italic;
            damageText.color = Color.red;
        }
        else
        {
            damageText.fontSize = 8f;
            damageText.fontStyle = FontStyles.Normal;
        }
    }
}
