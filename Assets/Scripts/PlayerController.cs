using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum playerState {Body, Light};

    public playerState myState;

    private Transform body;
    private SpriteRenderer bodySprite;
    private Rigidbody2D rb_body;
    private Animator rb_anim;

    public float moveSpeed = 5f;
    Vector2 movement;

    void Awake()
    {
        myState = playerState.Body;
        body = transform.Find("Body");
        bodySprite = body.GetComponent<SpriteRenderer>();
        rb_body = body.GetComponent<Rigidbody2D>();
        rb_anim = body.GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(myState == playerState.Body)
        {
            rb_anim.SetFloat("Speed", movement.sqrMagnitude);
            if(movement.x < 0)
            {
                bodySprite.flipX = false;
            }
            else if (movement.x > 0)
            {
                bodySprite.flipX = true;
            }
        }

        if(movement.x != 0 && movement.y != 0) 
        {
            movement.x /= 2;
            movement.y /= 2;
        }
    }

    void FixedUpdate()
    {
        if(myState == playerState.Body)
        {
            rb_body.MovePosition(rb_body.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
