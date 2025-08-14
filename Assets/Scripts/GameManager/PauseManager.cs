using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public Button continueGameButton;
    public Stats stats;
    public Text pauseText;
    public SkillTree skilltree;
    public PlayerExpBar peb;
    public PlayerHealthBar phb;
    private bool invActive = false;
    private bool isPausedSkillTree = false;
    public bool isPaused = false;
    public bool gameFreezed = false;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        pauseText.gameObject.SetActive(false);
        continueGameButton.gameObject.SetActive(false);
        continueGameButton.onClick.AddListener(OnContinueButtonClicked);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.Tab)) //Inventory
        {
            ToggleInv();
        }
    }

    public void OnContinueButtonClicked()
    {
        Resume();
        pauseText.gameObject.SetActive(false);
        continueGameButton.gameObject.SetActive(false);
    }
    public void ToggleInv()
    {
        if (invActive)
        {
            peb.gameObject.SetActive(true); // setzt XP bar aktiv wenn Inv inaktiv
            phb.gameObject.SetActive(true); // setzt HP bar inaktiv wenn Inv aktiv
            Resume();
            stats.gameObject.SetActive(false);
        }
        else
        {
            peb.gameObject.SetActive(false); // setzt XP bar inaktiv wenn Inv aktiv
            phb.gameObject.SetActive(false); // setzt HP bar inaktiv wenn Inv aktiv
            Pause();
            stats.gameObject.SetActive(true);
        }
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
            pauseText.gameObject.SetActive(false);
            continueGameButton.gameObject.SetActive(false);
        }
        else
        {
            Pause();
            pauseText.gameObject.SetActive(true);
            continueGameButton.gameObject.SetActive(true);
        }
    }
    public void ToggleSkillTree()
    {
        if (isPausedSkillTree)
        {
            Resume();
            skilltree.gameObject.SetActive(false);
        }
        else
        {
            skilltree.gameObject.SetActive(true);
            Pause();
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        invActive = true;
        isPausedSkillTree = true;
        gameFreezed = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false; 
        invActive = false;
        isPausedSkillTree = false;
        gameFreezed = false;
    }
}
