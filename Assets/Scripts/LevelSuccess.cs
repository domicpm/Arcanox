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
    public Button shopButton;
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
    public AzrielShop shop;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        continueLevelButton.onClick.AddListener(OnContinueLevelButtonClicked);
        teleportButton.onClick.AddListener(OnTeleportClicked);
        continueLevelButtoninRoom.onClick.AddListener(OnContinueLevelButtoninRoomClicked);
        azriel.gameObject.SetActive(false);
        shopButton.onClick.AddListener(OnShopButtonClicked);
    }
    private void Awake()
    {
       Instance = this;
        continueLevelButtoninRoom.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    public void OnContinueLevelButtonClicked()
    {
        //healthbar.setPlayerHealth(shop.hpboost + player.newhp);
        //healthbar.setPlayerMaxHealth(shop.hpboost + player.maxhp);
        level++;
        enemyManager.InitializeLevel(level, true);
        gameObject.SetActive(false);
        //continueLevelButtoninRoom.gameObject.SetActive(true);
        teleportButton.gameObject.SetActive(false);
    }
    public void OnShopButtonClicked() {
        shop.gameObject.SetActive(true);
        azriel.gameObject.SetActive(false);
    }
    public void OnContinueLevelButtoninRoomClicked()
    {
        nextSpawnPosition = new Vector2(-23.53f, -3.23f);
        player.transform.position = nextSpawnPosition;
        continueLevelButtoninRoom.gameObject.SetActive(false);
        azriel.gameObject.SetActive(false);
        OnContinueLevelButtonClicked();
        isInLootRoom = false;
        azriel.gameObject.SetActive(false);
        interactRange.gameObject.SetActive(false);
        Enemy.isDummy = false;
    }

    public void OnTeleportClicked()
    {
        isTeleported = true;
        nextSpawnPosition = new Vector2(82f, -87f); 
        player.transform.position = nextSpawnPosition;
        //player.Hp.gameObject.SetActive(false);
        //healthbar.gameObject.SetActive(false);
        gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        isInLootRoom = true;
        interactRange.gameObject.SetActive(true);
        Enemy.isDummy = true;
    }


    public void setAct()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
        {
            gameObject.SetActive(true);
            teleportButton.gameObject.SetActive(true);
            continueLevelText.text = "Level " + (level) + " done!";
        }
        else
        {
            Debug.Log("Fail0");
        }
    }
    

}
