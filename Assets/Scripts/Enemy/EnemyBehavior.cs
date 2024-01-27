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

    private bool onGround;

    private void Start()
    {
        onGround = false;

        selfRb = GetComponent<Rigidbody2D>();
        selfcollider = GetComponent<BoxCollider2D>();

        target = GameObject.FindGameObjectWithTag("Player");
        targetTr = target.transform;
    }

    private void Update()
    {
        if(target != null)
        {
            if (onGround)
            {
                float distance = transform.position.x - targetTr.position.x;

                if (distance < attackDistance && distance > -attackDistance)
                {
                    selfRb.velocity = new Vector3(0, 0);

                    Physics2D.IgnoreCollision(target.GetComponent<BoxCollider2D>(), selfcollider , true);

                    Debug.Log("Attack");
                }
                else
                {
                    if (transform.position.x > targetTr.position.x)
                    {
                        selfRb.velocity = new Vector3(-speed, selfRb.velocity.y);
                    }
                    else
                    {
                        selfRb.velocity = new Vector3(speed, selfRb.velocity.y);
                    }

                    Physics2D.IgnoreCollision(target.GetComponent<BoxCollider2D>(), selfcollider, false);
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
}
