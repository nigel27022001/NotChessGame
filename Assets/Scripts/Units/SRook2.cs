using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SRook2 : ChessPiece
{
    public Sprite bSR2, wSR2;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Cannon\n\nCan jump over a piece to capture another piece";
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
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }
        if (sc.PositionOnBoard(x, y) && (sc.GetPosition(x, y).name == "OBSTACLE" || sc.GetPosition(x, y).name == "RIVER"))
        {
            MovePlateAttackSpawn(x, y);
        }
        else
        {
            if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<ChessPiece>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public override void Attack(GameObject captured, int x, int y)
    {
        if (captured.name == "OBSTACLE")
        {
            Destroy(captured);
            controller.GetComponent<Game>().SetPositionEmpty(xBoard, yBoard);
            this.SetXBoard(x);
            this.SetYBoard(y);
            this.MovePiece();
            controller.GetComponent<Game>().SetPosition(this.gameObject);
            this.DestroyMovePlates();
        }
        else
        {
            if (!captured.GetComponent<ChessPiece>().Defence())
            {
                Destroy(captured);
                controller.GetComponent<Game>().SetPositionEmpty(xBoard, yBoard);
                this.SetXBoard(x);
                this.SetYBoard(y);
                this.MovePiece();
                controller.GetComponent<Game>().SetPosition(this.gameObject);
                this.DestroyMovePlates();
            }

            controller.GetComponent<Game>().NextTurn();
        }
    }
}
