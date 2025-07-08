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
    public Button continueLevelButtoninRoom;
    public Button teleportButton;
    public Text teleportText;
    public Text continueLevelText;
    public EnemyManager enemyManager;
    public PlayerMovement player;
    public static Vector2 nextSpawnPosition;
    public static bool isInLootRoom = false;
    public PlayerHealthBar healthbar;
    private bool isTeleported = false;
    public npcAzriel azriel;
    public Interact interactRange;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        continueLevelButton.onClick.AddListener(OnContinueLevelButtonClicked);
        teleportButton.onClick.AddListener(OnTeleportClicked);
        continueLevelButtoninRoom.onClick.AddListener(OnContinueLevelButtoninRoomClicked);
        azriel.gameObject.SetActive(false);
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
        continueLevelButtoninRoom.gameObject.SetActive(true);
        teleportButton.gameObject.SetActive(false);
    }
    public void OnContinueLevelButtoninRoomClicked()
    {
        nextSpawnPosition = new Vector2(-23.53f, -3.23f);
        player.transform.position = nextSpawnPosition;
        continueLevelButtoninRoom.gameObject.SetActive(false);
        azriel.gameObject.SetActive(false);
        OnContinueLevelButtonClicked();
        isInLootRoom = false;
        player.Hp.gameObject.SetActive(true);
        healthbar.gameObject.SetActive(true);
        azriel.gameObject.SetActive(false);
        interactRange.gameObject.SetActive(false);
    }

    public void OnTeleportClicked()
    {
        isTeleported = true;
        nextSpawnPosition = new Vector2(82f, -87f); 
        player.transform.position = nextSpawnPosition;
        player.Hp.gameObject.SetActive(false);
        healthbar.gameObject.SetActive(false);
        gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        isInLootRoom = true;
        interactRange.gameObject.SetActive(true);
    }


    public void setAct()
    {
        gameObject.SetActive(true);
        teleportButton.gameObject.SetActive(true);
        continueLevelText.text = "Level " + (level) +  " done!";
    }
    

}
