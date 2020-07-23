using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class L3PauseMenuScript : MonoBehaviour
{
    public bool isGamePaused { get; set; }
    public bool isGameStarted { get; set; }
    public bool isLastStage { get; set; }

    public GameObject instructionsPanel;
    public GameObject LastBattleInstructionsPanel;
    public GameObject pauseMenu;
    public GameObject StagePanel;
    public GameObject EnemiesDefeatedPanel;
    public GameObject GrenadeAmountPanel;
    public RigidbodyFirstPersonController fps;


    private void Start()
    {
        gamePause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isGameStarted)
        {
            if (!isLastStage)
            {
                instructionsPanel.SetActive(false);
            }

            else // last stage
            {
                LastBattleInstructionsPanel.SetActive(false);
            }

            StagePanel.SetActive(true);
            EnemiesDefeatedPanel.SetActive(true);
            GrenadeAmountPanel.SetActive(true);
            resume();
            isGameStarted = true;

        }

        if (isGameStarted && Input.GetKeyDown(KeyCode.Escape))
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
        fps.enabled = true;
    }

    void midGamePause()
    {
        fps.enabled = false;
        pauseMenu.SetActive(true);
        gamePause();
    }

    public void gamePause()
    {
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
        Time.timeScale = 0f;
        isGamePaused = true;
    }
}
