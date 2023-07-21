using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SPawn2 : ChessPiece
{
    public Sprite bSP2, wSP2;
    public UnitManager UM;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Shapeshifter\n\nCan transform into captured piece";
    }
    public void Awake()
    {
        UM = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitManager>();
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
    }

    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wSP2;
            this.player = player;
            this.upgraded = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bSP2;
            this.player = player;
            this.upgraded = true;
        }

        this.name = "pawnS2";
        this.rawName = "pawn";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }

    protected override void InitiateMovePlates()
    {
        switch (this.player)
        {
            case "black":
                if (yBoard == 6)
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                    PawnMovePlate2(xBoard, yBoard - 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                }

                break;
            case "white":
                if (yBoard == 1)
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                    PawnMovePlate2(xBoard, yBoard + 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                }

                break;
        }
    }

    public override void Attack(GameObject captured,int x, int y)
    {
        string capturedname = captured.GetComponent<ChessPiece>().name;
        if (!captured.GetComponent<ChessPiece>().Defence())
        {
           
           Destroy(captured);
           ChessPiece newObj = UM.Replace(this.gameObject, capturedname).GetComponent<ChessPiece>();
           gameState.SetPositionEmpty(xBoard, yBoard);
           newObj.SetXBoard(x);
           newObj.SetYBoard(y);
           newObj.MovePiece();
           gameState.SetPosition(newObj.gameObject);
           
           gameState.NextTurn();
           this.DestroyMovePlates();
        }
    }
}
