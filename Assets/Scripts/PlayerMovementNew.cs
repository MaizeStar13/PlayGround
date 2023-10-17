using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float JumpHeight = 16f;

    private int AirCount;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded;
    //Animator animator;

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //fungsi loncat
        if (Input.GetKeyDown(KeyCode.Space) && AirCount <= 1)
        {
            Vector2 direction = new Vector2(0, 1);
            rb.velocity = direction * JumpHeight;
            AirCount += 1;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            AirCount = 0;
            isGrounded = true;
            Debug.Log("isGrounded");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = false;
            Debug.Log("no");
        }
    }
}

