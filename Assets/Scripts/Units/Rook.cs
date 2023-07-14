using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public Sprite bR, wR;
    
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wR;
            this.player = player;
            this.upgraded = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bR;
            this.player = player;
            this.upgraded = false;
        }
        this.name = "rook";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }


    protected override void InitiateMovePlates()
    {
        LineMovePlate(0, 1);
        LineMovePlate(1, 0);
        LineMovePlate(0, -1);
        LineMovePlate(-1, 0);
    }
}
