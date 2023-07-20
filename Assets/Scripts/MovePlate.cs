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
    public Game gameState;
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

    public void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        
    }

    public void OnMouseUp()
    {
        GameObject cp = gameState.GetPosition(matrixX, matrixY);
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
                    print(attackedPiece.name);
                    if (attackedPiece.name == "king")
                    {
                        if (attackedPiece.player == "black")
                        {
                            gameState.Winner("White");
                        }

                        if (attackedPiece.player == "white")
                        {
                            gameState.Winner("Black");
                        }
                    }
                    else
                    {
                        attackedPiece.Defence();
                        attackingPiece.Attack(cp, matrixX, matrixY);
                    }
                }
            }
            else if(cp.name == "RIVER"){
                int originalX = reference.GetComponent<ChessPiece>().GetXBoard();
                int originalY = reference.GetComponent<ChessPiece>().GetYBoard();
                gameState.SetPositionEmpty(originalX, originalY);

                reference.GetComponent<ChessPiece>().SetXBoard(matrixX);
                reference.GetComponent<ChessPiece>().SetYBoard(matrixY);
                //print(matrixX + "" + matrixY);
                reference.GetComponent<ChessPiece>().MovePiece();

                gameState.SetPosition(reference);
                gameState.NextTurn();
                reference.GetComponent<ChessPiece>().DestroyMovePlates();
            }
        }

        else
        {
            if (gameState.portalPositions[matrixX, matrixY] != null)
            {
                Portal portal2 = gameState.portalPositions[matrixX, matrixY]
                    .GetPairPortal();
                matrixX = portal2.GetXBoard();
                matrixY = portal2.GetYBoard();
                if (gameState.positions[matrixX, matrixY] != null)
                {
                    ChessPiece conflict = gameState.positions[matrixX, matrixY]
                        .GetComponent<ChessPiece>();

                    void Flush(int x, int y)
                    {
                        gameState.SetPositionEmpty(matrixX, matrixY);
                        conflict.SetXBoard(matrixX + 1);
                        conflict.SetYBoard(matrixY);
                        conflict.MovePiece();
                        gameState.SetPosition(conflict.gameObject);
                    }
                    if (gameState.positions[matrixX + 1, matrixY] == null)
                    {
                        Flush(matrixX + 1, matrixY);
                    } else if (gameState.positions[matrixX - 1, matrixY] == null)
                    {
                        Flush(matrixX - 1, matrixY);
                    } else if (gameState.positions[matrixX, matrixY + 1] == null)
                    {
                        Flush(matrixX, matrixY + 1);
                    } else if (gameState.positions[matrixX, matrixY - 1] == null)
                    {
                        Flush(matrixX, matrixY - 1);
                    } else if (gameState.positions[matrixX + 1, matrixY + 1] == null)
                    {
                        Flush(matrixX + 1, matrixY + 1);
                    } else if (gameState.positions[matrixX - 1, matrixY + 1] == null)
                    {
                        Flush(matrixX - 1, matrixY + 1);
                    } else if (gameState.positions[matrixX + 1, matrixY - 1] == null)
                    {
                        Flush(matrixX + 1, matrixY - 1);
                    } else if (gameState.positions[matrixX - 1, matrixY - 1] == null)
                    {
                        Flush(matrixX - 1, matrixY - 1);
                    }
                    else
                    {
                        Destroy(conflict.gameObject);
                    }
                }
            }

            // empty the old position
                int originalX = reference.GetComponent<ChessPiece>().GetXBoard();
                int originalY = reference.GetComponent<ChessPiece>().GetYBoard();
                gameState.SetPositionEmpty(originalX, originalY);

                reference.GetComponent<ChessPiece>().SetXBoard(matrixX);
                reference.GetComponent<ChessPiece>().SetYBoard(matrixY);
                //print(matrixX + "" + matrixY);
                reference.GetComponent<ChessPiece>().MovePiece();

                gameState.SetPosition(reference);
                gameState.NextTurn();
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
