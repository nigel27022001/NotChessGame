using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ChessPiece: MonoBehaviour
{
    public string player;
    protected Game gameState;
    public GameObject movePlate;
    protected bool upgraded;
    protected int xBoard = -1;
    protected int yBoard = -1;
    public string rawName;
    private string pastLife;
    protected bool crossedRiver;

    public void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        crossedRiver = false;
    }

    public void Update()
    {
        if (gameState.riverActive && !crossedRiver)
        {
            int riverY = gameState.riverSpot[xBoard];
            if (player == "white")
            {
                if (yBoard > riverY)
                {
                    crossedRiver = true;
                    print("crossed");
                }
            }
            else
            {
                if (yBoard < riverY)
                {
                    crossedRiver = true;
                    print("crossed");
                }
            }
        }
        if (gameState.riverActive == false && crossedRiver != false)
        {
            crossedRiver = false;
            print("over");
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
        if (!gameState.IsGameOver() &&
            gameState.GetCurrentPlayer() == player)
        {
            if (gameState.restriction == null)
            {
                DestroyMovePlates();

                InitiateMovePlates();
            }
            else
            {
                if (gameState.restriction == this.name || gameState.restriction == rawName )
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

    protected bool RiverCheck(int x, int y)
    {
        if (gameState.riverActive && crossedRiver)
        {
            if (this.player == "white")
            {
                if (gameState.riverSpot[x] < y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (gameState.riverSpot[x] > y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return true;
        }
    }

    protected void LineMovePlate(int xIncrement, int yIncrement)
    {
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (gameState.PositionOnBoard(x, y) && gameState.GetPosition(x,y) == null && RiverCheck(x,y))
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (gameState.PositionOnBoard(x, y) && RiverCheck(x, y))
        {
            if (gameState.PositionOnBoard(x, y) && gameState.GetPosition(x, y).name != "OBSTACLE")
            {
                if (gameState.GetPosition(x, y).GetComponent<ChessPiece>().player != player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    }
    
    protected void PointMovePlate(int x, int y)
    {
        if (gameState.PositionOnBoard(x, y))
        {
            GameObject cp = gameState.GetPosition(x, y);
            if (cp == null)
            {
                if (RiverCheck(x, y))
                {
                    MovePlateSpawn(x, y);
                }
            } else 
            {
                if(cp.name != "OBSTACLE" && cp.GetComponent<ChessPiece>().player != player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    }
    protected void PawnMovePlate(int x, int y)
    {
        if (gameState.PositionOnBoard(x, y))
        {
            if (gameState.GetPosition(x, y) == null )
            {
                MovePlateSpawn(x, y);
            }
                
            if (gameState.PositionOnBoard(x + 1, y) && gameState.GetPosition(x + 1, y) != null && gameState.GetPosition(x + 1, y).name != "OBSTACLE" )
            {
                if (gameState.PositionOnBoard(x + 1, y) && gameState.GetPosition(x + 1, y) != null &&
                    gameState.GetPosition(x + 1, y).GetComponent<ChessPiece>().player != player)
                {
                    MovePlateAttackSpawn(x + 1, y);
                }
            }

            if (gameState.PositionOnBoard(x - 1, y) && gameState.GetPosition(x - 1, y) != null && gameState.GetPosition(x - 1, y).name != "OBSTACLE")
            {
                if (gameState.PositionOnBoard(x - 1, y) && gameState.GetPosition(x - 1, y) != null &&
                    gameState.GetPosition(x - 1, y).GetComponent<ChessPiece>().player != player)
                {
                    MovePlateAttackSpawn(x - 1, y);
                }
            }
        }
    }

    protected void PawnMovePlate2(int x, int y)
    {
        if (gameState.PositionOnBoard(x, y))
        {
            if (gameState.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
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
            gameState.SetPositionEmpty(xBoard, yBoard);
            this.SetXBoard(x);
            this.SetYBoard(y);
            this.MovePiece();
            gameState.SetPosition(this.gameObject);
            this.DestroyMovePlates();
        }
        gameState.NextTurn();
    }
}
