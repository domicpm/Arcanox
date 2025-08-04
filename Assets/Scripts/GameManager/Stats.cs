using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public PlayerMovement player;
    public Bullets bullets;
    public AzrielShop azs;

    public Text hp;
    public Text ad;
    public Text ats;
    public Text ap;
    public Text acc;
    public Text accSpell;
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
        ad.text = Bullets.maxdamage.ToString(); // bezieht sich aktuell nur auf MaxDmg, evtl Average bilden oder Range anzeigen
        ats.text = bullets.speed.ToString();
        ap.text = Bullets.maxdamageSpell.ToString(); // wie oben
        acc.text = Bullets.accuracy.ToString() + "%";
        accSpell.text = Bullets.accuracySpell.ToString() + "%";
        cdr.text = ((1 - azs.cooldown) * 100).ToString() + "%";
        ms.text = player.speed.ToString();
        phEva.text = "0";
        magEva.text = "0";
        heal.text = player.healamount.ToString();
    }
}
