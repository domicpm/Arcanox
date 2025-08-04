using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Button redPathButton;
    public Button bluePathButton;
    public Button greenPathButton;
    public GameManager gm;
    public static bool isBurn = false;
    public static bool isSlow = false;
    public static bool isHeal = false;
    public static int skillPoints = 0;

    void Start()
    {
        redPathButton.onClick.AddListener(OnRedButtonClick);
        bluePathButton.onClick.AddListener(OnBlueButtonClick);
        greenPathButton.onClick.AddListener(OnGreenButtonClick);
    }

    void Update()
    {
    }

    public void OnPointerEnter()
    {
        Debug.Log("Hover successful");
    }

    void OnRedButtonClick()
    {
        gm.onButtonClick = true;
        gm.setSkillTreeActive();
        PauseManager.Instance.Resume();
        isBurn = true;
        gameObject.SetActive(false);
    }

    void OnBlueButtonClick()
    {
        gm.onButtonClick = true;
        gm.setSkillTreeActive();
        PauseManager.Instance.Resume();
        isSlow = true;
        gameObject.SetActive(false);
    }

    void OnGreenButtonClick()
    {
        gm.onButtonClick = true;
        gm.setSkillTreeActive();
        PauseManager.Instance.Resume();
        isHeal = true;
        gameObject.SetActive(false);
    }
}
