using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgradeable : MonoBehaviour
{
    protected Game gameState;

    protected UnitManager UM;
    public void Awake()
    {
        UM = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitManager>();
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
    }

    public abstract void Upgrade();
}
