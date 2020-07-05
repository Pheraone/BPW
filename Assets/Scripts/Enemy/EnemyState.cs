using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStateType { Idle, Chase, Attack }

public abstract class EnemyState
{

    protected EnemyFSM owner;
    protected Enemy enemy;
    public void Initialize(EnemyFSM owner)
    {
        this.owner = owner;
        enemy = owner.owner;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}

public class IdleState : EnemyState
{
    
    public override void Enter()
    {
        Walk();
  
    }
    public override void Update()
    {
        
        //check distance
        if (Vector3.Distance(enemy.transform.position, PlayerMove.Instance.transform.position) < 10)
        {
            enemy.navMeshAgent.speed = 1.5f;
            Debug.Log("Hoi");
            enemy.GotoChase();
        }
        //enemy.gotochase
        Debug.Log("navmesh pos "+ enemy.navMeshAgent.transform.position);
        Debug.Log("final pos " + enemy.finalPosition);
        
            if (CheckDestinationReached())
            {
            Debug.Log("I am here!");
                Walk();
                
            }
        



    }
    public override void Exit()
    {

    }

    void Walk()
    {
        Vector3 randomDirection = Random.insideUnitSphere * enemy.walkRadius;
        randomDirection += enemy.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, enemy.walkRadius, 1);
        Debug.Log("hit is " +  hit);
        enemy.finalPosition = hit.position;
        enemy.navMeshAgent.SetDestination(enemy.finalPosition);
        Debug.Log("final position is " + enemy.finalPosition);
        enemy.finalPosition.y = 1f;
    }
    
    bool CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.finalPosition);
        if (distanceToTarget < 2f)
        {
            return true;
        }
        else { return false;  }
    }

}

public class ChaseState : EnemyState
{
    public override void Enter()
    {
        Debug.Log("I am in the chaseState");
        enemy.navMeshAgent.speed = 2f;
       
        
    }
    public override void Update()
    {
        enemy.navMeshAgent.SetDestination(PlayerMove.Instance.transform.position);
        //Debug.Log(PlayerMove.Instance.transform.position);
        //check distance (groter dan)
        //enemy.gotoidle
        if (Vector3.Distance(enemy.transform.position, PlayerMove.Instance.transform.position) > 10)
        {
            Debug.Log("Doei");
            enemy.GotoIdle();
        }

        if (Vector3.Distance(enemy.transform.position, PlayerMove.Instance.transform.position) < 5)
        {
            enemy.GotoAttack();
        }

     
        //setdestination -> gameobject?
        //Player.Instance.transform.position
    }
    public override void Exit()
    {

    }
}

public class AttackState : EnemyState
{
    float timer;
    float time = 5f;
    public override void Enter()
    {
        enemy.navMeshAgent.speed = 3.1f;
        timer = time;
        Debug.Log("I am in the attackState");
    }
    public override void Update()
    {
        enemy.navMeshAgent.SetDestination(PlayerMove.Instance.transform.position);
        if (enemy.wait == true) {
            enemy.navMeshAgent.speed = 1f;
            Debug.Log("going to wait");
            timer -= Time.deltaTime;
           // Debug.Log(timer);
            if (timer <= 0)
            {
                Debug.Log("slow down");
                enemy.GotoChase();
                enemy.wait = false;
            }
        }

        if(Vector3.Distance(enemy.transform.position, PlayerMove.Instance.transform.position) > 5)
        {
            enemy.GotoChase();
        }
    }
    public override void Exit()
    {

    }
}