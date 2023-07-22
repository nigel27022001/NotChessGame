using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SRook2 : ChessPiece
{
    public Sprite bSR2, wSR2;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Recon\n\nCan remove hard terrains";
    }
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wSR2;
            this.player = player;
            this.upgraded = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bSR2;
            this.player = player;
            this.upgraded = true;
        }

        this.name = "rookS2";
        this.rawName = "rook";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }

    protected override void InitiateMovePlates()
    {

        SpecialRook2Plate(0, 1);
        SpecialRook2Plate(1, 0);
        SpecialRook2Plate(0, -1);
        SpecialRook2Plate(-1, 0);
    }

    void SpecialRook2Plate(int xIncrement, int yIncrement)
    {
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (gameState.PositionOnBoard(x, y) && gameState.GetPosition(x, y) == null && RiverCheck(x,y))
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (gameState.PositionOnBoard(x, y) && RiverCheck(x, y))
        {
            if (gameState.GetPosition(x, y) != null)
            {
                if (gameState.GetPosition(x, y).name == "OBSTACLE")
                {
                    MovePlateAttackSpawn(x, y);
                } else if (gameState.GetPosition(x, y).GetComponent<ChessPiece>().player != player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    }

    public override void Attack(GameObject captured, int x, int y)
    {
        if (captured.name == "OBSTACLE")
        {
            Destroy(captured);
            gameState.SetPositionEmpty(xBoard, yBoard);
            this.SetXBoard(x);
            this.SetYBoard(y);
            this.MovePiece();
            gameState.SetPosition(this.gameObject);
            this.DestroyMovePlates();
        }
        else
        {
            if (!captured.GetComponent<ChessPiece>().Defence())
            {
                Destroy(captured);
                gameState.SetPositionEmpty(xBoard, yBoard);
                this.SetXBoard(x);
                this.SetYBoard(y);
                this.MovePiece();
                gameState.SetPosition(this.gameObject);
                this.DestroyMovePlates();
            }
            gameState.NextTurn();
        }
    }
}
