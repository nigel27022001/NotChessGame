using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPawn1 : ChessPiece
{
    public Sprite bSP1, wSP1;

    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wSP1;
            this.player = player;
            this.upgraded = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bSP1;
            this.player = player;
            this.upgraded = true;
        }

        this.name = "pawnS1";
        this.rawName = "pawn";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }

    protected override void InitiateMovePlates()
    {
        switch (this.player)
        {
            case "white":
                PawnMovePlate(xBoard, yBoard + 2);
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "black":
                PawnMovePlate(xBoard, yBoard - 2);
                PawnMovePlate(xBoard, yBoard - 1);
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

                if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null &&
                    sc.GetPosition(x + 1, y).name != "OBSTACLE" && sc.GetPosition(x + 1, y).name != "PORTAL")
                {
                    if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null &&
                        sc.GetPosition(x + 1, y).GetComponent<ChessPiece>().player != player)
                    {
                        MovePlateAttackSpawn(x + 1, y);
                    }
                }

                if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                    sc.GetPosition(x - 1, y).name != "OBSTACLE" && sc.GetPosition(x - 1, y).name != "PORTAL")
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
}
