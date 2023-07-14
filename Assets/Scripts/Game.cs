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
    public GameObject Chesspiece;
    public GameObject EventManager;
    public GameObject UnitManagerPrefab;
    private UnitManager UM;
    public Transform Gameboard;
    public GameObject panel;
    private AudioSource audio;

    public static int numOfUpgrade = 8;
    // Random Number Generator to Generate Random Augments
    public Random rnd = new Random();
    // Augment Index: 1: Pawn1 2: Pawn2 3: Rook1 4: Rook2 5: Bishop1 6: Bishop2 7: Knight1 8: Knight2 9:Queen 10: King
    public bool[] WhiteAugments;
    public bool[] BlackAugments;

    public GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[] portalPair1;
    private GameObject[] portalPair2;

    private string currentPlayer = "white";

    private int turnNumber = 1;

    private bool gameOver = false;

    private bool panelActive = false;

    private bool eventDone = false;

    private bool selectedUpgrade = false;

    public void Awake()
    {
        GameObject obj = Instantiate(this.UnitManagerPrefab);
        this.UM = obj.GetComponent<UnitManager>();
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

        BlackAugments = new bool[numOfUpgrade];
        WhiteAugments = new bool[numOfUpgrade];
        for (int i = 0; i < BlackAugments.Length; i++)
        {
            BlackAugments[i] = false;
            WhiteAugments[i] = false;
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(Chesspiece, new Vector3(0, 0, 10), quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        obj.transform.parent = Gameboard;
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
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

        print(currentPlayer);

        turnNumber++;
        panelActive = false;
        eventDone = false;
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void Update()
    {
        if (turnNumber == 15 && !eventDone)
        {
            
            GameObject obj = Instantiate(this.EventManager);
            ObstacleEventManager OEM = obj.GetComponent<ObstacleEventManager>();
            //OEM.PortalEvent();
            //OEM.RiverEvent(4);
            eventDone = true;
        }
        
        if (turnNumber == 3 && panelActive == false)
        {
            this.UpgradePanel();
            panelActive = true;
        }

        if (turnNumber % 10 == 9)
        {
            Animator anim = GameObject.FindGameObjectWithTag("Reminder").GetComponent<Animator>();
            anim.Play("Remind");
        }
        
        if ((turnNumber == 11) && panelActive == false) 
        {
            this.UpgradePanel();
            panelActive = true;
        }
        
        if (turnNumber%15 == 0 && !eventDone)
        {
            eventDone = true;
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

    public int RowCheck(int rowNum)
    {
        
        bool checkIfNotFull = false;
        for (int k = 0; k <= 7; k++)
        {
            if (GetPosition(k, rowNum) == null)
            {
                checkIfNotFull = true;
            }
        }
        if (!checkIfNotFull)
        {
            return -1;
        }
        else
        {
            int rndCol = rnd.Next(0, 8);
            while (GetPosition(rndCol, rowNum) != null)
            {
                rndCol = rnd.Next(0, 8);
            }

            return rndCol;
        }
    }
    

    public void UpgradePanel()
    {
        GameObject obj = Instantiate(panel, new Vector3(100, 100, 10), Quaternion.identity);
        PanelManager panelManager = obj.GetComponent<PanelManager>();
        //panelManager.audio1.Play();
        panelManager.openPanel();
        void AllocateUpgrade(int optionNumber)
        {
            int curr = 0;
            if (currentPlayer != "white")
            {
                
                bool check = false;
                for (int i = 0; i < WhiteAugments.Length; i++)
                {
                    check = check || !WhiteAugments[i];
                }

                if (check == false)
                {
                    print("No More Upgrades");
                    return;
                }

                curr = rnd.Next(0, numOfUpgrade );
                print(WhiteAugments);
                while (WhiteAugments[curr] == true)
                {
                    curr = rnd.Next(0, numOfUpgrade);
                }
                WhiteAugments[curr] = true;
            }
            if (currentPlayer != "black")
            {
                
                bool check = false;
                for (int i = 0; i < BlackAugments.Length; i++)
                {
                    check = check || !BlackAugments[i];
                }

                if (check == false)
                {
                    print("No More Upgrades");
                    return;
                }

                curr = rnd.Next(0, numOfUpgrade);
                while (BlackAugments[curr] == true)
                {
                    curr = rnd.Next(0, numOfUpgrade);
                }

                BlackAugments[curr] = true;
            }


            switch (optionNumber)
            {
                case 1 :
                    panelManager.GetComponent<PanelManager>().Option1Text.GetComponent<TextMeshProUGUI>().text =
                        UpgradeTexter(5);
                    panelManager.GetComponent<PanelManager>().Option1Button.GetComponent<Button>().onClick
                        .AddListener(delegate
                        {
                            SelectUpgrades(5);
                            GameObject.FindGameObjectWithTag("UpgradeSound").GetComponent<AudioSource>().Play();
                            obj.SetActive(false);
                        });
                    break;
                case 2 :
                    panelManager.GetComponent<PanelManager>().Option2Text.GetComponent<TextMeshProUGUI>().text =
                        UpgradeTexter(curr);
                    panelManager.GetComponent<PanelManager>().Option2Button.GetComponent<Button>().onClick
                        .AddListener(delegate
                        {
                            SelectUpgrades(curr);
                            GameObject.FindGameObjectWithTag("UpgradeSound").GetComponent<AudioSource>().Play();
                            obj.SetActive(false);
                        });
                    break;
                case 3:
                    panelManager.GetComponent<PanelManager>().Option3Text.GetComponent<TextMeshProUGUI>().text =
                        UpgradeTexter(curr);
                    panelManager.GetComponent<PanelManager>().Option3Button.GetComponent<Button>().onClick
                        .AddListener(delegate
                        {
                            SelectUpgrades(curr);
                            GameObject.FindGameObjectWithTag("UpgradeSound").GetComponent<AudioSource>().Play();
                            obj.SetActive(false);
                        });
                    break;
            }
        }
        void SelectUpgrades(int number)
        {
            if (currentPlayer != "white")
            {
                switch (number)
                {
                    case 0:
                        WhiteAugments[0] = true;
                        PawnUpgrade1();
                        break;
                    case 1:
                        WhiteAugments[1] = true;
                        PawnUpgrade2();
                        break;
                    case 2:
                        WhiteAugments[2] = true;
                        RookUpgrade1();
                        break;
                    case 3:
                        WhiteAugments[3] = true;
                        RookUpgrade2();
                        break;
                    case 4:
                        WhiteAugments[3] = true;
                        BishopUpgrade1();
                        break;
                    case 5:
                        WhiteAugments[5] = true;
                        KnightUpgrade1();
                        break;
                    case 6:
                        WhiteAugments[5] = true;
                        QueenUpgrade1();
                        break;
                    case 7:
                        WhiteAugments[7] = true;
                        BishopUpgrade2();
                        break;
                }
            }
            else
            {
                switch (number)
                {
                    case 0:
                        BlackAugments[0] = true;
                        PawnUpgrade1();
                        break;
                    case 1:
                        BlackAugments[1] = true;
                        PawnUpgrade2();
                        break;
                    case 2:
                        BlackAugments[2] = true;
                        RookUpgrade1();
                        break;
                    case 3:
                        BlackAugments[3] = true;
                        RookUpgrade2();
                        break;
                    case 4:
                        BlackAugments[4] = true;
                        BishopUpgrade1();
                        break;
                    case 5:
                        BlackAugments[5] = true;
                        KnightUpgrade1();
                        break;
                    case 6:
                        BlackAugments[6] = true;
                        QueenUpgrade1();
                        break;
                    case 7:
                        BlackAugments[7] = true;
                        BishopUpgrade2();
                        break;
                }
            }
        }

        string UpgradeTexter(int Number)
        {
            switch (Number)
            {
                case 0:
                    return "Your pawns can now move twice.";
                    break;
                case 1:
                    return "Your pawns can now convert a captured piece.";
                    break;
                case 2:
                    return "Your rooks can now jump over a piece to capture it";
                    break;
                case 3:
                    return "Your rooks can now remove terrain obstacles by capturing them";
                    break;
                case 4:
                    return "Your bishops can also take one step forward and backwards";
                    break;
                case 5:
                    return "Your knights can resist capture once";
                    break;
                case 6:
                    return "Your queen can also move in a L Shape";
                    break;
                case 7:
                    return "Your bishops will spawn pawns everytime you capture an opposing unit";
            }

            return "error";
        }
        
        // Sprite UpgradeImage(int upgradeNumber, int slotNumber)
        // {
        //     switch (upgradeNumber)
        //     {
        //         case 0:
        //             return 
        //             break;
        //         case 1:
        //             return "Your pawns can now convert a captured piece.";
        //             break;
        //         case 2:
        //             return "Your rooks can now jump over a piece to capture it";
        //             break;
        //         case 3:
        //             return "Your rooks can now remove terrain obstacles by capturing them";
        //             break;
        //         case 4:
        //             return "Your bishop can also take one step forward and backwards";
        //             break;
        //         case 5:
        //             return "Your knights can resist capture once";
        //             break;
        //         case 6:
        //             return "Your queen can also move in a L Shape";
        //             break;
        //     }
        //
        //     return "error";
        // }
        
        
        AllocateUpgrade(1);
        AllocateUpgrade(2);
        AllocateUpgrade(3);
    }

    

    public void PawnUpgrade1()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k] != null && playerBlack[k].GetComponent<ChessPiece>().name == "pawn")
                {
                    GameObject obj = UM.Replace(playerBlack[k], "pawnS1");
                    playerBlack[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<ChessPiece>().name == "pawn")
                {
                    GameObject obj = UM.Replace(playerWhite[k], "pawnS1");
                    playerWhite[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
    }

    public void PawnUpgrade2()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k] != null && playerBlack[k].GetComponent<ChessPiece>().name == "pawn")
                {
                    GameObject obj = UM.Replace(playerBlack[k], "pawnS2");
                    playerBlack[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<ChessPiece>().name == "pawn")
                {
                    GameObject obj = UM.Replace(playerWhite[k], "pawnS2");
                    playerWhite[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
    }

    public void RookUpgrade1()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k] != null && playerBlack[k].GetComponent<ChessPiece>().name == "rook")
                {
                    GameObject obj = UM.Replace(playerBlack[k], "rookS1");
                    playerBlack[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<ChessPiece>().name == "rook")
                {
                    GameObject obj = UM.Replace(playerWhite[k], "rookS1");
                    playerWhite[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
    }
    
    public void RookUpgrade2()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k] != null && playerBlack[k].GetComponent<ChessPiece>().name == "rook")
                {
                    GameObject obj = UM.Replace(playerBlack[k], "rookS2");
                    playerBlack[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<ChessPiece>().name == "rook")
                {
                    GameObject obj = UM.Replace(playerWhite[k], "rookS2");
                    playerWhite[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
    }
    
    public void KnightUpgrade1()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k] != null && playerBlack[k].GetComponent<ChessPiece>().name == "knight")
                {
                    GameObject obj = UM.Replace(playerBlack[k], "knightS1");
                    playerBlack[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<ChessPiece>().name == "knight")
                {
                    GameObject obj = UM.Replace(playerWhite[k], "knightS1");
                    playerWhite[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
    }

    public void BishopUpgrade1()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k] != null && playerBlack[k].GetComponent<ChessPiece>().name == "bishop")
                {
                    GameObject obj = UM.Replace(playerBlack[k], "bishopS1");
                    playerBlack[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<ChessPiece>().name == "bishop")
                {
                    GameObject obj = UM.Replace(playerWhite[k], "bishopS1");
                    playerWhite[k] = obj;
                    SetPosition(obj);
                }
            }
            this.selectedUpgrade = true;
        }
    }
    
    public void BishopUpgrade2()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k] != null && playerBlack[k].GetComponent<Chessman>().name == "bB")
                {
                    playerBlack[k].GetComponent<Chessman>().name = "bSB2";
                    playerBlack[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<Chessman>().name == "wB")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSB2";
                    playerWhite[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
    }
    public void QueenUpgrade1()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k] != null && playerBlack[k].GetComponent<Chessman>().name == "bQ")
                {
                    playerBlack[k].GetComponent<Chessman>().name = "bSQ1";
                    playerBlack[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<Chessman>().name == "wQ")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSQ1";
                    playerWhite[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
    }
}
