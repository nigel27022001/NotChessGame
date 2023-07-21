using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = System.Random;

public class SBishop2 : ChessPiece
{
    public Sprite bSB2, wSB2;

    public UnitManager UM;

    public Random rnd = new Random();

    public override void Describe()
    {
        GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TextMeshProUGUI>().text = "Necromancer\n\nCan convert captured pieces into your pawns ";
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
            gameState.SetPositionEmpty(xBoard, yBoard);
            this.SetXBoard(x);
            this.SetYBoard(y);
            this.MovePiece();
            gameState.SetPosition(this.gameObject);
            this.PawnNecromancy(1);
            gameState.NextTurn();
            this.DestroyMovePlates();
        }
    }
    
    private void PawnNecromancy(int number)
    {
        int RowCheck(int rowNum)
        {
        
            bool checkIfNotFull = false;
            for (int k = 0; k <= 7; k++)
            {
                if (gameState.GetPosition(k, rowNum) == null)
                {
                    checkIfNotFull = true;
                }
            }
            if (!checkIfNotFull)
            {
                return -1;
            }
            else
            {
                int rndCol = rnd.Next(0, 8);
                while (gameState.GetPosition(rndCol, rowNum) != null)
                {
                    rndCol = rnd.Next(0, 8);
                }

                return rndCol;
            }
        }
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
            int colNum = RowCheck(spawnRow);
            if (colNum == -1)
            {
                while (colNum == -1)
                {
                    colNum = RowCheck(spawnRow);
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
            gameState.SetPosition(cm);
            curr++;
        }
    }
}
