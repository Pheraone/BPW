using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    internal EnemyFSM fsm;

    private Renderer colorchange;
    private Color colorStart;
    
    public bool iGotShot = false;
    public float timeToChange = 0.3f;
    private float timeSinceChange;
    public float health;
    public float maxHealth = 100;
    public NavMeshAgent navMeshAgent;
    public float damage;
    public bool iHitThePlayer = false;
    [SerializeField] public float walkRadius = 20;
    internal Vector3 finalPosition;
    public bool walking = false;

    public GameObject speler;
    public Vector3 firstPos;

    private void Awake()
    {
        //initialize fsm 
        fsm = new EnemyFSM();
        fsm.Initialize(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
        speler = PlayerMove.Instance.gameObject;
        //setting first position
        firstPos = transform.position;

        //adding states
        fsm.AddState(EnemyStateType.Idle, new IdleState());
        fsm.AddState(EnemyStateType.Chase, new ChaseState());
        fsm.AddState(EnemyStateType.Attack, new AttackState());

        //setting health
        health = maxHealth;
    }

    private void Start()
    {
        //making sure AI starts in idle state
        GotoIdle();
    }

    
    private void Update()
    {
        fsm.UpdateState();

        // if enemy got shot change color to red and back
        if (iGotShot == false)
        {
            colorchange = this.gameObject.GetComponent<Renderer>();
            colorchange.material.SetColor("_Color", colorStart);
        }
        else if (iGotShot == true)
        {
            colorchange = this.gameObject.GetComponent<Renderer>();
            colorchange.material.SetColor("_Color", Color.red);

            timeSinceChange += Time.deltaTime;
            if (timeSinceChange >= timeToChange)
            {
                iGotShot = false;
                timeSinceChange = 0f;
            }
        }
        //checking if health is 0 or below 
        if (health <= 0 && iGotShot == false)
        {
            //if true setting enemy to inactive
            gameObject.SetActive(false);
        }

    }

    public void ResetPosition()
    {
        //resetting the position from the navmeshagent
        navMeshAgent.Warp(firstPos);
    }

    public void GotoIdle()
    {
        fsm.GotoState(EnemyStateType.Idle);
    }

    public void GotoChase()
    {
        fsm.GotoState(EnemyStateType.Chase);
    }
    
    public void GotoAttack()
    {
        fsm.GotoState(EnemyStateType.Attack);
    }



    private void OnCollisionEnter(Collision collision)
    {
        //checking if there is collision with the player
        if (collision.gameObject.tag == "Player")
        {
            //damaging player
            PlayerMove.Instance.DamageTaken(damage);
            iHitThePlayer = true;
        }
    }

    public void TakeDamage(float amount)
    {
        //getting damage
        iGotShot = true;
        health = health - amount;
    }
}

