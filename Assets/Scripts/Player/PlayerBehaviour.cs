using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 2.0f;
    private float Move;

    private Rigidbody2D rb;

    //public AudioSource quack;

    public Animator animator;

    public Transform headAttackPoint, handAttackPoint;
    public LayerMask enemyLayers;

    public float headAttackingRange = 0.5f;
    public float handAttackingRange = 0.5f;
    public int headAttackDamage = 10;
    public int handAttackDamage = 10;
    public float headAttackRate = 6f;
    public float handAttackRate = 6f;
    float nextAttackTime = 0f;

    public int headAttackStaminaCost = 10;
    public int handAttackStaminaCost = 10;

    public HealthSystem healthSystem;

    public float stunChance = 0.2f;

    private bool isStunned = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            Move = Input.GetAxis("Horizontal");
            Debug.Log(Move);
            rb.velocity = new Vector2(Move * speed, rb.velocity.y);

            animator.SetBool("IsWalking", Mathf.Abs(Move) > 0.1f);

            if (Move > 0.1f) // Moving to the right
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (Move < -0.1f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    quack.Play();
            //}
            if (Time.time >= nextAttackTime)
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
    }

    void HeadAttack()
    {
        healthSystem.UsedStamina(headAttackStaminaCost);
        animator.SetTrigger("HeadAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(headAttackPoint.position, headAttackingRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit enemy by head");
            enemy.GetComponent<HealthSystem>().TakeDamage(headAttackDamage);
        }

        float stunDuration = 1.0f;

        if (Random.value < stunChance)
        {
            StunPlayer(stunDuration);
        }
    }

    void HandAttack()
    {
        healthSystem.UsedStamina(handAttackStaminaCost);
        animator.SetTrigger("Slap");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(handAttackPoint.position, handAttackingRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit enemy by hand");
            enemy.GetComponent<HealthSystem>().TakeDamage(handAttackDamage);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (handAttackPoint == null || headAttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(headAttackPoint.position, headAttackingRange);
        Gizmos.DrawWireSphere(handAttackPoint.position, handAttackingRange);
    }


    void StunPlayer(float stunDuration)
    {
        if (!isStunned)
        {
            isStunned = true;
            animator.SetTrigger("Stun");

            StartCoroutine(StunCooldown(stunDuration));
        }
    }

    IEnumerator StunCooldown(float stunDuration)
    {
        yield return new WaitForSeconds(stunDuration);

        // After stun duration, resume normal controls
        isStunned = false;
    }
}