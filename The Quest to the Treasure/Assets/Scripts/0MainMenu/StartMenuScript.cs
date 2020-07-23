using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public GameObject overwriteWarningCanvas;
    public Button playButton;
    public Button selectLevelButton;
    public Button[] selectStageButtons;
    int goToLevel;
    PlayerData playerSave;

    private void Awake()
    {
        goToLevel = SceneManager.GetActiveScene().buildIndex + 1;
        playerSave = SaveSystem.LoadPlayer();
    }

    private void Update()
    {
        if (!selectLevelButton.interactable && playerSave != null && playerSave.isGameFinished && playButton.interactable)
            selectLevelButton.interactable = true;
    }

    void PlayLevel()
    {
        SceneManager.LoadScene(goToLevel);
    }

    public void Play() 
    {
        goToLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (playerSave != null)
        {
            overwriteWarningCanvas.SetActive(true);
        }
        else
            PlayLevel();
    }

    public void StartNewGame() // overwrite the old save
    {
        playerSave = new PlayerData(goToLevel);
        SaveSystem.SavePlayer(playerSave);
        PlayLevel();
    }

    public void Continue()
    {
        if (playerSave != null)
            goToLevel = playerSave.Level;

        PlayLevel();
    }

    public void SelectLevel1()
    {
        SelectLevel(goToLevel);
    }

    public void SelectLevel2()
    {
        goToLevel += 1;
        SelectLevel(goToLevel);
    }

    public void SelectLevel3()
    {
        goToLevel += 2;

        foreach (Button button in selectStageButtons)
        {
            button.interactable = true;
        }
    }

    public void SelectStage1()
    {
        SelectLevel(goToLevel, 0);
    }

    public void SelectStage2()
    {
        SelectLevel(goToLevel, 1);
    }

    public void SelectStage3()
    {
        SelectLevel(goToLevel, 2);
    }

    public void SelectStage4()
    {
        SelectLevel(goToLevel, 3);
    }

    void SelectLevel(int level, int stage = 0)
    {
        playerSave.Level = level;
        playerSave.Stage = stage;
        SaveSystem.SavePlayer(playerSave);
        PlayLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
