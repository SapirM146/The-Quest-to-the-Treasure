using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    Animator animator;
    CapsuleCollider capsuleCollider;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarScript healthBar;
    public bool isAlive;
    [HideInInspector]
    public Transform damageOrigin;


    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    public void takeDamage(int damage, Transform origin = null)
    {
        if (animator != null)
            animator.SetTrigger("GetHit");

        currentHealth -= damage;
        healthBar.setHealth(currentHealth);

        if(origin != null)
            damageOrigin = origin;
        
        if (currentHealth <= 0)
            die();
    }

    void die()
    {
        isAlive = false;
        capsuleCollider.enabled = false;

        if (animator != null)
            animator.SetTrigger("Dead");

        Destroy(gameObject, 3f);
    }
}
