using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM
{
    public Enemy owner { get; private set; }
    private Dictionary<EnemyStateType, EnemyState> states;

    public EnemyStateType CurrentStateType { get; private set; }
    private EnemyState currentState;
    private EnemyState previousState;

    public void Initialize(Enemy owner)
    {
        this.owner = owner;
        // setup the dictionary for the states to be added
        states = new Dictionary<EnemyStateType, EnemyState>();

        // add all states 

    }

    public void AddState(EnemyStateType newType, EnemyState newState)
    {
        states.Add(newType, newState);
        states[newType].Initialize(this);
    }

    public EnemyState GetState(EnemyStateType type)
    {
        // returns state if it exists, 
        // else null
        if (states.ContainsKey(type))
            return states[type];
        else
            return null;
    }

    public void UpdateState()
    {
        // if the current state exists, 
        // update it
        if (currentState != null)
            currentState.Update();
    }

    public void GotoState(EnemyStateType key)
    {
        // if this state doesn't exist, return
        if (!states.ContainsKey(key))
            return;

        // if there is a current state, 
        // end the state properly by calling exit
        if (currentState != null)
            currentState.Exit();

        // remember the previous state
        // and set the current state
        previousState = currentState;
        CurrentStateType = key;
        currentState = states[CurrentStateType];

        // start the new current state 
        // by entering it
        currentState.Enter();
    }
}