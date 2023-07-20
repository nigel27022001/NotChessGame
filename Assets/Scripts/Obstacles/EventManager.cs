using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class ObstacleEventManager : MonoBehaviour
{
    private Animator anim;
    private GameObject controller;
    public GameObject RiverPrefab;
    public GameObject PortalPrefab;
    public GameObject MountainPrefab;
    public GameObject LavaPrefab;
    public GameObject AntiMagicPrefab;
    public GameObject WindPrefab;
    public Random rnd = new Random();
    private Game Gamestate;
   
    

    public void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        anim = GameObject.FindGameObjectWithTag("EventAnimator").GetComponent<Animator>();
        Gamestate = controller.GetComponent<Game>();

    }

    public void RiverEvent(int rownum)
    {
        anim.Play("River");
        Gamestate.riverActive = true;
        for (int k = 0; k <= 7; k++)
        {
            GameObject obj = Instantiate(RiverPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Rivers river = obj.GetComponent<Rivers>();
            river.name = "RIVER";
            obj.transform.parent = Gamestate.Gameboard;
            if (Gamestate.GetPosition(k, rownum) != null)
            {
                if (rownum == 4)
                {
                    if (Gamestate.GetPosition(k, rownum - 1) == null)
                    {
                        river.Activate(k, rownum - 1);
                        Gamestate.positions[k, rownum - 1] = obj;
                        Gamestate.riverSpot[k] = rownum - 1;
                    }
                    else if (Gamestate.GetPosition(k, rownum + 1) == null)
                    {
                        river.Activate(k, rownum + 1);
                        Gamestate.positions[k, rownum + 1] = obj;
                        Gamestate.riverSpot[k] = rownum + 1;
                    }
                }

                if (rownum == 3)
                {
                    if (Gamestate.GetPosition(k, rownum + 1) == null)
                    {
                        river.Activate(k, rownum + 1);
                        Gamestate.positions[k, rownum + 1] = obj;
                        Gamestate.riverSpot[k] = rownum + 1;
                    }
                    else if (Gamestate.GetPosition(k, rownum - 1) == null)
                    {
                        river.Activate(k, rownum - 1);
                        Gamestate.positions[k, rownum - 1] = obj;
                        Gamestate.riverSpot[k] = rownum - 1;
                    }
                }
            }
            else
            {
                river.Activate(k,rownum);
                Gamestate.positions[k, rownum] = obj;
                Gamestate.riverSpot[k] = rownum;
            }
            
        }
    }
    
    public void PortalEvent()
    {
        anim.Play("Portal");
        Portal CreatePortal(int x, int y)
        {
            GameObject obj = Instantiate(PortalPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Portal cm = obj.GetComponent<Portal>();
            obj.transform.parent = Gamestate.Gameboard;
            cm.name = "PORTAL";
            cm.Activate(x,y,1);
            Gamestate.portalPositions[x, y] = cm;
            return cm;
        }
        Portal CreatePairedPortal(int x, int y, Portal pairing)
        {
            GameObject obj = Instantiate(PortalPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Portal cm = obj.GetComponent<Portal>();
            obj.transform.parent = Gamestate.Gameboard;
            cm.name = "PORTAL";
            cm.Activate(x,y,2,pairing);
            Gamestate.portalPositions[x, y] = cm;
            return cm;
        }
    
        void CreateMountain(int x, int y)
        {
            GameObject obj = Instantiate(MountainPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Mountains cm = obj.GetComponent<Mountains>();
            obj.transform.parent = Gamestate.Gameboard;
            cm.name = "OBSTACLE";
            cm.Activate(x,y);
            Gamestate.positions[x, y] = obj;
        }

        int mainRow = rnd.Next(3, 5);
        int start = 2;
        Portal portal1 = null;
        for (int k = 2; k <= 5; k++)
        {
            if (k == 2)
            {
                if (Gamestate.GetPosition(k, mainRow) == null)
                {
                    CreateMountain(k, mainRow);
                    print(1);
                }
                else
                {
                    if (mainRow == 3)
                    {
                        if (Gamestate.GetPosition(k, 4) == null)
                        {
                            CreateMountain(k, 4);
                            print(1);
                        }
                    }
                    else
                    {
                        if (Gamestate.GetPosition(k, 3) == null)
                        {
                            CreateMountain(k, 3);
                            print(1);
                        }
                    }
                }
                if (mainRow == 3)
                {
                    if (Gamestate.GetPosition(k, 4) == null)
                    {
                        portal1 = CreatePortal(k, 4);
                    }
                    else if (Gamestate.GetPosition(k, 5) == null)
                    {
                        portal1 = CreatePortal(k, 5);
                    }
                    else if (Gamestate.GetPosition(1, 4) == null)
                    {
                        portal1 = CreatePortal(1, 4);
                    }
                    else if (Gamestate.GetPosition(1, 5) == null)
                    {
                        portal1 = CreatePortal(1, 5);
                    }
                    else if (Gamestate.GetPosition(1, 3) == null)
                    {
                        portal1 = CreatePortal(1, 3);
                    }
                }
                else
                {
                    if (Gamestate.GetPosition(k, 3) == null)
                    {
                        portal1 = CreatePortal(k, 3);
                    }
                    else if (Gamestate.GetPosition(k, 2) == null)
                    {
                        portal1 = CreatePortal(k, 2);
                    }
                    else if (Gamestate.GetPosition(1, 3) == null)
                    {
                        portal1 = CreatePortal(1, 3);
                    }
                    else if (Gamestate.GetPosition(1, 2) == null)
                    {
                        portal1 = CreatePortal(1, 2);
                    }
                    else if (Gamestate.GetPosition(1, 4) == null)
                    {
                        portal1 = CreatePortal(1, 4);
                    }
                }
            }
            else if (k == 5)
            {
                if (mainRow == 3)
                {
                    if (Gamestate.GetPosition(k, 4) == null)
                    {
                        CreateMountain(k, 4);
                    }
                    else if (Gamestate.GetPosition(k, 3) == null)
                    {
                        CreateMountain(k, 3);
                    }
                }
                else
                {
                    if (Gamestate.GetPosition(k, 3) == null)
                    {
                        CreateMountain(k, 3);
                    }
                    else if (Gamestate.GetPosition(k, 4) == null)
                    {
                        CreateMountain(k, 4);
                    }
                }
                if (mainRow == 3)
                {
                    if (Gamestate.GetPosition(k, 3) == null)
                    {
                        Portal portal2 = CreatePairedPortal(k, 3,portal1);
                        portal1.SetPair(portal2);
                    }
                    else if (Gamestate.GetPosition(k, 2) == null)
                    {
                        Portal portal2 = CreatePairedPortal(k, 2,portal1);
                        portal1.SetPair(portal2);
                    }
                    else if (Gamestate.GetPosition(6, 3) == null)
                    {
                        Portal portal2 = CreatePairedPortal(6, 3,portal1);
                        portal1.SetPair(portal2);
                    }
                    else if (Gamestate.GetPosition(6, 2) == null)
                    {
                        Portal portal2 = CreatePairedPortal(6, 2,portal1);
                        portal1.SetPair(portal2);
                    }
                    else if (Gamestate.GetPosition(6, 4) == null)
                    {
                        Portal portal2 = CreatePairedPortal(6, 4,portal1);
                        portal1.SetPair(portal2);
                    }
                }
                else
                {
                    if (Gamestate.GetPosition(k, 4) == null)
                    {
                        CreatePairedPortal(k, 4,portal1);
                    }
                    else if (Gamestate.GetPosition(k, 5) == null)
                    {
                        CreatePairedPortal(k, 5,portal1);
                    }
                    else if (Gamestate.GetPosition(6, 4) == null)
                    {
                        CreatePairedPortal(6, 4,portal1);
                    }
                    else if (Gamestate.GetPosition(6, 5) == null)
                    {
                        CreatePairedPortal(6, 5,portal1);
                    }
                    else if (Gamestate.GetPosition(6, 3) == null)
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
                    if (Gamestate.GetPosition(k, randomY) == null)
                    {
                        CreateMountain(k,randomY);
                    }
                    curr++;
                }
            }
        }
    }
    public void LavaEvent(int noOfLava)
    {
        anim.Play("Lava");
        void CreateLava(int x, int y)
        {
            GameObject obj = Instantiate(LavaPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Lava cm = obj.GetComponent<Lava>();
            obj.transform.parent = Gamestate.Gameboard;
            cm.name = "OBSTACLE";
            cm.Activate(x,y);
            Gamestate.positions[x, y] = obj;
        }
        int i = 0;
        
        while (i < noOfLava)
        {
            int randomRow = rnd.Next(2,6);
            int randomCol = rnd.Next(0,8);
            if (Gamestate.GetPosition(randomCol, randomRow) == null && Gamestate.PositionOnBoard(randomCol, randomRow))
            {
                CreateLava(randomCol,randomRow);
                i++;
            }
        }
    }

    public void PawnEvent()
    {
        anim.Play("Rebellion");
        Gamestate.restriction = "pawn";
    }

    public void AntiMagicEvent()
    {
        anim.Play("Anti-Magic");
        GameObject obj = Instantiate(AntiMagicPrefab);
        AntiMagicEvent AME = obj.GetComponent<AntiMagicEvent>();
        AME.Activate();
    }

    public void WindEvent()
    {
        anim.Play("Windy");
        GameObject obj = Instantiate(WindPrefab);
        WindEvent WE = obj.GetComponent<WindEvent>();
        WE.Activate();
    }
    public void RandomEvent()
    {
        int randomnum = 3 /*rnd.Next(0, 6)*/;
        print(randomnum);
        switch (randomnum)
        {
            case 0 :
                int randomRiver = rnd.Next(3, 5);
                RiverEvent(randomRiver);
                break;
            case 1 :
                int randomLava = rnd.Next(7, 13);
                LavaEvent(randomLava);
                break;
            case 2 :
                PawnEvent();
                break;
            case 3 :
                PortalEvent();
                break;
            case 4 :
                AntiMagicEvent();
                break;
            case 5 :
                WindEvent();
                break;
        }
    }
    public void ClearEvent()
    {
        GameObject[] Obstacles = GameObject.FindGameObjectsWithTag("OBSTACLE");
        for (int k = 0; k < Obstacles.Length; k++)
        {
            Destroy(Obstacles[k]);
            Gamestate.SetPositionEmpty(Obstacles[k].GetComponent<Obstacles>().GetXBoard(), Obstacles[k].GetComponent<Obstacles>().GetYBoard());
        }
        GameObject[] Rivers = GameObject.FindGameObjectsWithTag("RIVER");
        for (int k = 0; k < Obstacles.Length; k++)
        {
            Destroy(Rivers[k]);
            Gamestate.SetPositionEmpty(Rivers[k].GetComponent<Obstacles>().GetXBoard(), Obstacles[k].GetComponent<Obstacles>().GetYBoard());
        }
        Gamestate.restriction = null;
        Gamestate.riverActive = false;
    }
}
