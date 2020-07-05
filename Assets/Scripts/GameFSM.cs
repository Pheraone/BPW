using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameFSM
{
    private Dictionary<GameStateType, GameState> states;

    public GameStateType CurrentStateType { get; private set; }
    private GameState currentState;
    private GameState previousState;

    public void Initialize()
    {
        // setup the dictionary for the states to be added
        states = new Dictionary<GameStateType, GameState>();

        // add all states 
        states.Add(GameStateType.MainMenu, new MainMenuState());
        states.Add(GameStateType.Play, new PlayState());
        states.Add(GameStateType.Pause, new PauseState());
        states.Add(GameStateType.Win, new WinState());
        states.Add(GameStateType.Lose, new LoseState());

    }

    public GameState GetState(GameStateType type)
    {
        // if state exists return
        // else null
        if (states.ContainsKey(type))
            return states[type];
        else
            return null;
    }

    public void UpdateState()
    {
        //update if current state exists
        if (currentState != null)
            currentState.Update();
    }

    public void GotoState(GameStateType key)
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
