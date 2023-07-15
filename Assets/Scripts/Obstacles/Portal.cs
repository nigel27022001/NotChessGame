using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Obstacles
{
    private Portal pair;
    private GameObject controller;
    public Sprite portalSprite;
    public Sprite portal2Sprite;
    
    public void Activate(int x, int y, int portalColour)
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (portalColour == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = portalSprite;
        }
        if (portalColour == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = portal2Sprite;
        }
        // takes the instantiated the location and adjust the transform
        this.xBoard = x;
        this.yBoard = y;
        SetCoords();
    }
    public void Activate(int x, int y, int portalColour, Portal portalPair)
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (portalColour == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = portalSprite;
        }
        if (portalColour == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = portal2Sprite;
        }
        // takes the instantiated the location and adjust the transform
        this.xBoard = x;
        this.yBoard = y;
        SetCoords();
        this.pair = portalPair;
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;
        
        x *= 1.11f;
        y *= 1.11f;
        
        x += -3.9f;
        y += -3.9f;
        
        this.transform.position = new Vector3(x, y, -1.0f);
    }
    
    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    public void SetPair(Portal pairPortal)
    {
        this.pair = pairPortal;
    }

    public Portal GetPairPortal()
    {
        return this.pair;
    }
}
