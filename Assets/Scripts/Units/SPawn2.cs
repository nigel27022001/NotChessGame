using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPawn2 : ChessPiece
{
    public Sprite bSP2, wSP2;
    public UnitManager UM;

    public void Awake()
    {
        UM = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitManager>();
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wSP2;
            this.player = player;
            this.upgraded = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bSP2;
            this.player = player;
            this.upgraded = true;
        }

        this.name = "pawnS2";
        this.rawName = "pawn";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }

    protected override void InitiateMovePlates()
    {
        switch (this.player)
        {
            case "black" :
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
            case "white" :
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

        void PawnMovePlate(int x, int y)
        {
            Game sc = controller.GetComponent<Game>();
            if (sc.PositionOnBoard(x, y))
            {
                if ((sc.GetPosition(x, y) == null || sc.GetPosition(x, y).name == "PORTAL"))
                {
                    MovePlateSpawn(x, y);
                }
                
                if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).name != "OBSTACLE" && sc.GetPosition(x + 1,y).name != "PORTAL")
                {
                    if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null &&
                        sc.GetPosition(x + 1, y).GetComponent<ChessPiece>().player != player)
                    {
                        MovePlateAttackSpawn(x + 1, y);
                    }
                }

                if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).name != "OBSTACLE" && sc.GetPosition(x - 1,y).name != "PORTAL")
                {
                    if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                        sc.GetPosition(x - 1, y).GetComponent<ChessPiece>().player != player)
                    {
                        MovePlateAttackSpawn(x - 1, y);
                    }
                }
            }
        }
    }

    public override void Attack(GameObject captured,int x, int y)
    {
        string capturedname = captured.GetComponent<ChessPiece>().name;
        if (!captured.GetComponent<ChessPiece>().Defence())
        {
           
           Destroy(captured);
           ChessPiece newObj = UM.Replace(this.gameObject, capturedname).GetComponent<ChessPiece>();
           controller.GetComponent<Game>().SetPositionEmpty(xBoard, yBoard);
           newObj.SetXBoard(x);
           newObj.SetYBoard(y);
           newObj.MovePiece();
           controller.GetComponent<Game>().SetPosition(newObj.gameObject);
           
           controller.GetComponent<Game>().NextTurn();
           this.DestroyMovePlates();
        }
    }
}
