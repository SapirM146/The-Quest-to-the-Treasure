using UnityEngine;
using UnityEngine.UI;

public class PlayerCollect : MonoBehaviour
{
    public Text coinsCollectedText;
    public Text gemsCollectedText;
    public GameObject findCoinsGemsText;
    public GameObject findMapKeyText;
    public GameObject CoinsCollectedPanel;
    public GameObject GemsCollectedPanel;

    public GameObject map;
    public GameObject key;

    public AudioClip coinCollectSound;
    public AudioClip gemCollectSound;
    public AudioClip mapCollectSound;
    public AudioClip keyCollectSound;

    public bool AllTasksComplete { get; private set; }
    bool allCoinsCollected;
    bool allGemsCollected;
    bool mapCollected;
    bool keyCollected;

    int numOfCoinsCollected = 0;
    readonly int numOfCoins = 7;
    int numOfGemsCollected = 0;
    readonly int numOfGems = 7;


    // Start is called before the first frame update
    void Start()
    {
        allCoinsCollected = false;
        allGemsCollected = false;
        mapCollected = false;
        keyCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!L1GameManager.LevelComplete)
        {
            if (coinsCollectedText.gameObject.activeInHierarchy && gemsCollectedText.gameObject.activeInHierarchy)
            {
                updatePanel(ref numOfCoinsCollected, allCoinsCollected, ref coinsCollectedText, numOfCoins);
                updatePanel(ref numOfGemsCollected, allGemsCollected, ref gemsCollectedText, numOfGems);
            }

            if (!keyCollected && !mapCollected && !map.activeInHierarchy && allCoinsCollected && allGemsCollected)
            {
                findCoinsGemsText.SetActive(false);
                showKeyAndMap();
            }

            else if (!AllTasksComplete && allCoinsCollected && allGemsCollected && mapCollected && keyCollected)
            {
                findMapKeyText.SetActive(false);
                AllTasksComplete = true;
            }
        }
    }

    public void collectCoin()
    {
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
        numOfCoinsCollected++;
        if (numOfCoinsCollected == numOfCoins)
            allCoinsCollected = true;
    }

    public void collectGem()
    {
        AudioSource.PlayClipAtPoint(gemCollectSound, transform.position);
        numOfGemsCollected++;
        if (numOfGemsCollected == numOfGems)
            allGemsCollected = true;
    }

    public void collectMap()
    {
        AudioSource.PlayClipAtPoint(mapCollectSound, transform.position);
        mapCollected = true;
    }

    public void collectKey()
    {
        AudioSource.PlayClipAtPoint(keyCollectSound, transform.position);
        keyCollected = true;
    }

    void showKeyAndMap()
    {
        key.SetActive(true);
        map.SetActive(true);
        findMapKeyText.SetActive(true);
    }

    public void showCollectPanels(bool flag)
    {
        CoinsCollectedPanel.SetActive(flag);
        GemsCollectedPanel.SetActive(flag);
        findCoinsGemsText.SetActive(flag);
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
}
