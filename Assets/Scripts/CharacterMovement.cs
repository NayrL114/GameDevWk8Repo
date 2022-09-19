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
