using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SRook1 : ChessPiece
{
    public Sprite bSR1, wSR1;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Cannon\n\nCan jump over a piece and river to capture another";
    }
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wSR1;
            this.player = player;
            this.upgraded = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bSR1;
            this.player = player;
            this.upgraded = true;
        }

        this.name = "rookS1";
        this.rawName = "rook";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }

    protected override void InitiateMovePlates()
    {
        SpecialRook1Plate(0, 1);
        SpecialRook1Plate(1, 0);
        SpecialRook1Plate(0, -1);
        SpecialRook1Plate(-1, 0);
    }

    void SpecialRook1Plate(int xIncrement, int yIncrement)
    {
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;
        while (gameState.PositionOnBoard(x, y) && gameState.GetPosition(x,y) == null )
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (gameState.PositionOnBoard(x, y) && gameState.GetPosition(x, y).name != "OBSTACLE")
        {
            if (gameState.PositionOnBoard(x, y) && gameState.GetPosition(x, y).GetComponent<ChessPiece>().player != player)
            {
                x += xIncrement;
                y += yIncrement;
            }
            if (gameState.PositionOnBoard(x, y))
            {
                while (gameState.PositionOnBoard(x, y))
                {
                    if (gameState.GetPosition(x, y) != null && gameState.GetPosition(x, y).GetComponent<ChessPiece>().player != player 
                                                            && gameState.GetPosition(x, y).name != "OBSTACLE")
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
}
