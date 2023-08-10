using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SPawn1 : ChessPiece
{
    public Sprite bSP1, wSP1;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Guard\n\nCan move two tiles at once";
    }
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
                if (gameState.GetPosition(xBoard, yBoard + 1) == null)
                {
                    PawnMovePlate(xBoard, yBoard + 2);   
                }
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "black":
                if (gameState.GetPosition(xBoard, yBoard - 1) == null)
                {
                    PawnMovePlate(xBoard, yBoard - 2);   
                }
                PawnMovePlate(xBoard, yBoard - 1);
                break;
        }
    }
}
