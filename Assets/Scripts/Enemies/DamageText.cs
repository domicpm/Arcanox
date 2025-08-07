using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro damageText;

    void Start()
    {
        Destroy(gameObject, 1f);
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

            if (dmg > 750)
            {
                ShowDamage(dmg, 14f, FontStyles.Bold | FontStyles.Italic, Color.red, 1.1f);
            }
            else if (dmg > 290)
            {
                ShowDamage(dmg, 13f, FontStyles.Bold | FontStyles.Italic, new Color(1f, 0.5f, 0f), 1.1f);
            }
            else
            {
                ShowDamage(dmg, 8f, FontStyles.Normal, Color.white, 1f);
            }
        }
        else
        {
            damageText.fontStyle = FontStyles.Bold;
            damageText.color = Color.white;
            damageText.text = "MISS";
        }

    }
    void ShowDamage(float damage, float size, FontStyles style, Color color, float scale)
    {
        damageText.text = damage.ToString();
        damageText.fontSize = size;
        damageText.fontStyle = style;
        damageText.color = color;
        damageText.transform.localScale = Vector3.one * scale;
    
    }
}
