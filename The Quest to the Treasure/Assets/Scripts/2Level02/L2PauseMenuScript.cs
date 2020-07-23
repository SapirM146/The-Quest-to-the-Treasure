using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class L2PauseMenuScript : MonoBehaviour
{
    public static bool isGamePaused;
    public static bool isGameStarted;

    public GameObject instructionPanel;
    public GameObject pauseMenu;
    public GameObject ChestsCollectedPanel;
    public GameObject EnemiesDefeatedPanel;
    public FirstPersonController fps;
    BoatShottingScript shootingAbility;
    SwitchModeScript modeSwitch;


    private void Start()
    {
        isGamePaused = false;
        isGameStarted = false;
        shootingAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatShottingScript>();
        modeSwitch = GetComponent<SwitchModeScript>();
        gamePause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetComponent<AudioSource>().Play();
            instructionPanel.SetActive(false);
            ChestsCollectedPanel.SetActive(true);
            EnemiesDefeatedPanel.SetActive(true);
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
        shootingAbility.enabled = true;
        modeSwitch.enabled = true;
    }
    
    void midGamePause()
    {
        fps.enabled = false;
        pauseMenu.SetActive(true);
        gamePause();
    }

    void gamePause()
    {
        modeSwitch.enabled = false;
        shootingAbility.enabled = false;
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
        Time.timeScale = 0f;
        isGamePaused = true;
    }

}
