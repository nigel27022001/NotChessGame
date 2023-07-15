using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountains : Obstacles
{
    private GameObject controller;
    public Sprite MountainSprite;
    
    public void Activate(int x, int y)
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        this.GetComponent<SpriteRenderer>().sprite = MountainSprite;
        // takes the instantiated the location and adjust the transform
        this.xBoard = x;
        this.yBoard = y;
        SetCoords();
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
}
