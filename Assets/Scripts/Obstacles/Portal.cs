using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Portal pair;
    private GameObject controller;
    private int xBoard = -1;
    private int yBoard = -1;
    public Sprite portalSprite;
    
    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        this.GetComponent<SpriteRenderer>().sprite = portalSprite;
        // takes the instantiated the location and adjust the transform
        SetCoords();
    }
    public void Activate(Portal portalPair)
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        this.GetComponent<SpriteRenderer>().sprite = portalSprite;
        // takes the instantiated the location and adjust the transform
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
