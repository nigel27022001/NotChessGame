using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SBishop2 : ChessPiece
{
    public Sprite bSB2, wSB2;

    public UnitManager UM;

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Necromancer\n\nCan convert captured pieces into your pawns ";
    }
    public void Awake()
    {
        UM = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitManager>();
        controller = GameObject.FindGameObjectWithTag("GameController");
    }
    public override void Activate(string player, int xCoord, int yCoord)
    {
        if (player == "white")
        {
            this.GetComponent<SpriteRenderer>().sprite = wSB2;
            this.player = player;
            this.upgraded = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = bSB2;
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
    }
    
    public override void Attack(GameObject captured,int x, int y)
    {
        if (!captured.GetComponent<ChessPiece>().Defence())
        {
            Destroy(captured);
            controller.GetComponent<Game>().SetPositionEmpty(xBoard, yBoard);
            this.SetXBoard(x);
            this.SetYBoard(y);
            this.MovePiece();
            controller.GetComponent<Game>().SetPosition(this.gameObject);
            this.PawnNecromancy(1);
            controller.GetComponent<Game>().NextTurn();
            this.DestroyMovePlates();
        }
    }
    
    private void PawnNecromancy(int number)
    {
        int spawnNum = number;
        int curr = 0;
        while (curr < number)
        {
            int spawnRow;
            if (this.player == "white")
            {
                spawnRow = 1;
            }
            else
            {
                spawnRow = 6;
            }
            int colNum = controller.GetComponent<Game>().RowCheck(spawnRow);
            if (colNum == -1)
            {
                while (colNum == -1)
                {
                    colNum = controller.GetComponent<Game>().RowCheck(spawnRow);
                    if (player == "w")
                    {
                        spawnRow++;
                    }
                    else
                    {
                        spawnRow--;
                    }
                }
            }

            GameObject cm = UM.Create("pawn", this.player, colNum, spawnRow);
            controller.GetComponent<Game>().SetPosition(cm);
            curr++;
        }
    }
}
