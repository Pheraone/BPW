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
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    private float timeInAir = 0.5f;
   

    public float fullHealth = 100;
    public float health;

    public Vector3 firstPosition;

    private bool isJumping;
    private bool isSprinting = false;

    public GameObject tagObj;

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
        //intialize
        charController = GetComponent<CharacterController>();
        //setting first position
        firstPosition = transform.position;
        health = fullHealth;
        tagObj = GameObject.FindGameObjectWithTag("EndGameTrigger");
    }

    private void Update()
    {
        //update player movement
        PlayerMovement();

  
    }

    private void PlayerMovement()
    {
        //making player move
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        //changing direction
        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        //checking for jump and sprint input
        JumpInput();
        SprintInput();
    }

    private void SprintInput()
    {
        //checking for sprint input
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
        //making player sprint
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
        //checking for jump input
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {  
        //making player jump
        charController.slopeLimit = 90.0f;
        
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above );
        //setting slope limit
        charController.slopeLimit = 45.0f;
        isJumping = false;
 
    }

    public void DamageTaken(float amount)
    { 
        //changing health if taken damage
        health = health - amount;
        if(health <= 0)
        {
            //no health left means losescreen
            GameManager.Instance.GoToLose();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //checking if player collides with object with tag
        if(other.transform.tag == "EndGameWinTrigger")
        {
            GameManager.Instance.GoToWin();
        }

        if (other.transform.tag == "EndGameTrigger")
        {
            GameManager.Instance.GoToLose();
        }
    }
    public void ResetPosition()
    {   
        //resetting the position to the position of the first frame
        transform.SetPositionAndRotation(firstPosition, Quaternion.identity);
    }

}