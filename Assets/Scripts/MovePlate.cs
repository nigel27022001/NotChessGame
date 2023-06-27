using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    private GameObject reference = null;

    private int matrixX;
    private int matrixY;
    
    //false is movement, true is attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            //change to red tile
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (attack) // remove attacked piece
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            String attackingName = reference.GetComponent<Chessman>().name;
            if (attackingName == "bSP2" || attackingName == "wSP2")
            {
                reference.GetComponent<Chessman>().Convert(cp.name);
            }

            if (cp.name == "wSN1" || cp.name == "bSN1")
            {
                if (cp.name == "wSN1")
                {
                    cp.GetComponent<Chessman>().name = "wN";
                    cp.GetComponent<Chessman>().Activate();
                }
                if (cp.name == "bSN1")
                {
                    cp.GetComponent<Chessman>().name = "bN";
                    cp.GetComponent<Chessman>().Activate();
                }
                controller.GetComponent<Game>().NextTurn();
                reference.GetComponent<Chessman>().DestroyMovePlates();
                return;
            }
            if (cp.name == "bK")
            {
                controller.GetComponent<Game>().Winner("White");
            } else if (cp.name == "wK")
            {
                controller.GetComponent<Game>().Winner("Black");
            }
            Destroy(cp);
        }
        // empty the old position
        int originalX = reference.GetComponent<Chessman>().GetXBoard();
        int originalY = reference.GetComponent<Chessman>().GetYBoard();
        controller.GetComponent<Game>().SetPositionEmpty(originalX, originalY);
        
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        print(matrixX + "" + matrixY);
        reference.GetComponent<Chessman>().MovePiece();
        
        controller.GetComponent<Game>().SetPosition(reference);
        controller.GetComponent<Game>().NextTurn();
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixY = y;
        matrixX = x;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
