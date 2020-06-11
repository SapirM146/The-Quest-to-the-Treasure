using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenScript : MonoBehaviour
{
    public AudioClip chestClip;
    public Transform treasureChestBox;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(chestClip, transform.position);
            treasureChestBox.GetComponent<Animation>().Play();
            Destroy(gameObject);
        }
    }
}
