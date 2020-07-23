using UnityEngine;

public class EnemyBoatShottingScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletStartPos;
    EnemyBoatMotionScript motion;
    EnemyHPScript hp;

    readonly float shootTimeConstant = 5f;
    float shootTimer;
    public bool shootFlag;


    private void Start()
    {
        motion = GetComponent<EnemyBoatMotionScript>();
        hp = GetComponent<EnemyHPScript>();
        shootFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hp.isAlive)
        {
            if (shootFlag)
            {
                if (shootTimer > 0f)
                    shootTimer -= Time.deltaTime;

                else
                    shootFlag = false;
            }

            if (motion.isPlayerDetected && !shootFlag)
            {
                shootTimer = shootTimeConstant;
                GameObject b = Instantiate(bullet, bulletStartPos.position, Quaternion.identity);
                b.GetComponent<BulletScript>().origin = gameObject;
                shootFlag = true;
            }
        }
        else
        {
            this.enabled = false;
        }
    }

}
