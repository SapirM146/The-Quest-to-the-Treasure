using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenuScript : MonoBehaviour
{
    public void PlayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Continue()
    {
        int level = 1;
        PlayerData player = SaveSystem.LoadPlayer();

        if (player != null)
            level = player.Level;

        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
