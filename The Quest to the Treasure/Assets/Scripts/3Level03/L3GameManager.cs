using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class L3GameManager : MonoBehaviour
{
    public int CurrentStage {get; private set;}
    readonly int lastStage = 3;
    bool isLevelEnded;
    bool showDamage;
    bool isPreparing;
    int sumOfDamage;

    public L3StageHandler stageHandler;
    public PlayerHPScript playerHP;
    public RigidbodyFirstPersonController fps;
    Transform playerPos;
    public Transform[] playerDropPointsByStage;
    [Space] [Space]
    public L3PauseMenuScript pauseMenu;
    public GameObject loseMenu;
    public GameObject damageToEnemyCanvas;
    public Text damageText;
    Coroutine showDamageCoroutine;
    public Text stageText;
    public Text enemiesLeftText;
    AudioSource respawnCountdownSound;
    PlayerData playerSave;


    private void Awake()
    {
        playerSave = SaveSystem.LoadPlayer();

        if (CurrentStage == 0)
            loadProgress();
        else
            saveProgress();

        playerPos = fps.transform;
        playerPos.position = playerDropPointsByStage[CurrentStage].position;
        playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        isLevelEnded = false;
        showDamage = false;
        isPreparing = false;
        sumOfDamage = 0;
        stageHandler.showNextStage(CurrentStage);
        respawnCountdownSound = GetComponent<AudioSource>();
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

            else if (CurrentStage + 1 <= lastStage && numOfEnemiesLeft == 0 && !isPreparing)
            {
                isPreparing = true;
                StartCoroutine(prepareToNextStageWithSound());
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

    IEnumerator prepareToNextStageWithSound()
    {
        respawnCountdownSound.Play();

        yield return new WaitForSeconds(3f); // wait 3 seconds

        stageHandler.showNextStage(++CurrentStage);
        saveProgress();

        fps.mouseLook.noAutoRotate = true;
        playerHP.heal(playerHP.maxHealth); // fully heal the player
        playerPos.position = playerDropPointsByStage[CurrentStage].position;
        playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;
        fps.cam.transform.rotation = playerDropPointsByStage[CurrentStage].rotation;

        yield return new WaitForSeconds(Time.unscaledDeltaTime);

        fps.mouseLook.noAutoRotate = false;

        if (CurrentStage == lastStage)
        {
            pauseMenu.gamePause();
            prepareToLastStage();
        }

        isPreparing = false;
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
        saveProgress(true);
        yield return new WaitForSeconds(1f);

        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
        GoToWinScene();
    }

    void GoToWinScene()
    {
        Time.timeScale = 1f;
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
        sumOfDamage += amount;
        damageText.text = "-" + sumOfDamage; // update damage to enemy text
        damageToEnemyCanvas.SetActive(true); // show the damage canvas
        showDamageCoroutine = StartCoroutine(hideDamageOnScreen());
    }

    IEnumerator hideDamageOnScreen()
    {
        showDamage = false;

        yield return new WaitForSeconds(2f);

        if (!showDamage)
        {
            damageToEnemyCanvas.SetActive(false); // hide the damage canvas
            sumOfDamage = 0;
        }
    }

    void loadProgress()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (playerSave != null)
        {
            if(playerSave.Level == currentLevel)
            {
                CurrentStage = playerSave.Stage;

                if (CurrentStage == lastStage)
                    prepareToLastStage();
            }

            else
            {
                playerSave.Level = currentLevel;
                playerSave.Stage = 0;
                SaveSystem.SavePlayer(playerSave);
            }
        }
        else // no save found
        {
            playerSave = new PlayerData(currentLevel);
            SaveSystem.SavePlayer(playerSave);
        }
    }

    void saveProgress(bool isFinished = false)
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (playerSave != null)
        {
            playerSave.Level = currentLevel;
            playerSave.Stage = CurrentStage;
        }
        else // no save found
        {
            playerSave = new PlayerData(currentLevel, CurrentStage);
        }

        if(CurrentStage == lastStage && isFinished)
            playerSave.isGameFinished = true;

        SaveSystem.SavePlayer(playerSave);
    }

}
