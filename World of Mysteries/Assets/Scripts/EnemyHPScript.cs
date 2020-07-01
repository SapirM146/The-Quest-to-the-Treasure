using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    Animator animator;
    CapsuleCollider capsuleCollider;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarScript healthBar;
    public bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {       
        if (currentHealth > 0)
        {
            if (animator != null)
                animator.SetTrigger("GetHit");

            currentHealth -= damage;
            healthBar.setHealth(currentHealth);
        }

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
