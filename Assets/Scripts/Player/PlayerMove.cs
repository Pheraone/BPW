using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float superSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private KeyCode sprintKey;


    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;


    private bool isJumping;
    private bool isSprinting = false;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        JumpInput();
        SprintInput();
    }

    private void SprintInput()
    {
        // Try 1
     
          if(Input.GetKeyDown(sprintKey) && !isSprinting)
          {
              isSprinting = false;
              StartCoroutine(SprintEvent());
          }
          else if (Input.GetKeyDown(sprintKey) && isSprinting)
          {
              isSprinting = true;
              StartCoroutine(SprintEvent());
          }

         /*
           if (Input.GetKeyDown(sprintKey))
            {
                isSprinting = !isSprinting;
                StartCoroutine(SprintEvent());
                Debug.Log(isSprinting);
            } */
    }

    private IEnumerator SprintEvent()
    {
       if(isSprinting == false)
        {
            movementSpeed = superSpeed;
            isSprinting = true;
            Debug.Log("I am Running");
            
        }
        else
        {
            movementSpeed = normalSpeed;
            isSprinting = false;
            Debug.Log("I am walking");
            
        }
        yield return isSprinting;
    }

    private void JumpInput()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above );

        charController.slopeLimit = 45.0f;
        isJumping = false;
 
    }
}