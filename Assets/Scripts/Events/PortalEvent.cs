using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class PortalEvent : Event
{
    [SerializeField] private GameObject PortalPrefab;
    [SerializeField] private GameObject MountainPrefab;
    public Random rnd = new Random();

    public override void Activate()
    {
        Portal CreatePortal(int x, int y)
        {
            GameObject obj = Instantiate(PortalPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Portal cm = obj.GetComponent<Portal>();
            obj.transform.parent = gameState.Gameboard;
            cm.name = "PORTAL";
            cm.Activate(x,y,1);
            gameState.portalPositions[x, y] = cm;
            return cm;
        }
        Portal CreatePairedPortal(int x, int y, Portal pairing)
        {
            GameObject obj = Instantiate(PortalPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Portal cm = obj.GetComponent<Portal>();
            obj.transform.parent = gameState.Gameboard;
            cm.name = "PORTAL";
            cm.Activate(x,y,2,pairing);
            gameState.portalPositions[x, y] = cm;
            return cm;
        }
    
        void CreateMountain(int x, int y)
        {
            GameObject obj = Instantiate(MountainPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Mountains cm = obj.GetComponent<Mountains>();
            obj.transform.parent = gameState.Gameboard;
            cm.name = "OBSTACLE";
            cm.Activate(x,y);
            gameState.positions[x, y] = obj;
        }

        int mainRow = rnd.Next(3, 5);
        int start = 2;
        Portal portal1 = null;
        for (int k = 2; k <= 5; k++)
        {
            if (k == 2)
            {
                if (gameState.GetPosition(k, mainRow) == null)
                {
                    CreateMountain(k, mainRow);
                    print(1);
                }
                else
                {
                    if (mainRow == 3)
                    {
                        if (gameState.GetPosition(k, 4) == null)
                        {
                            CreateMountain(k, 4);
                            print(1);
                        }
                    }
                    else
                    {
                        if (gameState.GetPosition(k, 3) == null)
                        {
                            CreateMountain(k, 3);
                            print(1);
                        }
                    }
                }
                if (mainRow == 3)
                {
                    if (gameState.GetPosition(k, 4) == null)
                    {
                        portal1 = CreatePortal(k, 4);
                    }
                    else if (gameState.GetPosition(k, 5) == null)
                    {
                        portal1 = CreatePortal(k, 5);
                    }
                    else if (gameState.GetPosition(1, 4) == null)
                    {
                        portal1 = CreatePortal(1, 4);
                    }
                    else if (gameState.GetPosition(1, 5) == null)
                    {
                        portal1 = CreatePortal(1, 5);
                    }
                    else if (gameState.GetPosition(1, 3) == null)
                    {
                        portal1 = CreatePortal(1, 3);
                    }
                }
                else
                {
                    if (gameState.GetPosition(k, 3) == null)
                    {
                        portal1 = CreatePortal(k, 3);
                    }
                    else if (gameState.GetPosition(k, 2) == null)
                    {
                        portal1 = CreatePortal(k, 2);
                    }
                    else if (gameState.GetPosition(1, 3) == null)
                    {
                        portal1 = CreatePortal(1, 3);
                    }
                    else if (gameState.GetPosition(1, 2) == null)
                    {
                        portal1 = CreatePortal(1, 2);
                    }
                    else if (gameState.GetPosition(1, 4) == null)
                    {
                        portal1 = CreatePortal(1, 4);
                    }
                }
            }
            else if (k == 5)
            {
                if (mainRow == 3)
                {
                    if (gameState.GetPosition(k, 4) == null)
                    {
                        CreateMountain(k, 4);
                    }
                    else if (gameState.GetPosition(k, 3) == null)
                    {
                        CreateMountain(k, 3);
                    }
                }
                else
                {
                    if (gameState.GetPosition(k, 3) == null)
                    {
                        CreateMountain(k, 3);
                    }
                    else if (gameState.GetPosition(k, 4) == null)
                    {
                        CreateMountain(k, 4);
                    }
                }
                if (mainRow == 3)
                {
                    if (gameState.GetPosition(k, 3) == null)
                    {
                        Portal portal2 = CreatePairedPortal(k, 3,portal1);
                        portal1.SetPair(portal2);
                    }
                    else if (gameState.GetPosition(k, 2) == null)
                    {
                        Portal portal2 = CreatePairedPortal(k, 2,portal1);
                        portal1.SetPair(portal2);
                    }
                    else if (gameState.GetPosition(6, 3) == null)
                    {
                        Portal portal2 = CreatePairedPortal(6, 3,portal1);
                        portal1.SetPair(portal2);
                    }
                    else if (gameState.GetPosition(6, 2) == null)
                    {
                        Portal portal2 = CreatePairedPortal(6, 2,portal1);
                        portal1.SetPair(portal2);
                    }
                    else if (gameState.GetPosition(6, 4) == null)
                    {
                        Portal portal2 = CreatePairedPortal(6, 4,portal1);
                        portal1.SetPair(portal2);
                    }
                }
                else
                {
                    if (gameState.GetPosition(k, 4) == null)
                    {
                        CreatePairedPortal(k, 4,portal1);
                    }
                    else if (gameState.GetPosition(k, 5) == null)
                    {
                        CreatePairedPortal(k, 5,portal1);
                    }
                    else if (gameState.GetPosition(6, 4) == null)
                    {
                        CreatePairedPortal(6, 4,portal1);
                    }
                    else if (gameState.GetPosition(6, 5) == null)
                    {
                        CreatePairedPortal(6, 5,portal1);
                    }
                    else if (gameState.GetPosition(6, 3) == null)
                    {
                        CreatePairedPortal(6, 3,portal1);
                    }
                }
            }
            else
            {
                int randomNumber = rnd.Next(1, 4);
                int curr = 0;
                while (curr <= randomNumber)
                {
                    int randomY = rnd.Next(2, 6);
                    if (gameState.GetPosition(k, randomY) == null)
                    {
                        CreateMountain(k,randomY);
                    }
                    curr++;
                }
            }
        }
    }
}
