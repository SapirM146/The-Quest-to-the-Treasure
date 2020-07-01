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
    bool isLevelEnded = false;
    bool showDamage = false;

    public L3StageHandler enemiesShowHide;
    public PlayerHPScript playerHP;
    public RigidbodyFirstPersonController fps;
    Transform playerPos;
    public Transform[] playerDropPointsByStage;
    [Space] [Space]
    public L2PauseMenuScript pauseMenu;
    public GameObject loseMenu;
    //public GameObject winMenu;
    public GameObject damageCanvas;
    public Text damageText;
    Coroutine showDamageCoroutine;



    // Start is called before the first frame update
    void Start()
    {
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
            int numOfEnemies = enemiesShowHide.checkNumOfEnemiesInStage();

            if (hp <= 0)
            {
                isLevelEnded = true;
                RestartLevel();
                //StartCoroutine(loseLevel());
            }

            else if (CurrentStage + 1 <= lastStage && numOfEnemies == 0)
            {
                enemiesShowHide.showNextStage(++CurrentStage);
                Debug.Log(CurrentStage);
                playerHP.heal(playerHP.maxHealth); // heal the player each stage
                playerPos.position = playerDropPointsByStage[CurrentStage].position;
                playerPos.rotation = playerDropPointsByStage[CurrentStage].rotation;

                if (CurrentStage == lastStage)
                    fps.movementSettings.JumpForce = 500;
            }

            else if (CurrentStage == lastStage && numOfEnemies == 0)
            {
                isLevelEnded = true;
                StartCoroutine(winLevel());
            }
        }
    }

    IEnumerator loseLevel()
    {
        pauseMenu.enabled = false;

        // show lose message


        yield return new WaitForSeconds(0.5f);

        //fps.enabled = false;
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
        Time.timeScale = 0f; // stop time
        
        //winMenu.SetActive(true); // show win menu
        
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

}
