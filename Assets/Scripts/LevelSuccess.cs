using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSuccess : MonoBehaviour
{
    public static LevelSuccess Instance;
    public int level = 1;
    public Button continueLevelButton;
    public Text continueLevelText;
    public EnemyManager enemyManager;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        continueLevelButton.onClick.AddListener(OnContinueLevelButtonClicked);

    }
    private void Awake()
    {
       Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    public void OnContinueLevelButtonClicked()
    {
        level++;

        enemyManager.InitializeLevel(level, true);
        gameObject.SetActive(false);
    }
    public void setAct()
    {
        gameObject.SetActive(true);
        continueLevelText.text = "Level " + (level) +  " done!";
    }
    

}
