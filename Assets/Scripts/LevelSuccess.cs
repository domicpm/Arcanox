using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSuccess : MonoBehaviour
{
    public static LevelSuccess Instance;
    public int level = 1;
    public Button continueLevelButton;
    public Button teleportButton;
    public Text teleportText;
    public Text continueLevelText;
    public EnemyManager enemyManager;
    public PlayerMovement player;
    public static Vector2 nextSpawnPosition;
    public static bool isInLootRoom = false;
    public PlayerHealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        continueLevelButton.onClick.AddListener(OnContinueLevelButtonClicked);
        teleportButton.onClick.AddListener(OnTeleportClicked);
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
    

    public void OnTeleportClicked()
    {
        nextSpawnPosition = new Vector2(82f, -87f); // gewünschte Position im Loot-Raum
        player.transform.position = nextSpawnPosition;
        player.Hp.gameObject.SetActive(false);
        healthbar.gameObject.SetActive(false);

        gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        isInLootRoom = true;
    }


    public void setAct()
    {
        gameObject.SetActive(true);
        teleportButton.gameObject.SetActive(true);
        continueLevelText.text = "Level " + (level) +  " done!";
    }
    

}
