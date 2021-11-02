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

    private float moveSpeed = 3f;
    private float lightSpeed = 5f;
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
            head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

            float angle = 1 - (head_pointer.transform.localEulerAngles.z / 360);
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

        //point head light toward mouse
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - head_pointer.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
        head_anim.Play("headLightIn");
    }

    public void ActivateHead()
    {
        //deactivate light
        lightBall.gameObject.SetActive(false);

        //unflip head
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;

        //rotate head sprite toward mouse
        float angle = 1 - (head_pointer.transform.localEulerAngles.z / 360);
        head_anim.SetFloat("Rotation", angle);      
    }

    public void ActivateBody()
    {
        //set body mode
        myState = playerState.Body;
    }

    IEnumerator ActivateLight()
    {
        //start body and head animations
        body_anim.Play("bodyLightOut");
        head_anim.Play("headLightOut");

        //ensure head is facing the same way as the body
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = bodySprite.flipX;

        //wait until body is snapping fingers
        yield return new WaitUntil(() => bodySprite.sprite.name == "BodySpriteSheet_2");

        //activate light
        lightBall.gameObject.SetActive(true);

        //wait until body's arm is back at side
        yield return new WaitUntil(() => bodySprite.sprite.name == "BodySpriteSheet_5");

        //allow light to roam freely
        lightBall.parent = null;
        myState = playerState.Light;
    }

    IEnumerator DeactivateLight()
    {
        //start light fade
        light_anim.Play("lightDisappear");

        //wait until light has disappeared
        yield return new WaitUntil(() => lightBall.transform.localScale.x < 0.01);

        //deactivate light
        lightBall.gameObject.SetActive(false);
    }

    IEnumerator ReactivateBody()
    {
        //freeze body and start snapping animation
        body_anim.SetFloat("Speed", 0);
        body_anim.Play("bodyLightIn");

        //wait until body is snapping fingers
        yield return new WaitUntil(() => bodySprite.sprite.name == "BodySpriteSheet_2");

        //instantiate light remnant
        Instantiate(lightRemnant, lightBall.position + (Vector3)lightBall.GetComponent<CircleCollider2D>().offset, lightBall.rotation);

        //turn off light
        StartCoroutine(DeactivateLight());

        //start head animation
        StartCoroutine(ReactivateHead());

        //return light to parent
        lightBall.parent = lightHolder;
        lightBall.localPosition = new Vector3(0, 0, 0);

        //wait until arm is back at side and light is deactivated
        yield return new WaitUntil(() => bodySprite.sprite.name == "BodySpriteSheet_5" && !lightBall.gameObject.activeInHierarchy);

        //allow head to rotate freely
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;

        //set body mode
        myState = playerState.Body;
    }

    IEnumerator ReactivateHead()
    {
        //point head light toward mouse
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - head_pointer.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        //double check head is flipped to align with body and play head animation
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = bodySprite.flipX;
        head_anim.Play("headLightIn");

        //wait until head is closed
        yield return new WaitUntil(() => head_anim.transform.GetComponent<SpriteRenderer>().sprite.name == "HeadSpriteSheet_2");

        //unflip head
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;

        //rotate head sprite toward mouse
        float angle = 1 - (head_pointer.transform.localEulerAngles.z / 360);
        head_anim.SetFloat("Rotation", angle);
    }
}
