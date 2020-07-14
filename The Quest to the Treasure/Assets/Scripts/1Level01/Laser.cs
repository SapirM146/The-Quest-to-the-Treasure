using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer laser;
    public CapsuleCollider2D collider2d;
    public Transform StartPoint;
    public Transform endPoint;

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, StartPoint.position);
        laser.SetPosition(1, endPoint.position);
    }

    public void turnOffLaser()
    {
        laser.enabled = false;
        collider2d.enabled = false;
    }
}
