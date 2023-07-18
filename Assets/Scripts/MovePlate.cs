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
    public Sprite AttackMovePlate;
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
            gameObject.GetComponent<SpriteRenderer>().sprite = AttackMovePlate;
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
                GameObject.FindGameObjectWithTag("KillSound").GetComponent<AudioSource>().Play();
                ChessPiece attackingPiece = reference.GetComponent<ChessPiece>();
                if (cp.name == "OBSTACLE")
                {
                    attackingPiece.Attack(cp, matrixX, matrixY);
                }
                else
                {
                    ChessPiece attackedPiece = cp.GetComponent<ChessPiece>();
                    /*String attackingName = reference.GetComponent<Chessman>().name;
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
                    */
                    print(attackedPiece.name);
                    if (attackedPiece.name == "king")
                    {
                        if (attackedPiece.player == "black")
                        {
                            controller.GetComponent<Game>().Winner("White");
                        }

                        if (attackedPiece.player == "white")
                        {
                            controller.GetComponent<Game>().Winner("Black");
                        }
                    }

                    /*if (attackingPiece.name == "pawnS2")
                    {
                        SPawn2 obj = attackingPiece.gameObject.GetComponent<SPawn2>();
                        reference = obj.Convert(cp);
                    }
                    */
                    else
                    {
                        attackedPiece.Defence();
                        attackingPiece.Attack(cp, matrixX, matrixY);
                    }
                }
            }

            if (cp.name == "PORTAL")
                //need to update moveplate to allow cp.name == portal
            {
                matrixX = cp.GetComponent<Portal>().GetPairPortal().GetXBoard();
                matrixY = cp.GetComponent<Portal>().GetPairPortal().GetYBoard();
                int originalX = reference.GetComponent<ChessPiece>().GetXBoard();
                int originalY = reference.GetComponent<ChessPiece>().GetYBoard();
                controller.GetComponent<Game>().SetPositionEmpty(originalX, originalY);

                reference.GetComponent<ChessPiece>().SetXBoard(matrixX);
                reference.GetComponent<ChessPiece>().SetYBoard(matrixY);
                //print(matrixX + "" + matrixY);
                reference.GetComponent<ChessPiece>().MovePiece();

                controller.GetComponent<Game>().SetPosition(reference);
                controller.GetComponent<Game>().NextTurn();
                reference.GetComponent<ChessPiece>().DestroyMovePlates();
            }
            else if(cp.name == "RIVER"){
                int originalX = reference.GetComponent<ChessPiece>().GetXBoard();
                int originalY = reference.GetComponent<ChessPiece>().GetYBoard();
                controller.GetComponent<Game>().SetPositionEmpty(originalX, originalY);

                reference.GetComponent<ChessPiece>().SetXBoard(matrixX);
                reference.GetComponent<ChessPiece>().SetYBoard(matrixY);
                //print(matrixX + "" + matrixY);
                reference.GetComponent<ChessPiece>().MovePiece();

                controller.GetComponent<Game>().SetPosition(reference);
                controller.GetComponent<Game>().NextTurn();
                reference.GetComponent<ChessPiece>().DestroyMovePlates();
            }
        }

        else
        {
            // empty the old position
                int originalX = reference.GetComponent<ChessPiece>().GetXBoard();
                int originalY = reference.GetComponent<ChessPiece>().GetYBoard();
                controller.GetComponent<Game>().SetPositionEmpty(originalX, originalY);

                reference.GetComponent<ChessPiece>().SetXBoard(matrixX);
                reference.GetComponent<ChessPiece>().SetYBoard(matrixY);
                //print(matrixX + "" + matrixY);
                reference.GetComponent<ChessPiece>().MovePiece();

                controller.GetComponent<Game>().SetPosition(reference);
                controller.GetComponent<Game>().NextTurn();
                reference.GetComponent<ChessPiece>().DestroyMovePlates();
        }
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
