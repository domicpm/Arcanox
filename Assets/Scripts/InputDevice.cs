using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputDevice : MonoBehaviour
{
    public Button ButtonController;
    public Button ButtonMouse;
    public static bool mouse = false;
    private bool firstTimeCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        ButtonController.onClick.AddListener(OnButtonClick); 
        ButtonMouse.onClick.AddListener(OnButtonClick1);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnButtonClick()
    {
        mouse = false;
        SceneManager.LoadScene("SampleScene");
        firstTimeCheckfunc();
    }
    void OnButtonClick1()
    {
        mouse = true;
        SceneManager.LoadScene("SampleScene");
        firstTimeCheckfunc();
    }
    void firstTimeCheckfunc()
    {
        if (firstTimeCheck == true)
        {
            EnemyManager.Instance.InitializeLevel(1, true);
        }
        else
        {
            EnemyManager.Instance.InitializeLevel(1, false);
        }
        firstTimeCheck = true;
    }
}
