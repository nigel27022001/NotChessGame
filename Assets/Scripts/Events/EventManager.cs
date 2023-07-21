using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class ObstacleEventManager : MonoBehaviour
{
    private Animator anim;
    private GameObject controller;
    public GameObject RiverEventPrefab;
    public GameObject PortalEventPrefab;
    public GameObject LavaEventPrefab;
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
        RiverEvent RE = Instantiate(RiverEventPrefab).GetComponent<RiverEvent>();
        RE.Activate();
    }
    
    public void PortalEvent()
    {
        anim.Play("Portal");
        PortalEvent PE = Instantiate(PortalEventPrefab).GetComponent<PortalEvent>();
        PE.Activate();
    }
    public void LavaEvent(int noOfLava)
    {
        anim.Play("Lava");
        LavaEvent LE = Instantiate(LavaEventPrefab).GetComponent<LavaEvent>();
        LE.Activate();
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
        int randomnum = rnd.Next(0, 6);
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
