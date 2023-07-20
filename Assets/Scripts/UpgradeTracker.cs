using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeTracker : MonoBehaviour
{
   private Upgradeable[] blackUpgrades;

   private Upgradeable[] whiteUpgrades;

   public bool checkUpgrades(Upgradeable check, string player)
   {
      if (player == "white")
      {
         foreach (Upgradeable u in whiteUpgrades)
         {
            if (check == u)
            {
               return true;
            }
         }
      }
      
      if (player == "black")
      {
         foreach (Upgradeable u in blackUpgrades)
         {
            if (check == u)
            {
               return true;
            }
         }
      }

      return false;
   }

   public void updateUpgrades(Upgradeable u, string player)
   {
      if (player == "white")
      {
         whiteUpgrades.Append(u);
      }

      if (player == "black")
      {
         blackUpgrades.Append(u);
      }
   }
}
