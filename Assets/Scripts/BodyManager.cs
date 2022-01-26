using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    //player
    private PlayerController pc;

    //footstep sounds
    private FootstepSounds footstepSounds;

    public AudioClip deathSound;

    private void Awake()
    {
        pc = gameObject.transform.parent.GetComponent<PlayerController>();
        footstepSounds = gameObject.GetComponent<FootstepSounds>();
    }

    public void LightOut()
    {
        pc.LightOut();
    }

    public void FreeLight()
    {
        pc.FreeLight();
    }

    public void LightIn()
    {
        pc.LightIn();
    }

    public void ActivateBody()
    {
        pc.ActivateBody();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Death"))
        {
            pc.Die();
        }
    }

    public void PlayFootstep()
    {
        //raycast to determine material currently being stepped on
        int layerMask = (LayerMask.GetMask("Step"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layerMask);
        if(hit.collider != null)
        {
            footstepSounds.PlayFootstepSound(hit.collider.name);
        }
    }

    public void PlayBodyFall()
    {
        footstepSounds.PlayCloakFall();
    }

    public void PlayDeathSound()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.PlayOneShot(deathSound);
    }
}
