using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class LavaEvent : Event
{
    [SerializeField] private GameObject LavaPrefab;
    
    public Random rnd = new Random();
    
    public override void Activate()
    {
        void CreateLava(int x, int y)
        {
            GameObject obj = Instantiate(LavaPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Lava cm = obj.GetComponent<Lava>();
            obj.transform.parent = gameState.Gameboard;
            cm.name = "OBSTACLE";
            cm.Activate(x,y);
            gameState.positions[x, y] = obj;
        }
        int i = 0;
        int noOfLava = rnd.Next(7, 13);
        while (i < noOfLava)
        {
            int randomRow = rnd.Next(2,6);
            int randomCol = rnd.Next(0,8);
            if (gameState.GetPosition(randomCol, randomRow) == null && gameState.PositionOnBoard(randomCol, randomRow))
            {
                CreateLava(randomCol,randomRow);
                i++;
            }
        }
    }
}
