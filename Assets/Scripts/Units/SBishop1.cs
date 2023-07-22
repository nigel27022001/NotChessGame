using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SBishop1 : ChessPiece
{
    public Sprite bSB1, wSB1;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Priest\n\nCan also take one step forward and backward";
    }

    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wSB1;
            this.player = player;
            this.upgraded = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bSB1;
            this.player = player;
            this.upgraded = true;
        }
        
        this.name = "bishopS1";
        this.rawName = "bishop";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }
    
    protected override void InitiateMovePlates()
    {
        LineMovePlate(1, 1);
        LineMovePlate(1, -1);
        LineMovePlate(-1, 1);
        LineMovePlate(-1, -1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard, yBoard + 1);
    }
}
