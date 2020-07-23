using UnityEngine;

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

    public void takeDamage(int damage)
    {
        if (CurrentHealth - damage < 0)
            CurrentHealth = 0;
        else
            CurrentHealth -= damage;

        healthBar.setHealth(CurrentHealth);
    }

    public void heal(int amount)
    {
        if (CurrentHealth + amount > maxHealth)
            CurrentHealth = maxHealth;
        else 
            CurrentHealth += amount;

        healthBar.setHealth(CurrentHealth);
    }
}
