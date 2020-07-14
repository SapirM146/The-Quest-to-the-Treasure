using UnityEngine;

public class L1PauseMenu : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject pauseMenu;
    public PlayerCollect playerCollect;

    public static bool isGamePaused;


    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;
        gamePause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!L1GameManager.isGameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            instructionPanel.SetActive(false);
            playerCollect.showCollectPanels(true);
            resume();
            L1GameManager.isGameStarted = true;
        }

        if (L1GameManager.isGameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                resume();
            else
                midGamePause();
        }
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked; // lock cursor
        Cursor.visible = false; // hide curser
    }

    void midGamePause()
    {
        pauseMenu.SetActive(true);
        gamePause();
    }

    void gamePause()
    {
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
        Time.timeScale = 0f;
        isGamePaused = true;
    }
}
