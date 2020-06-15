using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPScript : MonoBehaviour
{
    public int maxHealth = 100;

    public int CurrentHealth { get; private set; }
    public HealthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthBar.setHealth(CurrentHealth);
    }
}
