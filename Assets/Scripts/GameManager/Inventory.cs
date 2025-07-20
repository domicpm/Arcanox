using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public PlayerMovement player;
    public Bullets bullets;
    public Text hp;
    public Text ad;
    public Text ats;
    public Text ap;
    public Text acc;
    public Text cdr;
    public Text ms;
    public Text phEva;
    public Text magEva;
    public Text heal;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = player.maxhp.ToString();
        ad.text = bullets.maxdamage.ToString(); // bezieht sich aktuell nur auf MaxDmg, evtl Average bilden oder Range anzeigen
        ats.text = bullets.speed.ToString();
        ap.text = bullets.maxdamageSpell.ToString(); // wie oben
        acc.text = bullets.accuracy.ToString() + "%";
        cdr.text = "0" + "%";
        ms.text = player.speed.ToString();
        phEva.text = "0";
        magEva.text = "0";
        heal.text = player.healamount.ToString();
    }
}
