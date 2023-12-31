using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Game : MonoBehaviour
{
    public GameObject EventManager;
    public GameObject UnitManagerPrefab;
    private UnitManager UM;
    public Transform Gameboard;
    private AudioSource audio;
    public GameObject UpgradeManagerPrefab;
    public UpgradeManager UpgradeM;

    public GameObject[,] positions = new GameObject[8, 8];
    public Portal[,] portalPositions = new Portal[8, 8];
    public GameObject[] playerBlack = new GameObject[16];
    public GameObject[] playerWhite = new GameObject[16];
    

    public string currentPlayer = "white";

    public int turnNumber = 1;

    private bool gameOver = false;

    private bool panelActive = false;

    private bool eventDone = false;

    public bool selectedUpgrade = false;

    public string restriction;

    public bool riverActive;
    
    public int[] riverSpot;

    public void Awake()
    {
        GameObject obj = Instantiate(this.UnitManagerPrefab);
        this.UM = obj.GetComponent<UnitManager>();
        UpgradeM = Instantiate(UpgradeManagerPrefab).GetComponent<UpgradeManager>();
        riverSpot = new int[8];
    }

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            UM.Create("rook","white", 0, 0), UM.Create("knight","white", 1, 0),
            UM.Create("bishop","white", 2, 0), UM.Create("queen", "white", 3, 0),
            UM.Create("king", "white", 4, 0), UM.Create("bishop","white", 5, 0), 
            UM.Create("knight","white", 6, 0), UM.Create("rook","white", 7, 0),
            UM.Create("pawn", "white",0, 1), UM.Create("pawn", "white",1, 1),
            UM.Create("pawn", "white",2, 1), UM.Create("pawn", "white",3, 1),
            UM.Create("pawn", "white",4, 1), UM.Create("pawn", "white",5, 1), 
            UM.Create("pawn", "white",6, 1), UM.Create("pawn", "white",7, 1),
        };
        playerBlack = new GameObject[]
        {
            UM.Create("rook","black", 0, 7), UM.Create("knight","black", 1, 7), 
            UM.Create("bishop","black", 2, 7), UM.Create("queen", "black", 3, 7),
            UM.Create("king", "black", 4, 7), UM.Create("bishop","black", 5, 7),
            UM.Create("knight","black", 6, 7), UM.Create("rook","black", 7, 7),
            UM.Create("pawn","black", 0, 6), UM.Create("pawn","black", 1, 6), 
            UM.Create("pawn","black", 2, 6), UM.Create("pawn","black", 3, 6),
            UM.Create("pawn","black", 4, 6), UM.Create("pawn","black", 5, 6), 
            UM.Create("pawn","black", 6, 6), UM.Create("pawn","black", 7, 6),
        };
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
        restriction = null;
        riverActive = false;
    }
    
    public void SetPosition(GameObject obj)
    {
            ChessPiece cp = obj.GetComponent<ChessPiece>();
            positions[cp.GetXBoard(), cp.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
        turnNumber++;
        panelActive = false;
        eventDone = false;
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void Update()
    {
        if (turnNumber % 15 == 0 && !eventDone)
        {
            
            GameObject obj = Instantiate(this.EventManager);
            ObstacleEventManager OEM = obj.GetComponent<ObstacleEventManager>();
            OEM.ClearEvent();
            OEM.RandomEvent();
            eventDone = true;
        }
        
        if (turnNumber % 10 == 0 && panelActive == false)
        {
            UpgradeM.UpgradePanel();
            panelActive = true;
        }

        if (turnNumber % 10 == 9)
        {
            Animator anim = GameObject.FindGameObjectWithTag("Reminder").GetComponent<Animator>();
            anim.Play("Remind");
        }
        
        if ((turnNumber % 10 == 1 && turnNumber > 10) && panelActive == false) 
        {
            UpgradeM.UpgradePanel();
            panelActive = true;
        }
        
        
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
        
        if (turnNumber % 2 == 0 && gameOver != true)
        {
            GameObject.FindGameObjectWithTag("Turn").GetComponent<TextMeshProUGUI>().text = "Turn " + turnNumber;
            GameObject.FindGameObjectWithTag("Player").GetComponent<TextMeshProUGUI>().text = "Black to move";
        }
        else if (gameOver != true)
        {
            GameObject.FindGameObjectWithTag("Turn").GetComponent<TextMeshProUGUI>().text = "Turn " + turnNumber;
            GameObject.FindGameObjectWithTag("Player").GetComponent<TextMeshProUGUI>().text = "White to move";   
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("WinSound").GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag("Turn").GetComponent<TextMeshProUGUI>().text = playerWinner + " wins!";
        GameObject.FindGameObjectWithTag("Player").GetComponent<TextMeshProUGUI>().text = "Tap to Restart";
    }
    
}
