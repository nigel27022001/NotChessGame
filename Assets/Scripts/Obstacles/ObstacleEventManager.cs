using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class ObstacleEventManager : MonoBehaviour
{
    private Animator anim;
    private GameObject controller;
    public GameObject RiverPrefab;
    public GameObject PortalPrefab;
    public GameObject MountainPrefab;
    public GameObject LavaPrefab;
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
        for (int k = 0; k <= 7; k++)
        {
            GameObject obj = Instantiate(RiverPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Rivers river = obj.GetComponent<Rivers>();
            obj.transform.parent = Gamestate.Gameboard;
            river.Activate(k,rownum);
            Gamestate.positions[k, rownum] = obj;
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
            cm.Activate(x,y);
            Gamestate.positions[x, y] = obj;
            return cm;
        }
        Portal CreatePairedPortal(int x, int y, Portal pairing)
        {
            GameObject obj = Instantiate(PortalPrefab, new Vector3(0, 0, 10), quaternion.identity);
            Portal cm = obj.GetComponent<Portal>();
            obj.transform.parent = Gamestate.Gameboard;
            cm.name = "PORTAL";
            cm.Activate(x,y,pairing);
            Gamestate.positions[x, y] = obj;
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
    
        Portal portal1 = CreatePortal(3, 3);
        Portal portal2 = CreatePairedPortal(3, 5, portal1);
        portal1.SetPair(portal2);
        CreateMountain(3,4);
        CreateMountain(4,4);
        CreateMountain(2,4);
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
}
