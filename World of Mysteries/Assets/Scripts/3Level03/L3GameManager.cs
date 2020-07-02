using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class L3GameManager : MonoBehaviour
{
    //public int CurrentStage {get; private set;}
    public int CurrentStage;
    readonly int lastStage = 3;
    bool isLevelEnded;
    bool showDamage;

    public L3StageHandler stageHandler;
    public PlayerHPScript playerHP;
    public RigidbodyFirstPersonController fps;
    Transform playerPos;
    public Transform[] playerDropPointsByStage;
    [Space] [Space]
    public L3PauseMenuScript pauseMenu;
    public GameObject loseMenu;
    public GameObject damageCanvas;
    public Text damageText;
    Coroutine showDamageCoroutine;
    public Text stageText;
    public Text enemiesLeftText;
    PlayerData player;


    private void Awake()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        player = SaveSystem.LoadPlayer();

        if (player != null && player.Level == currentLevel)
        {
            CurrentStage = player.Stage;

            if (CurrentStage == lastStage)
                prepareToLastStage();
        }

        else
        {
            player = new PlayerData(SceneManager.GetActiveScene().buildIndex);
            SaveSystem.SavePlayer(player);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isLevelEnded = false;
        showDamage = false;
        stageHandler.showNextStage(CurrentStage);
        playerPos = fps.gameObject.transform;
        playerPos.position = playerDropPointsByStage[CurrentStage].position;
        playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLevelEnded)
        {
            int hp = playerHP.CurrentHealth;
            int numOfEnemiesLeft = stageHandler.checkNumOfEnemiesInStage();

            updatePanel(numOfEnemiesLeft, ref enemiesLeftText);
            updatePanel(CurrentStage + 1, ref stageText, lastStage + 1);

            if (hp <= 0)
            {
                isLevelEnded = true;
                StartCoroutine(loseLevel());
            }

            else if (CurrentStage + 1 <= lastStage && numOfEnemiesLeft == 0)
            {
                stageHandler.showNextStage(++CurrentStage);
                
                if (player != null)
                {
                    player.Stage = CurrentStage;
                    SaveSystem.SavePlayer(player);
                }

                playerHP.heal(playerHP.maxHealth); // heal the player each stage
                playerPos.position = playerDropPointsByStage[CurrentStage].position;
                playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;

                if (CurrentStage == lastStage)
                {
                    pauseMenu.gamePause();
                    prepareToLastStage();
                }

                
                
            }

            else if (CurrentStage == lastStage && numOfEnemiesLeft == 0)
            {
                isLevelEnded = true;
                StartCoroutine(winLevel());
            }
        }
    }

    void prepareToLastStage()
    {
        pauseMenu.isLastStage = true;
        pauseMenu.isGameStarted = false;
        pauseMenu.instructionsPanel.SetActive(false);
        pauseMenu.LastBattleInstructionsPanel.SetActive(true);
        fps.movementSettings.JumpForce = 500;
    }

    void updatePanel(int currentNum, ref Text textPanel, int targetNum = -1)
    {
        textPanel.text = (targetNum != -1) ? (currentNum + " / " + targetNum) : currentNum.ToString();   
    }

    IEnumerator loseLevel()
    {
        pauseMenu.enabled = false;

        yield return new WaitForSeconds(0.5f);

        fps.enabled = false;
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
        Time.timeScale = 0f; // stop time
        loseMenu.SetActive(true); // show lose menu
    }

    IEnumerator winLevel()
    {
        pauseMenu.enabled = false;

        yield return new WaitForSeconds(1f);

        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
        //Time.timeScale = 0f; // stop time        
        GoToWinScene();
    }

    void GoToWinScene()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void showDamageOnScreen(int amount)
    {
        if(showDamageCoroutine != null)
            StopCoroutine(showDamageCoroutine);
        showDamage = true;
        damageText.text = "-" + amount; // update damage text (-10 or -20) in red
        damageCanvas.SetActive(true); // show the damage canvas
        showDamageCoroutine = StartCoroutine(hideDamageOnScreen());
    }

    IEnumerator hideDamageOnScreen()
    {
        showDamage = false;

        yield return new WaitForSeconds(2f);

        if (!showDamage)
            damageCanvas.SetActive(false); // hide the damage canvas
    }

    void saveProgress()
    {

    }

}
