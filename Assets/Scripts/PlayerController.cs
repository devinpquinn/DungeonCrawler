using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum playerState {Body, Light, Swapping};

    public playerState myState;

    private Transform body;
    private SpriteRenderer bodySprite;
    private Rigidbody2D rb_body;
    private Animator body_anim;

    private Transform lightHolder;
    private Transform lightBall;
    private Rigidbody2D rb_light;
    private Animator light_anim;

    public GameObject lightRemnant;

    private Animator head_anim;
    private Transform head_pointer;

    public float moveSpeed = 3f;
    public float lightSpeed = 5f;
    public float rotationSpeed = 10f;
    Vector2 movement;

    void Awake()
    {
        myState = playerState.Body;
        body = transform.Find("Body");
        bodySprite = body.GetComponent<SpriteRenderer>();
        rb_body = body.GetComponent<Rigidbody2D>();
        body_anim = body.GetComponent<Animator>();

        lightHolder = body.Find("LightHolder");
        lightBall = lightHolder.Find("LightBall");
        rb_light = lightBall.GetComponent<Rigidbody2D>();
        light_anim = lightBall.GetComponent<Animator>();

        lightBall.gameObject.SetActive(false);

        head_anim = body.transform.Find("Head").GetComponent<Animator>();
        head_pointer = head_anim.gameObject.transform.Find("Pointer");
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(myState == playerState.Body)
        {
            //body movement
            body_anim.SetFloat("Speed", movement.sqrMagnitude);
            if(movement.x < 0)
            {
                if(bodySprite.flipX == true)
                {
                    //flip light holder
                    lightHolder.localPosition = new Vector3(lightHolder.localPosition.x * -1, lightHolder.localPosition.y, lightHolder.localPosition.z);
                }
                bodySprite.flipX = false;
            }
            else if (movement.x > 0)
            {
                if (bodySprite.flipX == false)
                {
                    //flip light holder
                    lightHolder.localPosition = new Vector3(lightHolder.localPosition.x * -1, lightHolder.localPosition.y, lightHolder.localPosition.z);
                }
                bodySprite.flipX = true;
            }

            //turn head to face cursor
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - head_pointer.transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion headRot = Quaternion.Lerp(head_pointer.transform.rotation, Quaternion.Euler(0f, 0f, rotation_z), Time.deltaTime * rotationSpeed);
            head_pointer.transform.rotation = headRot;

            //set head sprite to face cursor
            float angle = 1 - (headRot.eulerAngles.z / 360);
            head_anim.SetFloat("Rotation", angle);

            //check for mode transition
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(myState == playerState.Body)
                {
                    //start transition to light mode
                    myState = playerState.Swapping;
                    body_anim.SetFloat("Speed", 0);
                    body_anim.Play("bodyLightOut");
                }
            }
        }
        else if(myState == playerState.Light)
        {
            //check for mode transition
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //start animations to transition back to body mode
                myState = playerState.Swapping;
                body_anim.Play("bodyLightIn");
            }
        }
    }

    void FixedUpdate()
    {
        //player movement
        if(myState == playerState.Body)
        {
            rb_body.MovePosition(rb_body.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
        else if (myState == playerState.Light)
        {
            rb_light.MovePosition(rb_light.position + movement.normalized * lightSpeed * Time.fixedDeltaTime);
        }
    }

    public void LightOut()
    {
        //play head animation
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = bodySprite.flipX;
        head_anim.Play("headLightOut");

        //activate light ball
        lightBall.gameObject.SetActive(true);
    }

    public void FreeLight()
    {
        //allow light to roam freely
        lightBall.parent = null;
        myState = playerState.Light;
    }

    public void LightIn()
    {
        //snap!
        Instantiate(lightRemnant, lightBall.position + (Vector3)lightBall.GetComponent<CircleCollider2D>().offset, lightBall.rotation);
        light_anim.Play("lightDisappear");

        //return light to parent
        lightBall.parent = lightHolder;
        lightBall.localPosition = new Vector3(0, 0, 0);
        lightBall.GetComponent<TrailRenderer>().enabled = false;

        //zero out head rotation and play animation
        if (bodySprite.flipX)
        {
            head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        }
        else
        {
            head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, -135f);
        }
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = bodySprite.flipX;
        head_anim.Play("headLightIn");
    }

    public void ActivateHead()
    {
        //deactivate light
        lightBall.gameObject.SetActive(false);

        //unflip head
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;

        //set head sprite animation to correct starting point
        if (bodySprite.flipX)
        {
            head_anim.SetFloat("Rotation", 0.125f);
        }
        else
        {
            head_anim.SetFloat("Rotation", 0.375f);
        }
    }

    public void ActivateBody()
    {
        //set body mode
        myState = playerState.Body;
    }
}
