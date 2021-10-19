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
    private Animator body_anim;

    private Animator head_anim;
    private Transform head_pointer;

    public float moveSpeed = 5f;
    Vector2 movement;

    void Awake()
    {
        myState = playerState.Body;
        body = transform.Find("Body");
        bodySprite = body.GetComponent<SpriteRenderer>();
        rb_body = body.GetComponent<Rigidbody2D>();
        body_anim = body.GetComponent<Animator>();

        head_anim = body.transform.Find("Head").GetComponent<Animator>();
        head_pointer = head_anim.gameObject.transform.Find("Pointer");
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(myState == playerState.Body)
        {
            body_anim.SetFloat("Speed", movement.sqrMagnitude);
            if(movement.x < 0)
            {
                bodySprite.flipX = false;
            }
            else if (movement.x > 0)
            {
                bodySprite.flipX = true;
            }

            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - head_pointer.transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
            Debug.Log(head_pointer.transform.eulerAngles.z);
        }

        
    }

    void FixedUpdate()
    {
        if(myState == playerState.Body)
        {
            rb_body.MovePosition(rb_body.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
