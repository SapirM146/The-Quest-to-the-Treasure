using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class L4PauseMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public RigidbodyFirstPersonController fps;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (isGamePaused)
                resume();
            else
                pause();
        }
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        fps.enabled = true;
    }
    
    public void pause()
    {
        fps.enabled = false;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
