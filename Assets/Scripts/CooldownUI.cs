using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image spellCooldownImage; // das UI Image mit Fill Type = Filled
    public Image spellshieldCooldownImage;


    private float spellCooldownTimer;
    private float spellshieldCooldownTimer;

    void Start()
    {
        spellCooldownTimer = weapon.fireCooldownSpell;
        spellCooldownImage.fillAmount = 1f; // Anfang leer
        spellshieldCooldownTimer = PlayerMovement.spellShieldCooldown;
        spellshieldCooldownImage.fillAmount = 1f;
    }

    void Update()
    {
        if (spellCooldownTimer < weapon.fireCooldownSpell)
        {
            spellCooldownTimer += Time.deltaTime;
            spellCooldownImage.fillAmount = spellCooldownTimer / weapon.fireCooldownSpell;
        }
        if (spellshieldCooldownTimer < PlayerMovement.spellShieldCooldown)
        {
            spellshieldCooldownTimer += Time.deltaTime;
            spellshieldCooldownImage.fillAmount = spellshieldCooldownTimer / PlayerMovement.spellShieldCooldown;
        }
    }

    // Optional: Cooldown neu starten
    public void ResetCooldown(string type)
    {
        if (type == "spell")
        {
            spellCooldownTimer = 0f;
            spellCooldownImage.fillAmount = 0f;
        }
        else if (type == "spellshield")
        {
            spellshieldCooldownTimer = 0f;
            spellshieldCooldownImage.fillAmount = 0f;
        }
    }
}
