using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject controller;
    private Game gameState;
    public GameObject pawnPrefab,pawnS1Prefab, pawnS2Prefab;
    public GameObject bishopPrefab, bishopS1Prefab, bishopS2Prefab;
    public GameObject knightPrefab, knightS1Prefab;
    public GameObject queenPrefab, queenS1Prefab;
    public GameObject rookPrefab, rookS1Prefab, rookS2Prefab;
    public GameObject kingPrefab;
    
    public void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        gameState = controller.GetComponent<Game>();

    }

    public GameObject Create(string name, string player, int xCoord, int yCoord)
    {
        GameObject obj = null;
        ChessPiece cp = null;
        switch (name)
        {
            case "pawn" :
                obj = Instantiate(pawnPrefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<Pawn>();
                break;
            case "bishop" :
                obj = Instantiate(bishopPrefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<Bishop>();
                break;
            case "knight" :
                obj = Instantiate(knightPrefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<Knight>();
                break;
            case "rook" :
                obj = Instantiate(rookPrefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<Rook>();
                break;
            case "queen" :
                obj = Instantiate(queenPrefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<Queen>();
                break;
            case "king" :
                obj = Instantiate(kingPrefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<King>();
                break;
            case "pawnS1" :
                obj = Instantiate(pawnS1Prefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<SPawn1>();
                break;
            case "pawnS2" :
                obj = Instantiate(pawnS2Prefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<SPawn2>();
                break;
            case "rookS1" :
                obj = Instantiate(rookS1Prefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<SRook1>();
                break;
            case "rookS2" :
                obj = Instantiate(rookS2Prefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<SRook2>();
                break;
            case "bishopS1" :
                obj = Instantiate(bishopS1Prefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<SBishop1>();
                break;
            case "bishopS2" :
                obj = Instantiate(bishopS2Prefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<SBishop2>();
                break;
            case "knightS1" :
                obj = Instantiate(knightS1Prefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<SKnight1>();
                break;
            case "queenS1" :
                obj = Instantiate(queenS1Prefab, new Vector3(0, 0, 10), quaternion.identity);
                cp = obj.GetComponent<SQueen1>();
                break;

        }
        cp.transform.parent = gameState.Gameboard;
        cp.Activate(player, xCoord, yCoord);
        return obj;
    }

    public GameObject Replace(GameObject obj, string name)
    {
        ChessPiece cp = obj.GetComponent<ChessPiece>();
        int xBoard = cp.GetXBoard();
        int yBoard = cp.GetYBoard();
        string player = cp.player;
        Destroy(obj);
        return this.Create(name, player, xBoard, yBoard);
        
    }
}
