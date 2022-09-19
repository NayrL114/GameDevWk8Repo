using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Vector3 movement;
    private float movementSqrMagnitude;

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
        movementSqrMagnitude = movement.sqrMagnitude;
        Debug.Log(movement);
    }

    void CharacterPosition()
    {

    }

    void CharacterRotation()
    {

    }

    void WalkAnimation()
    {

    }

    void FootstepAudio()
    {

    }

}
