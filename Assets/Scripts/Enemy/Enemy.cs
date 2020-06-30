using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    internal EnemyFSM fsm;

    private void Awake()
    {
        fsm = new EnemyFSM();
        fsm.Initialize(this);

        fsm.AddState(EnemyStateType.Idle, new IdleState());
        fsm.AddState(EnemyStateType.Chase, new ChaseState());
        fsm.AddState(EnemyStateType.Attack, new AttackState());
    }

    private void Start()
    {
        GotoIdle();
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    public void GotoIdle()
    {

    }

    public void GotoChase()
    {

    }
    
    public void GotoAttack()
    {

    }
}
