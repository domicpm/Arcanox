using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image cooldownImage; // das UI Image mit Fill Type = Filled
    public float cooldownDuration = 2f;
    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = weapon.fireCooldownSpell;
        cooldownImage.fillAmount = 1f; // Anfang leer
    }

    void Update()
    {
        if (cooldownTimer < weapon.fireCooldownSpell)
        {
            cooldownTimer += Time.deltaTime;
            cooldownImage.fillAmount = cooldownTimer / weapon.fireCooldownSpell;
        }
    }

    // Optional: Cooldown neu starten
    public void ResetCooldown()
    {
        cooldownTimer = 0f;
        cooldownImage.fillAmount = 0f;
    }
}
