using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SRook1 : ChessPiece
{
    public Sprite bSR1, wSR1;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Recon\n\nCan traverse over terrains";
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
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;
        print(x + " " + y);
        while (sc.PositionOnBoard(x, y) && (sc.GetPosition(x,y) == null || sc.GetPosition(x,y).name == "PORTAL")|| (sc.GetPosition(x,y).name == "RIVER" && this.crossedRiver == false))
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).name != "OBSTACLE")
        {
            if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<ChessPiece>().player != player)
            {
                //MovePlateAttackSpawn(x, y);
                x += xIncrement;
                y += yIncrement;
            }
            if (sc.PositionOnBoard(x, y))
            {
                while (sc.PositionOnBoard(x, y))
                {
                    if (sc.GetPosition(x, y) != null && sc.GetPosition(x, y).GetComponent<ChessPiece>().player != player 
                                                     && sc.GetPosition(x, y).name != "OBSTACLE")
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
