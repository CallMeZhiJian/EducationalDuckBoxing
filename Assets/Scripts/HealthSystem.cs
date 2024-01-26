using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Player,
    Enemy
}
public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxStamina = 100;
    public int currentStamina;

    public int healthRegenRate ;
    public int staminaRegenRate = 1;

    public Type type = Type.Enemy;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        if (type == Type.Player)
        {
            currentStamina = maxStamina;
            InvokeRepeating("RegenerateStamina", 1f, 1f);
        }
            
    }
    void RegenerateStamina()
    {
        if (type == Type.Player)
        {
            currentStamina += staminaRegenRate;

            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }
    void RegenerateHealth()
    { 
        if (type == Type.Player)
        {
            currentStamina += healthRegenRate;

            currentStamina = Mathf.Min(currentHealth, maxHealth);
        }
    }
    public void UsedStamina(int consumed)
    {
        if (type == Type.Player )
        {
            currentStamina -= consumed;
        }  
    }

    public void TakeDamage(int damage)
    {
        if (type == Type.Player || type == Type.Enemy)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }  
    }

    void Die()
    {
        Debug.Log("Died");
    }
}
