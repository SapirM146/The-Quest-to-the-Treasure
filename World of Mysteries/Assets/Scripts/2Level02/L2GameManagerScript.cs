using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L2GameManagerScript : MonoBehaviour
{
    //public Image hp_bar;
    public static PlayerHPScript playerHP;
    public L2PauseMenuScript pauseMenu;
    public GameObject loseMenu;

    int numOfChestsFound = 0;
    readonly int numOfChests = 3;
    int numOfEnemies;

    bool isLevelEnded = false;
    bool allChestFounded = false;
    bool allEnemiesDefeted = false;
    public static bool playerInEndZone = false;

    private void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHPScript>();
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLevelEnded)
        {
            int temp = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (!allEnemiesDefeted && temp != numOfEnemies)
                numOfEnemies = temp;

            int hp = playerHP.CurrentHealth;
            //hp_bar.fillAmount = hp;

            if (hp <= 0)
            {
                isLevelEnded = true;
                StartCoroutine(loseLevel());
            }

            else if (playerInEndZone && allEnemiesDefeted && allChestFounded)
            {
                isLevelEnded = true;
                StartCoroutine(winLevel());
            }
        }
    }

    public void foundChest()
    {
        numOfChestsFound++;
        if (numOfChestsFound == numOfChests)
            allChestFounded = true;
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
        // show lose message
        Debug.Log("Win");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
