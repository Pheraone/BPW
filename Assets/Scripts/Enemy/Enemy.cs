using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    internal EnemyFSM fsm;

    private Renderer colorchange;
    private Color colorStart;
    Color colorAttack;
    public bool iGotShot = false;
    public float timeToChange = 0.3f;
    private float timeSinceChange;
    float health = 100;
    public NavMeshAgent navMeshAgent;
    public float damage;
    public bool wait = false;
    [SerializeField] public float walkRadius = 20;
    internal Vector3 finalPosition;
    public bool walking = false;

    public GameObject speler;
    private void Awake()
    {
        fsm = new EnemyFSM();
        fsm.Initialize(this);

        navMeshAgent = GetComponent<NavMeshAgent>();

        fsm.AddState(EnemyStateType.Idle, new IdleState());
        fsm.AddState(EnemyStateType.Chase, new ChaseState());
        fsm.AddState(EnemyStateType.Attack, new AttackState());
    }

    private void Start()
    {
        GotoIdle();
        speler = PlayerMove.Instance.gameObject;
        
    }





private void Update()
    {
        fsm.UpdateState();


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

        if (health <= 0 && iGotShot == false)
        {
            Destroy(gameObject);
        }

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
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("I hit the player");
            PlayerMove.Instance.DamageTaken(damage);
            wait = true;
        }
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("Damage taken" + amount);
        iGotShot = true;
        health = health - amount;
        Debug.Log(health);
    }
}

