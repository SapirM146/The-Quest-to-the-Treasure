using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class SwitchModeScript : MonoBehaviour
{
    public GameObject boat;
    public GameObject boatCamera;

    AutoCam autoCam;
    FreeLookCam freeCam;
    Rigidbody boatRB;

    public GameObject fps;
    public GameObject fpsStartPosition;

    public static bool isBoatMode;
    public static bool isFreeCamMode;

    private void Start()
    {
        isBoatMode = true;
        isFreeCamMode = false;
        autoCam = boatCamera.GetComponent<AutoCam>();
        freeCam = boatCamera.GetComponent<FreeLookCam>();
        boatRB = boat.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Boat Mode
        if (Input.GetKeyUp("1"))
        {
            boat.GetComponent<CapsuleCollider>().isTrigger = false;
            boatRB.isKinematic = false;
            boat.GetComponent<BoatMotionScript>().enabled = true;
            boatCamera.SetActive(true);
            fps.SetActive(false);
            isBoatMode = true;
        }

        // FPS Mode
        if (Input.GetKeyUp("2"))
        {
            boat.GetComponent<CapsuleCollider>().isTrigger = true;
            boatRB.isKinematic = true;
            boat.GetComponent<BoatMotionScript>().enabled = false;
            boatCamera.SetActive(false);
            fps.transform.position = fpsStartPosition.transform.position;
            fps.SetActive(true);
            isBoatMode = false;
        }

        if (isBoatMode && Input.GetKeyUp(KeyCode.C))
        {
            isFreeCamMode = !isFreeCamMode;
            autoCam.enabled = !isFreeCamMode;
            freeCam.enabled = isFreeCamMode;
        }
    }
}
