using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private GameObject controller;
    private int xBoard = -1;
    private int yBoard = -1;
    public Sprite LavaSprite;
    
    public void Activate(int x, int y)
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        this.GetComponent<SpriteRenderer>().sprite = LavaSprite;
        // takes the instantiated the location and adjust the transform
        this.xBoard = x;
        this.yBoard = y;
        SetCoords();
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;
        
        x *= 1.107f;
        y *= 1.107f;
        
        x += -3.87f;
        y += -3.87f;
        if (x <= 0)
        {
            x += 0.0069f;
            
        }
        if (x > 0)
        {
            x -= 0.0069f;
            
        }
        if (y <= 0)
        {
            y += 0.006f;
            
        }
        if (y > 0)
        {
            y -= 0.0060f;
            
        }
            
        
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
