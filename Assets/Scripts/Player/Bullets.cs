using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    [HideInInspector] public float mindamage = 100;
    [HideInInspector] public float maxdamage = 300;
    [HideInInspector] public float mindamageSpell = 450;
    [HideInInspector] public float maxdamageSpell = 750;
    public float accuracy = 75;
    public float accuracySpell = 75;
    public float speed = 4f;

    public float damage;
    public float damageSpell;
    public ObjectPooling objectPooling;
    //public Sprite newSprite;
    public PlayerMovement player;
    private Vector3 originalScale;
    private CooldownUI cdUI;
    public GameObject b;
    public Spell spell;
    public Items item;
    private void Awake()
    {
        objectPooling = FindObjectOfType<ObjectPooling>();
    }

    private void Start()
    {
        cdUI = FindObjectOfType<CooldownUI>();
        UpdateDamage();
        originalScale = transform.localScale;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            objectPooling.RemoveObject(gameObject);
        }
        if (collision.gameObject.CompareTag("Dummy"))
        {
            objectPooling.RemoveObject(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // dmgtxt.spawnDmg(damage);
            objectPooling.RemoveObject(gameObject);
        }
    }

    public float getDmg()
    {
        return damage;
    }

    public float getDmgSpell()
    {
        return damageSpell;
    }
    public void UpdateDamage()
    {
        int random = Random.Range(1, 101);
        if (accuracy >= random)
        {
            damage = Mathf.RoundToInt(Random.Range(mindamage, maxdamage));
        }
        else
        {
            damage = 0;
        }
    }
    public void UpdateDamageSpell()
    {
        int random = Random.Range(1, 101);
        if (accuracySpell >= random)
        {
            damageSpell = Mathf.RoundToInt(Random.Range(mindamageSpell, maxdamageSpell));
        }
        else
        {
            damageSpell = 0;
        }
    }
    public void shoot()
    {
        if (PauseManager.Instance.IsPaused || LevelSuccess.isInLootRoom == true || LevelSuccess.levelDoneText == true) // wenn Pause gedrückt oder in loot room, werden keine weiteren Bullets gespawnt
            return;

        // var bullet = Instantiate(b, player.bp.transform.position, Quaternion.identity); // <-- p.transform raus
        GameObject bullet = objectPooling.ActivateObject(objectPooling.leftClick, player.bp.transform.position, Quaternion.identity);
        var renderer = bullet.GetComponent<SpriteRenderer>();
        bullet.transform.localScale = originalScale;

        if (Items.looted == true)
        {
            //renderer.GetComponent<SpriteRenderer>().sprite = newSprite;
            renderer.color = new Color32(0xA2, 0xCF, 0x00, 0xFF);
            bullet.transform.localScale = bullet.transform.localScale * 2f; // skaliert das Bullet um 1.5x
            Debug.Log("Bullet wurde gefärbt");

        }
        UpdateDamage();
        Vector3 bulletDir = player.bp.transform.up;
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletDir * speed, ForceMode2D.Impulse);
    }
    public void shootLeft()
    {
        cdUI.ResetCooldown("spell");
        if (PauseManager.Instance.IsPaused || LevelSuccess.isInLootRoom == true) // wenn Pause gedrückt, werden keine weiteren Bullets gespawnt
            return;

        //var bullet = Instantiate(spell, player.bp.transform.position, Quaternion.identity);
        GameObject bullet = objectPooling.ActivateObject(objectPooling.rightClick, player.bp.transform.position, Quaternion.identity);
        if (bullet == null) return;
        UpdateDamageSpell();
        Vector3 bulletDir = player.bp.transform.up;
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletDir * speed, ForceMode2D.Impulse);
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
    }
}
