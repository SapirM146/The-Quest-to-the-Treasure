using UnityEngine;

public class Bowl_PowerUp : MonoBehaviour
{
    PlayerData playerSave;

    private void Start()
    {
        playerSave = SaveSystem.LoadPlayer();
        if(playerSave != null)
            gameObject.SetActive(!playerSave.colletedPowerUp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(playerSave != null)
        {
            playerSave.colletedPowerUp = true;
        }

        SaveSystem.SavePlayer(playerSave);
        gameObject.SetActive(false);
    }
}
