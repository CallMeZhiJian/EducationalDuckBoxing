using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int healthRegenRate = 1;
    public int staminaRegenRate = 1;

    public Type type = Type.Enemy;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        if (type == Type.Player)
        {
            currentStamina = maxStamina;
            InvokeRepeating("RegenerateStamina", 1f, 1f);
            InvokeRepeating("RegenerateHealth", 1f, 2f);
        }
            
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void RegenerateStamina()
    {
        if (type == Type.Player )
        {
            currentStamina += staminaRegenRate;

            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }
    void RegenerateHealth()
    { 
        if (type == Type.Player && currentHealth <= 0)
        {
            currentHealth += healthRegenRate;

            currentHealth = Mathf.Min(currentHealth, maxHealth);
                        
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

        if (type == Type.Player)
        {
            animator.SetBool("IsDead", true);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            Debug.Log("Player Die");
            
        }
        else if (type == Type.Enemy)
        {
            Destroy(gameObject);
        }

    }

}
