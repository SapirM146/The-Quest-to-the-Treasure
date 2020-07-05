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
    bool isPreparing;

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
    AudioSource spawnSound;
    PlayerData playerSave;


    private void Awake()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        playerSave = SaveSystem.LoadPlayer();

        //if (player != null && player.Level == currentLevel)
        //{
        //    CurrentStage = player.Stage;

        //    if (CurrentStage == lastStage)
        //        prepareToLastStage();
        //}

        //else
        //{
            playerSave = new PlayerData(SceneManager.GetActiveScene().buildIndex);
            SaveSystem.SavePlayer(playerSave);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        isLevelEnded = false;
        showDamage = false;
        isPreparing = false;
        stageHandler.showNextStage(CurrentStage);
        playerPos = fps.transform;
        playerPos.position = playerDropPointsByStage[CurrentStage].position;
        playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;

        AudioSource[] audios = GetComponents<AudioSource>();
        respawnCountdownSound = audios[0];
        spawnSound = audios[1];

        //prepareToLastStage();

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
                //prepareToNextStage();
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

    void prepareToNextStage()
    {
        //respawnCountdown.Play();
        // wait 3 seconds

        stageHandler.showNextStage(++CurrentStage);

        if (playerSave != null)
        {
            playerSave.Stage = CurrentStage;
            SaveSystem.SavePlayer(playerSave);
        }

        //playerHP.maxHealth = (playerHP.maxHealth / CurrentStage) * (2 * CurrentStage); // bigger player health capacity
        playerHP.heal(playerHP.maxHealth); // fully heal the player
        playerPos.position = playerDropPointsByStage[CurrentStage].position;
        playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;
        //playerPos.rotation = Quaternion.identity;
        fps.cam.transform.rotation = Quaternion.identity;

        if (CurrentStage == lastStage)
        {
            pauseMenu.gamePause();
            prepareToLastStage();
        }

        isPreparing = false;
    }

    IEnumerator prepareToNextStageWithSound()
    {
        respawnCountdownSound.Play();
        //playerPos.position = playerDropPointsByStage[CurrentStage+1].position;
        //playerPos.rotation = playerDropPointsByStage[CurrentStage+1].rotation;
        yield return new WaitForSeconds(3f); // wait 3 seconds

        stageHandler.showNextStage(++CurrentStage);

        if (playerSave != null)
        {
            playerSave.Stage = CurrentStage;
            SaveSystem.SavePlayer(playerSave);
        }

        fps.mouseLook.noAutoRotate = true;
        //playerHP.maxHealth = (playerHP.maxHealth / CurrentStage) * (2 * CurrentStage); // bigger player health capacity
        playerHP.heal(playerHP.maxHealth); // fully heal the player
        playerPos.position = playerDropPointsByStage[CurrentStage].position;
        playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;
        fps.cam.transform.rotation = playerDropPointsByStage[CurrentStage].rotation;
        yield return new WaitForSeconds(0.01f);

        //playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;
        //spawnSound.Play();
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
        loseMenu.GetComponent<AudioSource>().Play();
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
        damageToEnemyCanvas.SetActive(true); // show the damage canvas
        showDamageCoroutine = StartCoroutine(hideDamageOnScreen());
    }

    IEnumerator hideDamageOnScreen()
    {
        showDamage = false;

        yield return new WaitForSeconds(2f);

        if (!showDamage)
            damageToEnemyCanvas.SetActive(false); // hide the damage canvas
    }

    void saveProgress()
    {

    }

}
