using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopUpgrade2 : Upgradeable
{
    public Sprite wSB2, bSB2;
    public override void Upgrade()
    {
        if (gameState.currentPlayer != "black")
        {
                for (int k = 0; k < gameState.playerBlack.Length; k++)
                {
                    if (gameState.playerBlack[k] != null && gameState.playerBlack[k].GetComponent<ChessPiece>().rawName == "bishop")
                    {
                        GameObject obj = UM.Replace(gameState.playerBlack[k], "bishopS2");
                        gameState.playerBlack[k] = obj;
                        gameState.SetPosition(obj);
                    }
                }
                gameState.selectedUpgrade = true;
        }
        else if (gameState.currentPlayer != "white")
        {
                for (int k = 0; k < gameState.playerWhite.Length; k++)
                {
                    if (gameState.playerWhite[k] != null && gameState.playerWhite[k].GetComponent<ChessPiece>().rawName == "bishop")
                    {
                        GameObject obj = UM.Replace(gameState.playerWhite[k], "bishopS2");
                        gameState.playerWhite[k] = obj;
                        gameState.SetPosition(obj);
                    }
                }
                gameState.selectedUpgrade = true;
        }
    }
}
