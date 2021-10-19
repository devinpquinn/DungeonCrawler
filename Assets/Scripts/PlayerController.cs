using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum playerState {Body, Light};

    public playerState myState;

    private Rigidbody2D rb_body;

    public float moveSpeed = 5f;
    Vector2 movement;

    void Awake()
    {
        myState = playerState.Body;
        rb_body = transform.Find("BodySprite").gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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
