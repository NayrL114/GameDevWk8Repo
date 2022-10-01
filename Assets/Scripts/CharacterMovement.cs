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
        //normal condition update methods
        GetMovementInput();
        CharacterRotation();
        WalkAnimation();
        if (!RaycastCheck())// if RaycastCheck returns false, which means the enemy is far away
        {// resume other normal condition
            CharacterPosition();            
            FootstepAudio();
        }// if RaycastCheck() returns true, means enemy is close, then the above two update methods won't be called              

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
    {// Added a seperate check to determine if player is facing the enemy
        if (RaycastCheck())
        {// if player is facing enemy, stop the walking animation
            playerAnimator.SetFloat("MovingSpeed", 0f);
        }
        else
        {
            playerAnimator.SetFloat("MovingSpeed", movementSqrMagnitude);
        }
        
    }

    void FootstepAudio()
    {
        if (movementSqrMagnitude > 0.25f && !footstepSource.isPlaying)
        {// flip flop the footstep autio clip
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
        {// reset the bgm volume
            footstepSource.Stop();
            bgmSource.volume = 1f;
        }
    }

    bool RaycastCheck()
    {
        Vector3 RayDirection = transform.position + new Vector3(0f, 1f, 0f);
        RaycastHit hit;

        if (Physics.Raycast(RayDirection, transform.TransformDirection(Vector3.forward), out hit, 5f))// Do a raycast from player with 5 units length, and if it doesn hit anything
        {
            Debug.Log("Raycast Hit: " + hit.collider.name);// Raycast Hit:  <GameObject name>
            if (hit.collider.tag == "Freeze")
            {// if the hitted object returned by Raycast is the enemy, return true
                return true;
            }
        }
        return false;// on every other conditions, return false to indicate the enemy is not hitted by raycast
    }

    void OnCollisionEnter(Collision collision) // player is colliding with PhysicsBox
    {
        Debug.Log("Collision Enter: " + collision.collider.name + ": " + collision.contacts[0].point); //Collision Enter: <GameObject name>: <Last Contact Point> 
    }

    void OnCollisionStay(Collision collision) // player is hitting the wall
    {
        if (collision.collider.tag == "Impassable")
        {
            Debug.Log("Collision Stay: " + collision.collider.name); //Collision Stay: <GameObject name>
        }
    }

    void OnTriggerExit(Collider other) //player is NOT colliding with TriggerBox
    {        
        Debug.Log("Trigger Exit: " + other.name + ": " + other.transform.position); //Trigger Exit: <GameObject name>: <Gameobject position>         
    }

}
