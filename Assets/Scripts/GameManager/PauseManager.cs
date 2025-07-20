using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public Button continueGameButton;
    public Inventory inventory;
    public Text pauseText;
    private bool invActive = false;
    public bool IsPaused { get; private set; } = false;

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
            Resume();
            inventory.gameObject.SetActive(false);
        }
        else
        {
            Pause();
            inventory.gameObject.SetActive(true);
        }
    }
    public void TogglePause()
    {
        if (IsPaused)
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

    public void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        invActive = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false; 
        invActive = false;

    }
}
