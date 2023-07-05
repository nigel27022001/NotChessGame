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
    public GameObject LavaObject;
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

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("wR", 0, 0), Create("wN", 1, 0), Create("wB", 2, 0), Create("wQ", 3, 0),
            Create("wK", 4, 0), Create("wB", 5, 0), Create("wN", 6, 0), Create("wR", 7, 0),
            Create("wP", 0, 1), Create("wP", 1, 1), Create("wP", 2, 1), Create("wP", 3, 1),
            Create("wP", 4, 1), Create("wP", 5, 1), Create("wP", 6, 1), Create("wP", 7, 1),
        };
        playerBlack = new GameObject[]
        {
            Create("bR", 0, 7), Create("bN", 1, 7), Create("bB", 2, 7), Create("bQ", 3, 7),
            Create("bK", 4, 7), Create("bB", 5, 7), Create("bN", 6, 7), Create("bR", 7, 7),
            Create("bP", 0, 6), Create("bP", 1, 6), Create("bP", 2, 6), Create("bP", 3, 6),
            Create("bP", 4, 6), Create("bP", 5, 6), Create("bP", 6, 6), Create("bP", 7, 6),
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
        GameObject obj = Instantiate(Chesspiece, new Vector3(0, 0, -1), quaternion.identity);
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
        Chessman cm = obj.GetComponent<Chessman>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
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
        if (turnNumber == 1 && !eventDone)
        {
            GameObject obj = Instantiate(this.EventManager);
            ObstacleEventManager OEM = obj.GetComponent<ObstacleEventManager>();
            OEM.LavaEvent(10);
            eventDone = true;
        }
        if (turnNumber == 10 && panelActive == false)
        {
            this.UpgradePanel();
            panelActive = true;
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
        GameObject obj = Instantiate(panel, new Vector3(0, 0, 10), Quaternion.identity);
        PanelManager panelManager = obj.GetComponent<PanelManager>();
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
                        UpgradeTexter(curr);
                    panelManager.GetComponent<PanelManager>().Option1Button.GetComponent<Button>().onClick
                        .AddListener(delegate
                        {
                            SelectUpgrades(curr);
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
                if (playerBlack[k] != null && playerBlack[k].GetComponent<Chessman>().name == "bP")
                {
                    playerBlack[k].GetComponent<Chessman>().name = "bSP1";
                    playerBlack[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<Chessman>().name == "wP")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSP1";
                    playerWhite[k].GetComponent<Chessman>().Activate();
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
                if (playerBlack[k] != null && playerBlack[k].GetComponent<Chessman>().name == "bP")
                {
                    playerBlack[k].GetComponent<Chessman>().name = "bSP2";
                    playerBlack[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<Chessman>().name == "wP")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSP2";
                    playerWhite[k].GetComponent<Chessman>().Activate();
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
                if (playerBlack[k] != null && playerBlack[k].GetComponent<Chessman>().name == "bR")
                {
                    playerBlack[k].GetComponent<Chessman>().name = "bSR1";
                    playerBlack[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<Chessman>().name == "wR")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSR1";
                    playerWhite[k].GetComponent<Chessman>().Activate();
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
                if (playerBlack[k] != null && playerBlack[k].GetComponent<Chessman>().name == "bR")
                {
                    playerBlack[k].GetComponent<Chessman>().name = "bSR2";
                    playerBlack[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<Chessman>().name == "wR")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSR2";
                    playerWhite[k].GetComponent<Chessman>().Activate();
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
                if (playerBlack[k] != null && playerBlack[k].GetComponent<Chessman>().name == "bN")
                {
                    playerBlack[k].GetComponent<Chessman>().name = "bSN1";
                    playerBlack[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
        else if (currentPlayer != "white")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerWhite[k] != null && playerWhite[k].GetComponent<Chessman>().name == "wN")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSN1";
                    playerWhite[k].GetComponent<Chessman>().Activate();
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
                if (playerBlack[k] != null && playerBlack[k].GetComponent<Chessman>().name == "bB")
                {
                    playerBlack[k].GetComponent<Chessman>().name = "bSB1";
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
                    playerWhite[k].GetComponent<Chessman>().name = "wSB1";
                    playerWhite[k].GetComponent<Chessman>().Activate();
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
