using UnityEngine;

public class GpsArrowScript : MonoBehaviour
{
    public Transform endPoint;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(endPoint);
    }
}
