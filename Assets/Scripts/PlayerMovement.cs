using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 mousePos;
    public float angle;

    public static float spellShieldCooldown = 7f;
    public const float maxhp = 200;
    public int damageFromEnemy = 3;
    private float healamount = 50;
    public float speed = 8f;
    public int potamount = 0;

    public PlayerHealthBar healthbar;
    public Text Hp;
    public Heal heal;
    public float newhp;

    public bool PlayerGotDamage;
    public PotImageUI potui;
    public Bullets s;
    public bool isDead = false;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 60f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 1f;
    private bool shield;
    private bool onCooldown = false;
    public Enemy enemy;
    [SerializeField] private TrailRenderer tr;
    public GameObject player;
    public RotatePlayerSprite playerSprite;
    public BulletPosition bp;
    public SpellShield spellshield;
    public CooldownUI cdUI;
    public weapon wp;
    public Godmode gm;
    void Start()
    {
        newhp = maxhp;
        healthbar.setPlayerMaxHealth(maxhp);
        rb = GetComponent<Rigidbody2D>();
        Hp.text = healthbar.getPlayerHealth().ToString();
        potamount = 0;
        cdUI = FindObjectOfType<CooldownUI>();
    }
    

    void Update()
    {
        if (PauseManager.Instance.IsPaused)
            return;
        //if(Enemy.allCleared == true)
        //    return;
        
        if (!isDead)
        {
            if (PauseManager.Instance.IsPaused)
                return;

            if (!isDead)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    //GodMode
                    gm.gameObject.SetActive(true);
                    s.mindamage = 1000;
                    s.maxdamage = 1001;
                    s.mindamageSpell = 1500;
                    s.maxdamageSpell = 1501;
                    speed = 20;
                    damageFromEnemy = 0;
                    s.accuracy = 100;
                    wp.fireCooldown = 0.1f;
                }
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton3))
                {
                    if (onCooldown == false)
                    {
                        cdUI.ResetCooldown("spellshield");
                        StartCoroutine(Cooldown());
                        spellshield.gameObject.SetActive(true);
                        shield = true;
                        StartCoroutine(setSpellshieldTimer());
                    }               
                }
                    float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                 //Controller rechter Stick auslesen (XY)
                float rightStickX = Input.GetAxis("RightStickHorizontal");
                float rightStickY = Input.GetAxis("RightStickVertical");

                Vector2 rightStickDir = new Vector2(rightStickX, rightStickY);

                if (rightStickDir.magnitude > 0.1f) // Wenn Stick bewegt wird
                {
                    //Angle mit Stick - Richtung berechnen
                    angle = Mathf.Atan2(rightStickY, rightStickX) * Mathf.Rad2Deg - 90f;
                }
                else if(InputDevice.mouse == true)
                {
                    // Sonst Maus verwenden wie bisher
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
                }

                Vector3 movement = transform.position + new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;
                transform.position = movement;
                bp.transform.localRotation = Quaternion.Euler(0, 0, angle);

                // Heiltrank benutzen (Taste H)
                if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (potamount > 0 && newhp < maxhp)
                    {
                        newhp += healamount;
                        if (newhp > maxhp) newhp = maxhp;

                        potamount--;
                        healthbar.setPlayerHealth(newhp);
                        Hp.text = newhp.ToString();
                    }
                    else
                    {
                        Debug.Log("Keine Pots");
                    }
                }

                // Dash (Leertaste oder Controller)
                if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(Dash());
                }
            }
        }

        IEnumerator Dash()
        {
            Vector2 originalVelocity = rb.velocity;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            tr.emitting = true;

            float dashDirectionX = Input.GetAxisRaw("Horizontal");
            float dashDirectionY = Input.GetAxisRaw("Vertical");

            if (dashDirectionX != 0)
            {
                rb.velocity = new Vector2(dashDirectionX * dashingPower, rb.velocity.y);
            }
            else if (dashDirectionY != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, dashDirectionY * dashingPower);
            }
            else
            {
                Debug.Log("No dash input detected");
            }

            yield return new WaitForSeconds(dashingTime);
            tr.emitting = false;
            rb.velocity = originalVelocity;
            rb.gravityScale = originalGravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealPot"))
        {
            increasepot();
        }

        if (collision.gameObject.CompareTag("AttackSpeedBuff"))
        {
            s.speed += 1;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Damage durch Enemy – deaktiviert im Code, kann bei Bedarf aktiviert werden
            // newhp -= damageFromEnemy;
            // hp.setPlayerHealth(newhp);
            // Hp.text = newhp.ToString();
            // if (newhp <= 0 && !isDead)
            // {
            //     isDead = true;
            //     playerSprite.setDeadAnimation();
            // }
        }

        if (collision.gameObject.CompareTag("FireBall"))
        {
            if (shield == false)
            {
                playerSprite.setGotHitAnimation();
                newhp -= damageFromEnemy;
                healthbar.setPlayerHealth(newhp);
                Hp.text = newhp.ToString();

                if (newhp <= 0 && !isDead)
                {
                    Hp.gameObject.SetActive(false);
                    healthbar.gameObject.SetActive(false);
                    isDead = true;
                    playerSprite.setDeadAnimation();
                }
            }
        }
    }

    public void increasepot()
    {
        potamount++;
    }

    public void destroyObj()
    {
        gameObject.SetActive(false);
    }

    public bool getDead()
    {
        return isDead;
    }
    IEnumerator setSpellshieldTimer()
    {
        yield return new WaitForSeconds(2f);
        shield = false;
        spellshield.gameObject.SetActive(false);
    }
    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(spellShieldCooldown);
        onCooldown = false;
    }
}
