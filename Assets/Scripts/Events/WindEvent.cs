using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class WindEvent : Event
{
    public Random rnd = new Random(); 
    
    public override void Activate()
    {
        void Scramble(bool player)
        {
            GameObject[] units;
            if (player == true)
            {
                units = gameState.playerBlack;
            }
            else
            {
                units = gameState.playerWhite;
            }
            int firstNum = rnd.Next(-1, 16);
            int secondNum = rnd.Next(-1, 16);
            while (units[firstNum] == null || units[firstNum].GetComponent<ChessPiece>().name == "king")
            {
                    firstNum = rnd.Next(-1, 16);
            }
            while (units[secondNum] == null || units[secondNum].GetComponent<ChessPiece>().name == "king")
            {
                    secondNum = rnd.Next(-1, 16);
            }
            GameObject firstUnitObj = units[firstNum];
            GameObject secondUnitObj = units[secondNum];
            ChessPiece firstUnit = firstUnitObj.GetComponent<ChessPiece>();
            ChessPiece secondUnit = secondUnitObj.GetComponent<ChessPiece>();
            int firstUnitX = firstUnit.GetXBoard();
            int firstUnitY = firstUnit.GetYBoard();
            int secondUnitX = secondUnit.GetXBoard();
            int secondUnitY = secondUnit.GetYBoard();
            firstUnit.SetXBoard(secondUnitX);
            firstUnit.SetYBoard(secondUnitY);
            secondUnit.SetXBoard(firstUnitX);
            secondUnit.SetYBoard(firstUnitY);
            firstUnit.MovePiece();
            secondUnit.MovePiece();
            gameState.SetPosition(firstUnitObj);
            gameState.SetPosition(secondUnitObj);
        }
        Scramble(true);
        Scramble(false);
        Scramble(true);
        Scramble(false);
        Scramble(true);
        Scramble(false);
    }
}
