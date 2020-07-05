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
    [SerializeField] internal GameObject inGameUI;
    [SerializeField] internal GameObject winObject;
    [SerializeField] internal GameObject loseObject;
    [SerializeField] internal GameObject gun;

    // state machine
    internal GameFSM fsm;

    //reset thingys
    private GameObject[] enemies;
    
    public bool endGame = false;
    public bool iDied = false;

    public void Awake()
    {
 

        fsm = new GameFSM();
        fsm.Initialize();

        if(enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
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
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().ResetPosition();
            enemy.GetComponent<Enemy>().GotoIdle();

        }

        PlayerMove.Instance.ResetPosition();

        endGame = false;
        iDied = false;

    }

    public void EndLevel()
    {
       //GAME RESET
       foreach (GameObject enemy in enemies)
        {
           
            enemy.SetActive(false);
        }
    }



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
   
    public void GoToWin()
    {
        fsm.GotoState(GameStateType.Win);
    }
    public void GoToLose()
    {
        fsm.GotoState(GameStateType.Lose);
    }
}
