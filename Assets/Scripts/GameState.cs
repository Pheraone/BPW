using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStateType { MainMenu, Play, Pause }

public abstract class GameState
{ 
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}

public class MainMenuState : GameState
{
    public override void Enter()
    {
        GameManager.Instance.mainMenuObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        GameManager.Instance.inGameUI.SetActive(false);
        GameManager.Instance.gun.SetActive(false);
    }

    public override void Exit()
    {
        GameManager.Instance.mainMenuObject.SetActive(false);
        Time.timeScale = 1;
    }

    public override void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.fsm.GotoState(GameStateType.Play);
            GameManager.Instance.StartLevel();
        }
    }
}

public class PlayState : GameState
{
    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        GameManager.Instance.inGameUI.SetActive(true);
        GameManager.Instance.gun.SetActive(true);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.fsm.GotoState(GameStateType.Pause);
        }
           
      
        if (PlayerMove.Instance.endGame == true)
        {
            GameManager.Instance.fsm.GotoState(GameStateType.Pause);
        }
    }


}

public class PauseState : GameState
{
    public override void Enter()
    {
        GameManager.Instance.pauseObject.SetActive(true);
        GameManager.Instance.inGameUI.SetActive(false);
        GameManager.Instance.gun.SetActive(false);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Exit()
    {
        GameManager.Instance.pauseObject.SetActive(false);
        Time.timeScale = 1;
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.fsm.GotoState(GameStateType.Play);
    }
}