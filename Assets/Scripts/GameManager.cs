using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // can only be accessed in this class
    private static GameManager instance;

    // is available from all throughout the code with GameManager.Instance
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    // scene references
    [Header("UI objects")]
    [SerializeField] internal GameObject mainMenuObject;
    [SerializeField] internal GameObject pauseObject;
    

    // state machine
    internal GameFSM fsm;

    public void Awake()
    {
 

        fsm = new GameFSM();
        fsm.Initialize();
    }

    public void Start()
    {
        GotoMainMenu();
    }

    private void Update()
    {
        fsm.UpdateState();
    }


    public void StartLevel()
    {
        
    }

    public void EndLevel()
    {
        
    }

    #region GOTO_STATE
    public void GotoMainMenu()
    {
        fsm.GotoState(GameStateType.MainMenu);
        EndLevel();
    }

    public void GotoPlay()
    {
        fsm.GotoState(GameStateType.Play);
    }

    public void GotoPause()
    {
        fsm.GotoState(GameStateType.Pause);
    }
    #endregion
}
