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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            HeadAttack();
        }
        else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
        {
            HandAttack();
        }
    }

    void HeadAttack()
    {
        //animator.SetTrigger("headAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(headAttackPoint.position, headAttackingRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit enemy by head");
            enemy.GetComponent<HealthSystem>().TakeDamage(headAttackDamage);
        }    
    }

    void HandAttack()
    {
        //animator.SetTrigger("handAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(handAttackPoint.position, handAttackingRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
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
