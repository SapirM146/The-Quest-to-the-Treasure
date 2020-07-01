using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class L4GameManagerScript : MonoBehaviour
{
    public PlayerHPScript playerHP;
    public GameObject loseMenu;
    public L4PauseMenuScript pauseMenu;
    bool isLevelEnded = false;
    int numOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLevelEnded)
        {
            numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (playerHP.CurrentHealth <= 0)
            {
                isLevelEnded = true;
                StartCoroutine(loseLevel());
            }

            else if (numOfEnemies == 0)
            {
                isLevelEnded = true;
                StartCoroutine(winLevel());
            }
        }
            
    }

    IEnumerator winLevel()
    {
        // show lose message
        Debug.Log("Win");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
