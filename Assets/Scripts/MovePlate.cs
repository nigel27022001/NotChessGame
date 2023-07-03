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
        GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
        if (cp != null)
        {
            if (attack) // remove attacked piece
            {
                String attackingName = reference.GetComponent<Chessman>().name;
                if (attackingName == "bSB2" || attackingName == "wSB2")
                {
                    reference.GetComponent<Chessman>().PawnNecromancy(1);
                    print("tick");
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
                }
                else if (cp.name == "wK")
                {
                    controller.GetComponent<Game>().Winner("Black");
                }

                Destroy(cp);
            }

            if (cp.name == "PORTAL")
                //need to update moveplate to allow cp.name == portal
            {
                int otherX = cp.GetComponent<Portal>().GetPairPortal().GetXBoard();
                int otherY = cp.GetComponent<Portal>().GetPairPortal().GetYBoard();
                matrixX = otherX;
                matrixY = otherY;
            }
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
