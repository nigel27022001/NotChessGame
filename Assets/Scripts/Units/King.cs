using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public Sprite bK, wK;
    
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wK;
            this.player = player;
            this.upgraded = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bK;
            this.player = player;
            this.upgraded = false;
        }
        this.name = "king";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }


    protected override void InitiateMovePlates()
    {
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
        SurroundMovePlate();
    }
}
