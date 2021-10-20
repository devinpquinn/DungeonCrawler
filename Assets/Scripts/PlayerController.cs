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
    private float lightSpeed = 4.5f;
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
            head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;

            float angle = 1 - (head_pointer.transform.localEulerAngles.z / 360);
            head_anim.SetFloat("Rotation", angle);

            //check for mode transition
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(myState == playerState.Body)
                {
                    myState = playerState.Swapping;
                    StartCoroutine(ActivateLight());
                }
            }
        }
        else if(myState == playerState.Light)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myState = playerState.Swapping;
                StartCoroutine(ReactivateBody());
            }
        }
    }

    void FixedUpdate()
    {
        if(myState == playerState.Body)
        {
            rb_body.MovePosition(rb_body.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
        else if (myState == playerState.Light)
        {
            rb_light.MovePosition(rb_light.position + movement.normalized * lightSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator ActivateLight()
    {
        body_anim.Play("bodyLightOut");
        head_anim.Play("headLightOut");
        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = bodySprite.flipX;
        yield return new WaitUntil(() => bodySprite.sprite.name == "BodySpriteSheet_2");
        lightBall.gameObject.SetActive(true);

        yield return new WaitUntil(() => bodySprite.sprite.name == "BodySpriteSheet_5");
        lightBall.parent = null;
        myState = playerState.Light;
    }

    IEnumerator DeactivateLight()
    {
        light_anim.Play("lightDisappear");
        yield return new WaitForSeconds(light_anim.GetCurrentAnimatorStateInfo(0).length);
        lightBall.gameObject.SetActive(false);
    }

    IEnumerator ReactivateBody()
    {
        body_anim.SetFloat("Speed", 0);
        body_anim.Play("bodyLightIn");
        yield return new WaitUntil(() => bodySprite.sprite.name == "BodySpriteSheet_2");
        Instantiate(lightRemnant, lightBall.position, lightBall.rotation);
        StartCoroutine(DeactivateLight());
        StartCoroutine(ReactivateHead());
        lightBall.parent = lightHolder;
        lightBall.localPosition = new Vector3(0, 0, 0);
        Instantiate(lightRemnant, lightBall.position, lightBall.rotation);
        yield return new WaitUntil(() => bodySprite.sprite.name == "BodySpriteSheet_5" && !lightBall.gameObject.activeInHierarchy);
        myState = playerState.Body;
    }

    IEnumerator ReactivateHead()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - head_pointer.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        head_pointer.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        head_anim.gameObject.GetComponent<SpriteRenderer>().flipX = bodySprite.flipX;
        head_anim.Play("headLightIn");

        yield return new WaitForSeconds(head_anim.GetCurrentAnimatorStateInfo(0).length);
    }
}
