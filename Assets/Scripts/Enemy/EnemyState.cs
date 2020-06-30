using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}

public class ChaseState : EnemyState
{
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}

public class AttackState : EnemyState
{
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}