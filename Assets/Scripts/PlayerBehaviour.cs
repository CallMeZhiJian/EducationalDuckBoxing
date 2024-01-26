using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 2.0f;
    private float Move;

    private Rigidbody2D rb;

    //public AudioSource quack;

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
    }
}
