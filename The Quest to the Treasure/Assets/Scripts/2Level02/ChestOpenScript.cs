using UnityEngine;

public class ChestOpenScript : MonoBehaviour
{
    public AudioClip chestClip;
    public Transform treasureChestBox;
    public L2GameManagerScript gm;
    bool isOpen;


    private void Start()
    {
        isOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isOpen && other.gameObject.tag == "FPS")
        {
            isOpen = true;
            AudioSource.PlayClipAtPoint(chestClip, transform.position);
            treasureChestBox.GetComponent<Animation>().Play();
            gm.foundChest();
        }
    }
}
