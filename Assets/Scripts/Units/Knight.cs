using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public Sprite bN, wN;
    
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wN;
            this.player = player;
            this.upgraded = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bN;
            this.player = player;
            this.upgraded = false;
        }
        this.name = "knight";
        this.rawName = "knight";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }


    protected override void InitiateMovePlates()
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
}
