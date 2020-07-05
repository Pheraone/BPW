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
    private float timeInAir = 0.5f;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    float health = 100;

    public Vector3 firstPos;

    private bool isJumping;
    private bool isSprinting = false;

    private static PlayerMove instance;
    public static PlayerMove Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerMove>();

            return instance;
        }
    }

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        firstPos = transform.position;
}

    private void Update()
    {
        PlayerMovement();

        if(health <= 0)
        {
           //TO DO: GAME OVER
        }
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
    }

    private IEnumerator SprintEvent()
    {
       if(isSprinting == false)
        {
            movementSpeed = superSpeed;
            isSprinting = true;
            
        }
        else
        {
            movementSpeed = normalSpeed;
            isSprinting = false;
          
            
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

    public void DamageTaken(float amount)
    {
        Debug.Log(gameObject + "Damage taken" + amount);
        health = health - amount;
        Debug.Log(health);

        if(health <= 0)
        {
            GameManager.Instance.iDied = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "EndGameTrigger")
        {
            Debug.Log("game had ended");
            GameManager.Instance.endGame = true;
            //END GAME SCREEN
        }
    }
    public void ResetPosition()
    {
        Debug.Log("I work" + firstPos);
        transform.position = firstPos;
    }

}