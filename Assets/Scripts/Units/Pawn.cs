using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public Sprite bP, wP;
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wP;
            this.player = player;
            this.upgraded = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bP;
            this.player = player;
            this.upgraded = false;
        }
        this.name = "pawn";
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
                    PawnMovePlate2(xBoard, yBoard - 2);
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
                    PawnMovePlate2(xBoard, yBoard + 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                }
                break;
        }
    }
}
