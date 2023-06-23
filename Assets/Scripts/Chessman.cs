using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    // References
    public GameObject controller;
    public GameObject movePlate;

    public AudioSource audio;
    

    // Positions
    private int xBoard = -1;
    private int yBoard = -1;

    // Variable to keep track of "black" or "white" player
    private string player;

    //References for all the sprites that the chesspiece can be
    public Sprite bQ, bN, bB, bK, bR, bP, bSP2, bSP1;
    public Sprite wQ, wN, wB, wK, wR, wP, wSP2, wSP1;
    //References for Obstacle Sprites
    public Sprite lava;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        // takes the instantiated the location and adjust the transform
        SetCoords();

        switch (this.name)
        {   
            case "LAVA" :
                this.GetComponent<SpriteRenderer>().sprite = lava;
                player = "OBSTACLE";
                break;
            case "wSP1" :
                this.GetComponent<SpriteRenderer>().sprite = wSP1;
                player = "white";
                break;
            case "bSP1" :
                this.GetComponent<SpriteRenderer>().sprite = bSP1;
                player = "black";
                break;
            case "wSP2" :
                this.GetComponent<SpriteRenderer>().sprite = wSP2;
                player = "white";
                break;
            case "bSP2" :
                this.GetComponent<SpriteRenderer>().sprite = bSP2;
                player = "black";
                break;
            case "wSB1" :
                this.GetComponent<SpriteRenderer>().sprite = wB;
                this.GetComponent<SpriteRenderer>().color = Color.red;
                player = "white";
                break;
            case "bSB1" :
                this.GetComponent<SpriteRenderer>().sprite = wB;
                this.GetComponent<SpriteRenderer>().color = Color.red;
                player = "black";
                break;
            case "wSR1" :
                this.GetComponent<SpriteRenderer>().sprite = wR;
                this.GetComponent<SpriteRenderer>().color = Color.red;
                player = "white";
                break;
            case "bQ" :
                this.GetComponent<SpriteRenderer>().sprite = bQ;
                player = "black";
                break;
            case "bN" : this.GetComponent<SpriteRenderer>().sprite = bN;
                player = "black";
                break;
            case "bB" : this.GetComponent<SpriteRenderer>().sprite = bB;
                player = "black";
                break;
            case "bK" : this.GetComponent<SpriteRenderer>().sprite = bK;
                player = "black";
                break;
            case "bR" : this.GetComponent<SpriteRenderer>().sprite = bR;
                player = "black";
                break;
            case "bP" : this.GetComponent<SpriteRenderer>().sprite = bP;
                player = "black";
                break;
            case "wQ" : this.GetComponent<SpriteRenderer>().sprite = wQ;
                player = "white";
                break;
            case "wN" : this.GetComponent<SpriteRenderer>().sprite = wN;
                player = "white";
                break;
            case "wB" : this.GetComponent<SpriteRenderer>().sprite = wB;
                player = "white";
                break;
            case "wK" : this.GetComponent<SpriteRenderer>().sprite = wK;
                player = "white";
                break;
            case "wR" : this.GetComponent<SpriteRenderer>().sprite = wR;
                player = "white";
                break;
            case "wP" : this.GetComponent<SpriteRenderer>().sprite = wP;
                player = "white";
                break;
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

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() &&
            controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "wSP1" :
            case "bSP1" :
                PawnMovePlate(xBoard, yBoard + 2);
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "wSB1" :
            case "bSB1" :
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                PointMovePlate(xBoard, yBoard - 1);
                PointMovePlate(xBoard, yBoard + 1);
                break;
            case "bSR1" :
            case "wSR1" :
                SpecialRook1Plate(0, 1);
                SpecialRook1Plate(1, 0);
                SpecialRook1Plate(0, -1);
                SpecialRook1Plate(-1, 0);
                break;
            case "bQ":
            case "wQ":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "bN":
            case "wN":
                LMovePlate();
                break;
            case "bB" :
            case "wB" :
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "bK" :
            case "wK" :
                SurroundMovePlate();
                break;
            case "bR" :
            case "wR" :
                LineMovePlate(0, 1);
                LineMovePlate(1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, 0);
                break;
            case "bP" :
            case "bSP2" :
                if (yBoard == 6)
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                    PawnMovePlate(xBoard, yBoard - 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                }
                break;
            case "wP" :
            case "wSP2" :
                if (yBoard == 1)
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                    PawnMovePlate(xBoard, yBoard + 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                }
                break;
        }

          void LineMovePlate(int xIncrement, int yIncrement)
        {
            Game sc = controller.GetComponent<Game>();

            int x = xBoard + xIncrement;
            int y = yBoard + yIncrement;

            while (sc.PositionOnBoard(x, y) && sc.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x, y);
                x += xIncrement;
                y += yIncrement;
            }

            if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player
                && sc.GetPosition(x, y).GetComponent<Chessman>().player != "OBSTACLE")
            {
                MovePlateAttackSpawn(x, y);
            }
        }

         void SpecialRook1Plate(int xIncrement, int yIncrement)
         {
             Game sc = controller.GetComponent<Game>();

             int x = xBoard + xIncrement;
             int y = yBoard + yIncrement;

             while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
             {
                 MovePlateSpawn(x, y);
                 x += xIncrement;
                 y += yIncrement;
             }
             
             if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player
                 && sc.GetPosition(x, y).GetComponent<Chessman>().player != "OBSTACLE")
             {
                 MovePlateAttackSpawn(x, y);
                 x += xIncrement;
                 y += yIncrement;
             }

             if (this.name == "wSR1" || this.name == "bSR1")
             {
                 if (sc.PositionOnBoard(x, y))
                 {
                     while (sc.PositionOnBoard(x, y))
                     {
                         if (sc.GetPosition(x,y) != null && sc.GetPosition(x, y).GetComponent<Chessman>().player != player
                             && sc.GetPosition(x, y).GetComponent<Chessman>().player != "OBSTACLE")
                         {
                             MovePlateAttackSpawn(x, y);
                             return;
                         }
             
                         x += xIncrement;
                         y += yIncrement;
                     }
                 }
             }
         }

         void LMovePlate()
        {
            PointMovePlate(xBoard + 1, yBoard + 2);
            PointMovePlate(xBoard - 1, yBoard + 2);
            PointMovePlate(xBoard + 2, yBoard + 1);
            PointMovePlate(xBoard + 2, yBoard - 1);
            PointMovePlate(xBoard + 1, yBoard - 2);
            PointMovePlate(xBoard - 1, yBoard - 2);
            PointMovePlate(xBoard - 2, yBoard + 1);
            PointMovePlate(xBoard - 2, yBoard - 1);
        }

         void SurroundMovePlate()
        {
            PointMovePlate(xBoard, yBoard + 1);
            PointMovePlate(xBoard, yBoard - 1);
            PointMovePlate(xBoard - 1, yBoard - 1);
            PointMovePlate(xBoard - 1, yBoard);
            PointMovePlate(xBoard - 1, yBoard + 1);
            PointMovePlate(xBoard + 1, yBoard - 1);
            PointMovePlate(xBoard + 1, yBoard);
            PointMovePlate(xBoard + 1, yBoard + 1);
        }

         void PointMovePlate(int x, int y)
        {
            Game sc = controller.GetComponent<Game>();
            if (sc.PositionOnBoard(x, y))
            {
                GameObject cp = sc.GetPosition(x, y);
                if (cp == null)
                {
                    MovePlateSpawn(x, y);
                } else if (cp.GetComponent<Chessman>().player != player && cp.GetComponent<Chessman>().player != "OBSTACLE")
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
        
         void PawnMovePlate(int x, int y)
        {
            Game sc = controller.GetComponent<Game>();
            if (sc.PositionOnBoard(x, y))
            {
                if (sc.GetPosition(x, y) == null)
                {
                    MovePlateSpawn(x, y);
                }

                if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null &&
                    sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player && 
                    sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != "OBSTACLE")
                {
                    MovePlateAttackSpawn(x + 1, y);
                }
                
                if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                    sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player &&
                    sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != "OBSTACLE")
                {
                    MovePlateAttackSpawn(x - 1, y);
                }
            }
                
        }

         MovePlate MovePlateSpawn(int matrixX, int matrixY)
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
        
         void MovePlateAttackSpawn(int matrixX, int matrixY)
         {
             MovePlate mpScript = MovePlateSpawn(matrixX, matrixY);
             mpScript.attack = true;
         }
    }
    //Method for when converting current piece into captured piece
    public void Convert(String unitName)
    {
        char colour = unitName.ElementAt(0);
        if (colour == 'b')
        {
            string newUnit = 'w' + unitName.Substring(1);
            this.name = newUnit;
            this.Activate();
        }
        else if (colour == 'w')
        {
            string newUnit = 'b' + unitName.Substring(1);
            this.name = newUnit;
            this.Activate();
        }
        
    }
}
