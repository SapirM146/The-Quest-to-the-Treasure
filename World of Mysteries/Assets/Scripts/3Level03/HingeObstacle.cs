using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeObstacle : MonoBehaviour
{
    public HingeJoint hinge;
    public float rotationSpeed; // 300, 500, 700, 900, 1000
    public float force; // 1000
    public int damage; // 40
    public PlayerHPScript playerHP; // 40
   

    // Start is called before the first frame update
    void Start()
    {
        JointMotor motor = new JointMotor();
        motor.targetVelocity = rotationSpeed;
        motor.force = force;
        hinge.motor = motor;
        hinge.useMotor = true;
    }

    public void hitPlayer()
    {
        playerHP.takeDamage(damage);
    }


}
