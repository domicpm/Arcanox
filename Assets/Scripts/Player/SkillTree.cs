using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Button redPathButton;
    public Button bluePathButton;
    public Button greenPathButton;

    public static bool isBurn = false;
    public static bool isSlow = false;
    public static bool isHeal = false;

    void Start()
    {
        gameObject.SetActive(false);

        redPathButton.onClick.AddListener(OnRedButtonClick);
        bluePathButton.onClick.AddListener(OnBlueButtonClick);
        greenPathButton.onClick.AddListener(OnGreenButtonClick);
    }

    void Update()
    {
        // Optional: Logic during runtime
    }

    public void OnPointerEnter()
    {
        Debug.Log("Hover successful");
    }

    void OnRedButtonClick()
    {
        isBurn = true;
    }

    void OnBlueButtonClick()
    {
        isSlow = true;
    }

    void OnGreenButtonClick()
    {
        isHeal = true;
    }
}
