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

public class Game : MonoBehaviour
{
    public GameObject Chesspiece;
    public Transform Gameboard;
    public GameObject panel;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    

    private string currentPlayer = "white";

    private int turnNumber = 1;

    private bool gameOver = false;

    private bool panelActive = false;

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
    }

    public void Update()
    {
        if (turnNumber == 10 && panelActive == false)
        {
            this.OpenPanel();
            panelActive = true;
        }

        if (turnNumber == 11 && panelActive == false) 
        {
            this.OpenPanel();
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
        GameObject.FindGameObjectWithTag("Turn").GetComponent<TextMeshProUGUI>().text = playerWinner + " wins!";
        GameObject.FindGameObjectWithTag("Player").GetComponent<TextMeshProUGUI>().text = "Tap to Restart";
    }

    public void OpenPanel()
    {
        GameObject obj = Instantiate(panel, new Vector3(0, 0, 10), Quaternion.identity);
        PanelManager panelManager = obj.GetComponent<PanelManager>();
        panelManager.openPanel();
        panelManager.GetComponent<PanelManager>().Option1Text.GetComponent<TextMeshProUGUI>().text =
            "Turns all your Pawns to be able to take two steps forward";
        panelManager.GetComponent<PanelManager>().Option1Button.GetComponent<Button>().onClick
            .AddListener(delegate
            {
                PawnUpgrade1();
                obj.SetActive(false);
            });
        panelManager.GetComponent<PanelManager>().Option2Text.GetComponent<TextMeshProUGUI>().text =
            "Your Rooks will be able to also jump over an enemy piece to capture an enemy piece behind it";
        panelManager.GetComponent<PanelManager>().Option2Button.GetComponent<Button>().onClick
            .AddListener(delegate
            {
                RookUpgrade1();
                obj.SetActive(false);
            });
        panelManager.GetComponent<PanelManager>().Option3Text.GetComponent<TextMeshProUGUI>().text =
            "Your Bishops will also be able to take one step forward and backwards";
        panelManager.GetComponent<PanelManager>().Option3Button.GetComponent<Button>().onClick
            .AddListener(delegate
            {
                BishopUpgrade1();
                obj.SetActive(false);
            });
    }

    public void PawnUpgrade1()
    {
        if (currentPlayer != "black")
        {
            for (int k = 0; k < playerBlack.Length; k++)
            {
                if (playerBlack[k].GetComponent<Chessman>().name == "bP")
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
                if (playerWhite[k].GetComponent<Chessman>().name == "wP")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSP1";
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
                if (playerBlack[k].GetComponent<Chessman>().name == "bR")
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
                if (playerWhite[k].GetComponent<Chessman>().name == "wR")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSR1";
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
                if (playerBlack[k].GetComponent<Chessman>().name == "bB")
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
                if (playerWhite[k].GetComponent<Chessman>().name == "wB")
                {
                    playerWhite[k].GetComponent<Chessman>().name = "wSB1";
                    playerWhite[k].GetComponent<Chessman>().Activate();
                }
            }
            this.selectedUpgrade = true;
        }
    }
}
