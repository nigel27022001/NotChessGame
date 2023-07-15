using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public Sprite bQ, wQ;
    
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wQ;
            this.player = player;
            this.upgraded = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bQ;
            this.player = player;
            this.upgraded = false;
        }
        this.name = "queen";
        this.rawName = "queen";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }


    protected override void InitiateMovePlates()
    {
        LineMovePlate(1, 0);
        LineMovePlate(0, 1);
        LineMovePlate(1, 1);
        LineMovePlate(-1, 0);
        LineMovePlate(0, -1);
        LineMovePlate(-1, -1);
        LineMovePlate(-1, 1);
        LineMovePlate(1, -1);
    }
}
