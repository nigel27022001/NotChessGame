using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ChessPiece: MonoBehaviour
{
    public string player;
    public GameObject controller;
    public GameObject movePlate;
    public bool upgraded;
    public int xBoard = -1;
    public int yBoard = -1;
    public string rawName;
    private string pastLife;
    public bool crossedRiver;

    public void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        crossedRiver = false;
    }

    public void Update()
    {
        Game gameState = controller.GetComponent<Game>();
        if (gameState.riverActive && !crossedRiver)
        {
            int riverY = gameState.riverSpot[xBoard];
            if (player == "white")
            {
                if (yBoard > riverY)
                {
                    crossedRiver = true;
                    print("crossed river");
                }
            }
            else
            {
                if (yBoard < riverY)
                {
                    crossedRiver = true;
                    print("crossed river");
                }
            }
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;
        
        x *= 1.11f;
        y *= 1.11f;
        
        x += -3.9f;
        y += -3.9f;
        
        this.transform.position = new Vector3(x, y, -1.0f);
    }
    
    public void MovePiece()
    {
        float x = xBoard;
        float y = yBoard;
        
        x *= 1.11f;
        y *= 1.11f;
        
        x += -3.9f;
        y += -3.9f;
        MoveAnimation move = this.GetComponent<MoveAnimation>();
        move.SetVector(x, y);
    }
    
    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    public void SetPastLife(string pastName)
    {
        this.pastLife = pastName;
    }

    public string RetrievePastLife()
    {
        return pastLife;
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }
    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() &&
            controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            if (controller.GetComponent<Game>().restriction == null)
            {
                DestroyMovePlates();

                InitiateMovePlates();
            }
            else
            {
                if (controller.GetComponent<Game>().restriction == this.name || controller.GetComponent<Game>().restriction == rawName )
                {
                    DestroyMovePlates();

                    InitiateMovePlates();
                }
            }
        }
    }
    private void OnMouseOver()
    {
        this.Describe();
    }
    private void OnMouseExit()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "";
    }
    protected MovePlate MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;
            
        x *= 1.11f;
        y *= 1.11f;

        x += -3.9f;
        y += -3.9f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
        return mpScript;
    }
    protected void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && (sc.GetPosition(x,y) == null || sc.GetPosition(x,y).name == "PORTAL" || (sc.GetPosition(x,y).name == "RIVER" && this.crossedRiver == false)))
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).name != "OBSTACLE")
        {
            if (sc.GetPosition(x, y).GetComponent<ChessPiece>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            } 
        }
            
    }
    
    protected void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            if (cp == null || cp.name == "PORTAL" || (sc.GetPosition(x,y).name == "RIVER" && this.crossedRiver == false))
            {
                MovePlateSpawn(x, y);
            } else if (cp != null)
            {
                if(cp.name != "OBSTACLE" && cp.GetComponent<ChessPiece>().player != player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    }
    protected void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        MovePlate mpScript = MovePlateSpawn(matrixX, matrixY);
        mpScript.attack = true;
    }

    public abstract void Activate(string player, int xCoord, int yCoord);

    public virtual void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Basic Unit";
    }
    
    protected abstract void InitiateMovePlates();
    
    // Use "new" keyword on method to override
    public virtual bool Defence()
    {
        return false;
    }

    public virtual void Attack(GameObject captured, int x, int y)
    {
        if (!captured.GetComponent<ChessPiece>().Defence())
        {
            Destroy(captured);
            controller.GetComponent<Game>().SetPositionEmpty(xBoard, yBoard);
            this.SetXBoard(x);
            this.SetYBoard(y);
            this.MovePiece();
            controller.GetComponent<Game>().SetPosition(this.gameObject);
            this.DestroyMovePlates();
        }
        controller.GetComponent<Game>().NextTurn();
    }
}
