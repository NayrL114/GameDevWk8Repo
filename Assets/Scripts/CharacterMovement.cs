using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Vector3 movement;
    private float movementSqrMagnitude;
    public float walkSpeed = 1.75f;
    public Animator playerAnimator;
    public AudioSource footstepSource;
    public AudioClip[] footstepClips;
    public AudioSource bgmSource;

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        CharacterPosition();
        CharacterRotation();
        WalkAnimation();
        FootstepAudio();
    }

    void GetMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrMagnitude = movement.sqrMagnitude;
        //Debug.Log(movement);
    }

    void CharacterPosition()
    {
        movement.x = movement.x * walkSpeed * Time.deltaTime;
        movement.z = movement.z * walkSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    void CharacterRotation()
    {
        if (movement.x != 0 || movement.z != 0) transform.rotation = Quaternion.LookRotation(movement);

    }

    void WalkAnimation()
    {
        playerAnimator.SetFloat("MovingSpeed", movementSqrMagnitude);
    }

    void FootstepAudio()
    {
        if (movementSqrMagnitude > 0.25f && !footstepSource.isPlaying)
        {
            if (footstepSource.clip == footstepClips[1]){
                footstepSource.clip = footstepClips[0];
            }
            else
            {
                footstepSource.clip = footstepClips[1];
            }
            footstepSource.volume = movementSqrMagnitude;
            bgmSource.volume = 0.5f;
            footstepSource.Play();
        }
        else if (movementSqrMagnitude <= 0.3f && footstepSource.isPlaying)
        {
            footstepSource.Stop();
            bgmSource.volume = 1f;
        }
    }

}
