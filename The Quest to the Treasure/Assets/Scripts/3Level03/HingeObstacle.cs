using UnityEngine;

public class HingeObstacle : MonoBehaviour
{
    public HingeJoint hinge;
    public float rotationSpeed; 
    public float force; 
    public int damage; 
    public PlayerHPScript playerHP;
   

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
