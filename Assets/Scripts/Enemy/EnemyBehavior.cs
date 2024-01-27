using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehavior : MonoBehaviour
{
    //Data to take from elsewhere
    [SerializeField] private GameObject target;
    private Transform targetTr;

    //Data for self
    private Rigidbody2D selfRb;
    private BoxCollider2D selfcollider;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float attackDistance = 1.5f;

    public Animator animator;

    public Transform headAttackPoint, handAttackPoint;
    public LayerMask playerLayers;

    public float headAttackingRange = 0.5f;
    public float handAttackingRange = 0.5f;
    public int headAttackDamage = 10;
    public int handAttackDamage = 10;
    public float headAttackRate = 6f;
    public float handAttackRate = 6f;

    public int headAttackStaminaCost = 10;
    public int handAttackStaminaCost = 10;

    public HealthSystem healthSystem;

    private bool onGround;
    private bool attackAllow;
    public bool helmetDuck;

    private void Start()
    {
        onGround = false;
        attackAllow = true;

        selfRb = GetComponent<Rigidbody2D>();
        selfcollider = GetComponent<BoxCollider2D>();

        target = GameObject.Find("Player");
        targetTr = target.transform;
    }

    private void Update()
    {
        if(target != null)
        {
            if (onGround)
            {
                float distance = transform.position.x - targetTr.position.x;
                Debug.Log(distance);
                if (distance < attackDistance && distance > -attackDistance)
                {
                    selfRb.velocity = new Vector3(0, 0);

                    Physics2D.IgnoreCollision(target.GetComponent<BoxCollider2D>(), selfcollider , true);

                    if (attackAllow)
                    {
                        float randomNum = Random.value;
                        
                        if(randomNum > 0.5f)
                        {
                            HeadAttack();
                        }
                        else
                        {
                            if (!helmetDuck)
                            {
                                HandAttack();
                            } 
                        }

                        StartCoroutine(AttackDelay());
                    }

                    Debug.Log("Attack");
                }
                else
                {
                    if (transform.position.x > targetTr.position.x)
                    {
                        selfRb.velocity = new Vector3(-speed, selfRb.velocity.y);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        selfRb.velocity = new Vector3(speed, selfRb.velocity.y);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    //  Physics2D.IgnoreCollision(target.GetComponent<BoxCollider2D>(), selfcollider, false);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void HeadAttack()
    {
        //healthSystem.UsedStamina(headAttackStaminaCost);
        animator.SetTrigger("HeadAttack"); // can change to enemy head attack

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(headAttackPoint.position, headAttackingRange, playerLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit enemy by head");
            enemy.GetComponent<HealthSystem>().TakeDamage(headAttackDamage);
        }

    }

    void HandAttack()
    {
        healthSystem.UsedStamina(handAttackStaminaCost);
        animator.SetTrigger("Slap"); // can change to enemy slap

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(handAttackPoint.position, handAttackingRange, playerLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit enemy by hand");
            enemy.GetComponent<HealthSystem>().TakeDamage(handAttackDamage);
        }

    }

    IEnumerator AttackDelay()
    {
        attackAllow = false;

        yield return new WaitForSeconds(1f);
        attackAllow = true;
    }
}
