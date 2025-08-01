using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Experience : MonoBehaviour
{
    public PlayerMovement player;
    public SkillTree st;
    public PlayerExpBar exp;
    private int expPerLevel = 40;

    public Text playerLevelText;
    private int playerLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        st.gameObject.SetActive(false);
        exp.setPlayerMaxExp(player.maxExperience);
    }

    // Update is called once per frame
    void Update()
    {
        exp.setPlayerExp(player.experience);
        if (player.experience >= player.maxExperience)
        {
            SkillTree.skillPoints++;
            playerLevel++;
            playerLevelText.text = playerLevel.ToString();
            player.experience = player.experience - player.maxExperience;
            player.maxExperience += expPerLevel;
            exp.setPlayerMaxExp(player.maxExperience);
        }                 
    }
}
