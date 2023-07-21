using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SKnight1 : ChessPiece
{
    public Sprite bSN1, wSN1; 
    
    public UnitManager UM;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Cavalry\n\nCan resist capture once, capturing them back instead";
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
        this.rawName = "knight";
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
        gameState.SetPositionEmpty(xBoard, yBoard);
        newObj.SetXBoard(xBoard);
        newObj.SetYBoard(yBoard);
        newObj.MovePiece();
        gameState.SetPosition(newObj.gameObject);
        this.DestroyMovePlates();
        return true;
    }
}
