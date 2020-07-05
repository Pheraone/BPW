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

    //reset vars
    private GameObject[] enemies;
    private GameObject player;
   
    public void Awake()
    {
        //initialize fsm
        fsm = new GameFSM();
        fsm.Initialize();

        //finding all the enemies
        if(enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        //finding the player
        player = GameObject.FindGameObjectWithTag("Player");
        
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
        //resetting the player
        player.GetComponent<PlayerMove>().ResetPosition();
        //resetting every enemy in the array
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().ResetPosition();
            enemy.GetComponent<Enemy>().GotoIdle();
        }

    }

    public void EndLevel()
    {
       //preparing enemies for reset
       foreach (GameObject enemy in enemies)
        {
           
            enemy.SetActive(false);
            enemy.GetComponent<Enemy>().health = enemy.GetComponent<Enemy>().maxHealth;
        }
       //resetting the health
        PlayerMove.Instance.health = PlayerMove.Instance.fullHealth;
    }



    public void GotoMainMenu()
    {
        fsm.GotoState(GameStateType.MainMenu);
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
