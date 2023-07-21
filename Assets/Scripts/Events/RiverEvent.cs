using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class RiverEvent : Event
{
    [SerializeField] private GameObject RiverPrefab;
    
    public Random rnd = new Random();
    
    public override void Activate()
    {
        gameState.riverActive = true;
        int rownum = rnd.Next(3, 5);
        for (int k = 0; k <= 7; k++)
        {
            GameObject obj = Instantiate(RiverPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Rivers river = obj.GetComponent<Rivers>();
            river.name = "RIVER";
            obj.transform.parent = gameState.Gameboard;
            if (gameState.GetPosition(k, rownum) != null)
            {
                if (rownum == 4)
                {
                    if (gameState.GetPosition(k, rownum - 1) == null)
                    {
                        river.Activate(k, rownum - 1);
                        gameState.riverSpot[k] = rownum - 1;
                    }
                    else if (gameState.GetPosition(k, rownum + 1) == null)
                    {
                        river.Activate(k, rownum + 1);
                        gameState.riverSpot[k] = rownum + 1;
                    }
                }

                if (rownum == 3)
                {
                    if (gameState.GetPosition(k, rownum + 1) == null)
                    {
                        river.Activate(k, rownum + 1);
                        gameState.riverSpot[k] = rownum + 1;
                    }
                    else if (gameState.GetPosition(k, rownum - 1) == null)
                    {
                        river.Activate(k, rownum - 1);
                        gameState.riverSpot[k] = rownum - 1;
                    }
                }
            }
            else
            {
                river.Activate(k,rownum);
                gameState.riverSpot[k] = rownum;
            }
            
        }
    }
}
