using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image spellCooldownImage; // das UI Image mit Fill Type = Filled
    public Image spellshieldCooldownImage;
    public Image dashCooldownImage;


    private float spellCooldownTimer;
    private float spellshieldCooldownTimer;
    private float dashCooldownTimer;

    void Start()
    {
        spellCooldownImage.gameObject.SetActive(false);
        spellshieldCooldownImage.gameObject.SetActive(false);
        dashCooldownImage.gameObject.SetActive(false);
        spellCooldownTimer = PlayerAttackSpawn.fireCooldownSpell;
        spellshieldCooldownTimer = PlayerMovement.spellShieldCooldown;
        dashCooldownTimer = PlayerMovement.dashCooldown;
        spellshieldCooldownImage.fillAmount = 1f;
        dashCooldownImage.fillAmount = 1f;
        spellCooldownImage.fillAmount = 1f; 
    }

    void Update()
    {
        if (spellCooldownTimer < PlayerAttackSpawn.fireCooldownSpell)
        {
            spellCooldownTimer += Time.deltaTime;
            spellCooldownImage.fillAmount = spellCooldownTimer / PlayerAttackSpawn.fireCooldownSpell;
        }
        else 
        { 
            spellCooldownImage.gameObject.SetActive(false);
        }
        if (spellshieldCooldownTimer < PlayerMovement.spellShieldCooldown)
        {
            spellshieldCooldownTimer += Time.deltaTime;
            spellshieldCooldownImage.fillAmount = spellshieldCooldownTimer / PlayerMovement.spellShieldCooldown;
        }
        else
        {
            spellshieldCooldownImage.gameObject.SetActive(false);
        }
        if (dashCooldownTimer < PlayerMovement.dashCooldown)
        {
            dashCooldownTimer += Time.deltaTime;
            dashCooldownImage.fillAmount = dashCooldownTimer / PlayerMovement.dashCooldown;
        }
        else
        {
            dashCooldownImage.gameObject.SetActive(false);
        }
    }

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
        else if(type == "dash")
        {
            dashCooldownTimer = 0f;
            dashCooldownImage.fillAmount = 0f;
        }
    }
}
