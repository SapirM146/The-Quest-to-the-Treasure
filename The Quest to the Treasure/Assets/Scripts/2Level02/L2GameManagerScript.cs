using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L2GameManagerScript : MonoBehaviour
{
    public static PlayerHPScript playerHP;
    public L2PauseMenuScript pauseMenu;
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject GPS_Arrow;
    public Text ChestsCollectedText;
    public Text EnemiesDefeatedText;
    public GameObject LastObjectiveCanvas;


    int numOfChestsFound = 0;
    readonly int numOfChests = 3;
    int numOfEnemies;
    int numOfEnemiesLeft;

    bool isLevelEnded;
    bool allChestFounded;
    bool allEnemiesDefeated; 
    public static bool playerInEndZone;

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

    private void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHPScript>();
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        numOfEnemiesLeft = numOfEnemies;
        isLevelEnded = false;
        allChestFounded = false;
        allEnemiesDefeated = false;
        playerInEndZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLevelEnded)
        {
            updateNumOfEnemiesLeft();
            updatePanel(ref numOfChestsFound, allChestFounded, ref ChestsCollectedText, numOfChests);
            updatePanel(ref numOfEnemiesLeft, allEnemiesDefeated, ref EnemiesDefeatedText);

            int hp = playerHP.CurrentHealth;

            if (hp <= 0)
            {
                isLevelEnded = true;
                StartCoroutine(loseLevel());
            }

            else if (playerInEndZone && allEnemiesDefeated && allChestFounded)
            {
                isLevelEnded = true;
                StartCoroutine(winLevel());
            }

            else if (!GPS_Arrow.activeInHierarchy && allEnemiesDefeated && allChestFounded)
            {
                GPS_Arrow.SetActive(true);
                LastObjectiveCanvas.SetActive(true);
            }
        }
    }

    public void foundChest()
    {
        numOfChestsFound++;
        if (numOfChestsFound == numOfChests)
            allChestFounded = true;
    }

    void updateNumOfEnemiesLeft()
    {
        if(!allEnemiesDefeated)
        {
            int temp = GameObject.FindGameObjectsWithTag("Enemy").Length;
            
            if (temp != numOfEnemies)
                numOfEnemiesLeft = temp;
            
            if (numOfEnemiesLeft == 0)
            {
                allEnemiesDefeated = true;
            }         
        } 
    }

    void updatePanel(ref int currentNum, bool isDone, ref Text textPanel, int targetNum = -1)
    {
        string temptext;
        if (targetNum != -1)
        {
            temptext = currentNum + " / " + targetNum;
        }
        else
        {
            temptext = currentNum.ToString();
        }

        if (isDone)
        {
            temptext += " DONE!";
            textPanel.color = Color.green;
        }

        textPanel.text = temptext;
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
        winMenu.SetActive(true); // show win menu

    }

    public void GoToNextLevel()
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
}
