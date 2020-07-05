using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStateType { MainMenu, Play, Pause, Win, Lose }

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
        //showing main menu
        GameManager.Instance.mainMenuObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //making sure player can not move or shoot
        Time.timeScale = 0;
        GameManager.Instance.inGameUI.SetActive(false);
        GameManager.Instance.gun.SetActive(false);
        
    }

    public override void Exit()
    {
        //hiding main menu & enabling player to move
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
        //activating things necessary for game
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

    }


}

public class PauseState : GameState
{
    public override void Enter()
    {
        //showing pause menu
        GameManager.Instance.pauseObject.SetActive(true);
        GameManager.Instance.inGameUI.SetActive(false);
        GameManager.Instance.gun.SetActive(false);

        //making sure player does not move
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Exit()
    {
        //enabling player to move & hiding pause menu
        GameManager.Instance.pauseObject.SetActive(false);
        Time.timeScale = 1;
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.fsm.GotoState(GameStateType.Play);
    }
}

public class LoseState : GameState
{
    public override void Enter()
    {
        //menu for lose screen
        GameManager.Instance.loseObject.SetActive(true);
        GameManager.Instance.inGameUI.SetActive(false);
        GameManager.Instance.gun.SetActive(false);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Update()
    {
     
    }

    public override void Exit()
    {
        GameManager.Instance.loseObject.SetActive(false);
        Time.timeScale = 1;
    }

   
}

public class WinState : GameState
{
    public override void Enter()
    {
        //menu for win screen
        GameManager.Instance.winObject.SetActive(true);
        GameManager.Instance.inGameUI.SetActive(false);
        GameManager.Instance.gun.SetActive(false);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Update()
    {
       

    }

    public override void Exit()
    {
        GameManager.Instance.winObject.SetActive(false);
        Time.timeScale = 1;
    }

}


