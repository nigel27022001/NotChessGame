using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiMagicEvent : Event
{

    private int turnActivated;

    private int turnDeactivated;

    private bool flag;

    public override void Activate()
    {
        int duration = 5;
        turnActivated = gameState.turnNumber;
        turnDeactivated = gameState.turnNumber + duration;
        GameObject[] blackUnits = gameState.playerBlack;
        GameObject[] whiteUnits = gameState.playerWhite;
        for (int k = 0; k < blackUnits.Length; k++)
        {
            if (blackUnits[k] != null)
            {
                string oldName = blackUnits[k].GetComponent<ChessPiece>().name;
                GameObject downgradedUnit = UM.Replace(blackUnits[k], blackUnits[k].GetComponent<ChessPiece>().rawName);
                downgradedUnit.GetComponent<ChessPiece>().SetPastLife(oldName);
                blackUnits[k] = downgradedUnit;
                gameState.SetPosition(blackUnits[k]);
            }
        }
        for (int k = 0; k < whiteUnits.Length; k++)
        {
            if (whiteUnits[k] != null)
            {
                string oldName = whiteUnits[k].GetComponent<ChessPiece>().name;
                GameObject downgradedUnit = UM.Replace(whiteUnits[k], whiteUnits[k].GetComponent<ChessPiece>().rawName);
                downgradedUnit.GetComponent<ChessPiece>().SetPastLife(oldName);
                whiteUnits[k] = downgradedUnit;
                gameState.SetPosition(whiteUnits[k]);
            }
        }
        flag = true;
    }

    public void Deactivate()
    {
        GameObject[] blackUnits = gameState.playerBlack;
        GameObject[] whiteUnits = gameState.playerWhite;
        for (int k = 0; k < blackUnits.Length; k++)
        {
            if (blackUnits[k] != null)
            {
                string oldName = blackUnits[k].GetComponent<ChessPiece>().RetrievePastLife();
                GameObject revertedUnit = UM.Replace(blackUnits[k], oldName);
                blackUnits[k] = revertedUnit;
                gameState.SetPosition(blackUnits[k]);
            }
        }
        for (int k = 0; k < whiteUnits.Length; k++)
        {
            if (whiteUnits[k] != null)
            {
                string oldName = whiteUnits[k].GetComponent<ChessPiece>().RetrievePastLife();
                GameObject revertedUnit = UM.Replace(whiteUnits[k], oldName);
                whiteUnits[k] = revertedUnit;
                gameState.SetPosition(whiteUnits[k]);
            }
        }

        flag = false;
    }

    public void Update()
    {
        if (gameState.turnNumber == turnDeactivated && flag)
        {
            Deactivate();
            Destroy(this);
        }
    }
}
