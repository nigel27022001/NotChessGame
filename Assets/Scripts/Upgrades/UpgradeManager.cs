using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using TMPro;
using UnityEngine.UI;


public class UpgradeManager : MonoBehaviour
{
    private Game gameState;

    private UnitManager UM;

    public GameObject Panel;
    
    public Random rnd = new Random();

    public GameObject PU1, PU2, RU1, RU2, BU1, BU2, KU1, QU1;
    
    public int numOfUpgrade = 8;
    public void Awake()
    {
        UM = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitManager>();
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
    }

    public void UpgradePanel()
    {
        print("here");
        GameObject obj = Instantiate(Panel, new Vector3(100, 100, 10), Quaternion.identity);
        PanelManager panelManager = obj.GetComponent<PanelManager>();
        //panelManager.audio1.Play();
        panelManager.openPanel();

        void AllocateUpgrade(int optionNumber)
        {
            int curr = 0;
            if (gameState.currentPlayer != "white")
            {

                bool check = false;
                for (int i = 0; i < gameState.WhiteAugments.Length; i++)
                {
                    check = check || !gameState.WhiteAugments[i];
                }

                if (check == false)
                {
                    print("No More Upgrades");
                    return;
                }

                curr = rnd.Next(0, numOfUpgrade);
                while (gameState.WhiteAugments[curr] == true)
                {
                    curr = rnd.Next(0, numOfUpgrade);
                }

                gameState.WhiteAugments[curr] = true;
            }

            if (gameState.currentPlayer != "black")
            {

                bool check = false;
                for (int i = 0; i < gameState.BlackAugments.Length; i++)
                {
                    check = check || !gameState.BlackAugments[i];
                }

                if (check == false)
                {
                    print("No More Upgrades");
                    return;
                }

                curr = rnd.Next(0, numOfUpgrade);
                while (gameState.BlackAugments[curr] == true)
                {
                    curr = rnd.Next(0, numOfUpgrade);
                }

                gameState.BlackAugments[curr] = true;
            }


            switch (optionNumber)
            {
                case 1:
                    panelManager.GetComponent<PanelManager>().Option1Text.GetComponent<TextMeshProUGUI>().text =
                        UpgradeTexter(curr);
                    panelManager.GetComponent<PanelManager>().Option1Button.GetComponent<Button>().onClick
                        .AddListener(delegate
                        {
                            SelectUpgrades(curr);
                            GameObject.FindGameObjectWithTag("UpgradeSound").GetComponent<AudioSource>().Play();
                            obj.SetActive(false);
                        });
                    break;
                case 2:
                    panelManager.GetComponent<PanelManager>().Option2Text.GetComponent<TextMeshProUGUI>().text =
                        UpgradeTexter(curr);
                    panelManager.GetComponent<PanelManager>().Option2Button.GetComponent<Button>().onClick
                        .AddListener(delegate
                        {
                            SelectUpgrades(curr);
                            GameObject.FindGameObjectWithTag("UpgradeSound").GetComponent<AudioSource>().Play();
                            obj.SetActive(false);
                        });
                    break;
                case 3:
                    panelManager.GetComponent<PanelManager>().Option3Text.GetComponent<TextMeshProUGUI>().text =
                        UpgradeTexter(curr);
                    panelManager.GetComponent<PanelManager>().Option3Button.GetComponent<Button>().onClick
                        .AddListener(delegate
                        {
                            SelectUpgrades(curr);
                            GameObject.FindGameObjectWithTag("UpgradeSound").GetComponent<AudioSource>().Play();
                            obj.SetActive(false);
                        });
                    break;
            }
        }

        void SelectUpgrades(int number)
        {
            Upgradeable upgrade;
            if (gameState.currentPlayer != "white")
            {
                switch (number)
                {
                    case 0:
                        gameState.WhiteAugments[0] = true;
                        upgrade = Instantiate(PU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 1:
                        gameState.WhiteAugments[1] = true;
                        upgrade = Instantiate(PU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 2:
                        gameState.WhiteAugments[2] = true;
                        upgrade = Instantiate(RU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 3:
                        gameState.WhiteAugments[3] = true;
                         upgrade = Instantiate(RU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 4:
                        gameState.WhiteAugments[4] = true;
                        upgrade = Instantiate(BU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 5:
                        gameState.WhiteAugments[5] = true;
                        upgrade = Instantiate(KU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 6:
                        gameState.WhiteAugments[6] = true;
                        upgrade = Instantiate(QU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 7:
                        gameState.WhiteAugments[7] = true;
                        upgrade = Instantiate(BU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                }
            }
            else
            {
                switch (number)
                {
                    case 0:
                        gameState.BlackAugments[0] = true;
                        upgrade = Instantiate(PU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 1:
                        gameState.BlackAugments[1] = true;
                        upgrade = Instantiate(PU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 2:
                        gameState.BlackAugments[2] = true;
                        upgrade = Instantiate(RU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 3:
                        gameState.BlackAugments[3] = true;
                        upgrade = Instantiate(RU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 4:
                        gameState.BlackAugments[4] = true;
                        upgrade = Instantiate(BU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 5:
                        gameState.BlackAugments[5] = true;
                        upgrade = Instantiate(KU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 6:
                        gameState.BlackAugments[6] = true;
                        upgrade = Instantiate(QU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 7:
                        gameState.BlackAugments[7] = true;
                        upgrade = Instantiate(BU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                }
            }
        }

        string UpgradeTexter(int Number)
        {
            switch (Number)
            {
                case 0:
                    return "Your pawns can now move twice.";
                    break;
                case 1:
                    return "Your pawns can now convert a captured piece.";
                    break;
                case 2:
                    return "Your rooks can now jump over a piece to capture it";
                    break;
                case 3:
                    return "Your rooks can now remove terrain obstacles by capturing them";
                    break;
                case 4:
                    return "Your bishops can also take one step forward and backwards";
                    break;
                case 5:
                    return "Your knights can resist capture once";
                    break;
                case 6:
                    return "Your queen can also move in a L Shape";
                    break;
                case 7:
                    return "Your bishops will spawn pawns everytime you capture an opposing unit";
            }

            return "error";
        }
        AllocateUpgrade(1);
        AllocateUpgrade(2);
        AllocateUpgrade(3);
    }
}
