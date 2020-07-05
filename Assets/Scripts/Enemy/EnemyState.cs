using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStateType { Idle, Chase, Attack}

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
            //if distance player to enemy is below, change speed, change state
            enemy.navMeshAgent.speed = 1.5f;
            enemy.GotoChase();
        }
 
        if (CheckDestinationReached())
        {
          //checking if destination was reached
          //Walk -> set destination
                Walk();
                
        }
        



    }
    public override void Exit()
    {

    }

    void Walk()
    {
        //getting an area in a sphere
        Vector3 randomDirection = Random.insideUnitSphere * enemy.walkRadius;
        randomDirection += enemy.transform.position;
        NavMeshHit hit;
        //get a random navmesh position in the sphere
        NavMesh.SamplePosition(randomDirection, out hit, enemy.walkRadius, 1);
        enemy.finalPosition = hit.position;
        enemy.navMeshAgent.SetDestination(enemy.finalPosition);
        //setting vector3 y to 1, for if not points would not be the same
        enemy.finalPosition.y = 1f;
    }
    
    bool CheckDestinationReached()
    {
        //checking distance to final pos
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
        enemy.navMeshAgent.speed = 2f;
    }

    public override void Update()
    {
        //if distance player to enemy is below, change speed, change state
        enemy.navMeshAgent.SetDestination(PlayerMove.Instance.transform.position);
        if (Vector3.Distance(enemy.transform.position, PlayerMove.Instance.transform.position) > 10)
        {
            enemy.GotoIdle();
        }

        //if distance player to enemy is below, change speed, change state
        if (Vector3.Distance(enemy.transform.position, PlayerMove.Instance.transform.position) < 5)
        {
            enemy.GotoAttack();
        }

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
        //setting speed and timer
        enemy.navMeshAgent.speed = 3.1f;
        timer = time;
    }
    public override void Update()
    {  
        enemy.navMeshAgent.SetDestination(PlayerMove.Instance.transform.position);
        //collision with player = true
        if (enemy.iHitThePlayer == true) {
            //freeze navmeshAgent aka time out
            enemy.navMeshAgent.velocity = Vector3.zero;
            enemy.navMeshAgent.speed = 1f;
            
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //time out over
                enemy.GotoChase();
                enemy.iHitThePlayer = false;
            }
        }

        //if distance player to enemy is below, change speed, change state
        if (Vector3.Distance(enemy.transform.position, PlayerMove.Instance.transform.position) > 5 && enemy.iHitThePlayer == false)
        {
            enemy.GotoChase();
        }
    }
    public override void Exit()
    {

    }
}