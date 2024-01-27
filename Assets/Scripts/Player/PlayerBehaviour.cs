using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 2.0f;
    private float Move;

    private Rigidbody2D rb;

    //public AudioSource quack;

    //public Animator animator;

    public Transform headAttackPoint, handAttackPoint;
    public LayerMask enemyLayers;

    public float headAttackingRange = 0.5f;
    public float handAttackingRange = 0.5f;
    public int headAttackDamage = 10;
    public int handAttackDamage = 10;
    public float headAttackRate = 2f;
    public float handAttackRate = 2f;
    float nextAttackTime = 0f;

    public int headAttackStaminaCost = 10;
    public int handAttackStaminaCost = 10;

    public HealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2 (Move * speed, rb.velocity.y);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    quack.Play();
        //}
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
            {
                if (healthSystem.currentStamina >= headAttackStaminaCost)
                {
                    HeadAttack();
                    nextAttackTime = Time.time + 1f / headAttackRate;
                }
            }
            else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
            {
                if (healthSystem.currentStamina >= handAttackStaminaCost)
                {
                    HandAttack();
                    nextAttackTime = Time.time + 1f / headAttackRate;
                }
            }
        }
        
    }

    void HeadAttack()
    {
        
            healthSystem.UsedStamina(headAttackStaminaCost);
            //animator.SetTrigger("headAttack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(headAttackPoint.position, headAttackingRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit enemy by head");
                enemy.GetComponent<HealthSystem>().TakeDamage(headAttackDamage);
            }

        
    }

    void HandAttack()
    {
        
            healthSystem.UsedStamina(handAttackStaminaCost);
            //animator.SetTrigger("handAttack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(handAttackPoint.position, handAttackingRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit enemy by hand");
                enemy.GetComponent<HealthSystem>().TakeDamage(handAttackDamage);
            }

        
    }

    void OnDrawGizmosSelected()
    {
        if(handAttackPoint == null || headAttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(headAttackPoint.position, headAttackingRange);
        Gizmos.DrawWireSphere(handAttackPoint.position, handAttackingRange);
    }
}
