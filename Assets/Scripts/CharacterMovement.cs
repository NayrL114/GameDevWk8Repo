using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Vector3 movement;
    private float movementSqrMagnitude;
    public float walkSpeed = 1.75f;

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
        Debug.Log(movement);
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

    }

    void FootstepAudio()
    {

    }

}
