using UnityEngine;
using UnityEngine.SceneManagement;


public class L1GameManager : MonoBehaviour
{
    public CameraFollowPlayer2D cam;
    public Laser laser;
    public GameObject winMenu;
    public L1PauseMenu pauseMenu;
    public PlayerCollect playerCollect;
    public PlayerMovement playerMovement;
    public GameObject goToExitPanel;

    public static bool LevelComplete { get; private set; }
    public static bool playerInEndZone;
    public static bool isGameStarted;


    private void Awake()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerData playerSave = SaveSystem.LoadPlayer();

        if (playerSave != null)
            playerSave.Level = currentLevel;

        else // no save found
            playerSave = new PlayerData(currentLevel);
        
        SaveSystem.SavePlayer(playerSave);
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelComplete = false;
        playerInEndZone = false;
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelComplete)
        {
            if (!goToExitPanel.activeInHierarchy && playerCollect.AllTasksComplete)
            {
                goToExitPanel.SetActive(true);
                playerCollect.showCollectPanels(false);
                laser.turnOffLaser();
            }

            else if (playerInEndZone)
            {
                LevelComplete = true;
                goToExitPanel.SetActive(false);
                pauseMenu.enabled = false;
                showWinMenu();
            }
        }
    }

    public void showWinMenu()
    {
        Time.timeScale = 0f; // stop time
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
        winMenu.SetActive(true); // show win menu
    }

    public void GoToNextLevel()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
