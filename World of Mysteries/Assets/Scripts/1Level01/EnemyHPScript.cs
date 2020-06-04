using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPScript : MonoBehaviour
{
    Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarScript healthBar;
    public bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            animator.Play("Get_Hit");
            currentHealth -= damage;
            healthBar.setHealth(currentHealth);
        }

        if (currentHealth <= 0)
            die();
    }


    void die()
    {
        isAlive = false;
        animator.Play("Dead");
        Destroy(gameObject, 5f);
    }
}
