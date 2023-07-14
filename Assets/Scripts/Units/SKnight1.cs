using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKnight1 : ChessPiece
{
    public Sprite bSN1, wSN1; 
    
    public UnitManager UM;

    public void Awake()
    {
        UM = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitManager>();
        controller = GameObject.FindGameObjectWithTag("GameController");
    }
    
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wSN1;
            this.player = player;
            this.upgraded = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bSN1;
            this.player = player;
            this.upgraded = true;
        }

        this.name = "knightS1";
        this.xBoard = xCoord;
        this.yBoard = yCoord;
        this.SetCoords();
    }
    
    protected override void InitiateMovePlates()
    {
        LMovePlate();
    }
    
    private void LMovePlate()
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

    public override bool Defence()
    {
        ChessPiece newObj = UM.Replace(this.gameObject, "knight").GetComponent<ChessPiece>();
        controller.GetComponent<Game>().SetPositionEmpty(xBoard, yBoard);
        newObj.SetXBoard(xBoard);
        newObj.SetYBoard(yBoard);
        newObj.MovePiece();
        controller.GetComponent<Game>().SetPosition(newObj.gameObject);
        this.DestroyMovePlates();
        return true;
    }
}
