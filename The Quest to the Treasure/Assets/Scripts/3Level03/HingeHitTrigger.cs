using UnityEngine;

public class HingeHitTrigger : MonoBehaviour
{
    public HingeObstacle obstacle;
    AudioSource soundeffect;
    bool soundPlayed;

    private void Start()
    {
        soundeffect = GetComponent<AudioSource>();
        soundPlayed = false;
    }

    private void Update()
    {
        if (transform.eulerAngles.z > 300f && transform.eulerAngles.z < 330f)
        {
            if (!soundPlayed)
            {
                soundPlayed = true;
                soundeffect.Play();
            }
        }

        else
            soundPlayed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            obstacle.hitPlayer();

        else
        {
            EnemyHPScript enemy = other.GetComponent<EnemyHPScript>();
            if (enemy != null)
                enemy.takeDamage(enemy.maxHealth);
        }
    }
}
