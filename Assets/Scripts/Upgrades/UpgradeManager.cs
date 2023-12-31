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
    
    private bool[] WhiteAugments;
    private bool[] BlackAugments;

    [SerializeField] private GameObject Panel;
    
    Random rnd = new Random();

    [SerializeField] private GameObject PU1, PU2, RU1, RU2, BU1, BU2, KU1, QU1;
    
    private int numOfUpgrade = 8;
    public void Awake()
    {
        UM = GameObject.FindGameObjectWithTag("UnitManager").GetComponent<UnitManager>();
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        BlackAugments = new bool[numOfUpgrade];
        WhiteAugments = new bool[numOfUpgrade];
        for (int i = 0; i < BlackAugments.Length; i++)
        {
            BlackAugments[i] = false;
            WhiteAugments[i] = false;
        }
    }

    public void UpgradePanel()
    {
        GameObject obj = Instantiate(Panel, new Vector3(100, 100, 10), Quaternion.identity);
        PanelManager panelManager = obj.GetComponent<PanelManager>();
        //panelManager.audio1.Play();
        panelManager.openPanel();

        void AllocateUpgrade(int optionNumber)
        {
            int curr = 0;
            bool check = false;
            if (gameState.currentPlayer != "white")
            {
                for (int i = 0; i < WhiteAugments.Length; i++)
                {
                    check = check || !WhiteAugments[i];
                }

                curr = rnd.Next(0, numOfUpgrade);
                while (WhiteAugments[curr] == true)
                {
                    curr = rnd.Next(0, numOfUpgrade);
                }

                WhiteAugments[curr] = true;
            }

            if (gameState.currentPlayer != "black")
            {
                for (int i = 0; i < BlackAugments.Length; i++)
                {
                    check = check || !BlackAugments[i];
                }

                curr = rnd.Next(0, numOfUpgrade);
                while (BlackAugments[curr] == true)
                {
                    curr = rnd.Next(0, numOfUpgrade);
                }

                BlackAugments[curr] = true;
            }

            if (check == false)
            {
                switch (optionNumber)
                {
                    case 1:
                        panelManager.GetComponent<PanelManager>().Option1Text.GetComponent<TextMeshProUGUI>().text =
                            "No More Upgrades";
                        panelManager.GetComponent<PanelManager>().Option1Image.SetActive(false);
                        panelManager.GetComponent<PanelManager>().Option1Button.SetActive(false);
                    break;
                case 2:
                    panelManager.GetComponent<PanelManager>().Option2Text.GetComponent<TextMeshProUGUI>().text =
                        "No More Upgrades";
                    panelManager.GetComponent<PanelManager>().Option2Image.SetActive(false);
                    panelManager.GetComponent<PanelManager>().Option2Button.SetActive(false);
                    break;
                case 3:
                    panelManager.GetComponent<PanelManager>().Option3Text.GetComponent<TextMeshProUGUI>().text =
                        "No More Upgrades";
                    panelManager.GetComponent<PanelManager>().Option3Image.SetActive(false);
                    panelManager.GetComponent<PanelManager>().Option3Button.SetActive(false);
                    break;
                }
            }

            switch (optionNumber)
            {
                case 1:
                    panelManager.GetComponent<PanelManager>().Option1Text.GetComponent<TextMeshProUGUI>().text =
                        UpgradeTexter(curr);
                    panelManager.GetComponent<PanelManager>().Option1Image.GetComponent<Image>().sprite =
                        UpgradeImaging(curr);
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
                    panelManager.GetComponent<PanelManager>().Option2Image.GetComponent<Image>().sprite =
                       UpgradeImaging(curr);
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
                    panelManager.GetComponent<PanelManager>().Option3Image.GetComponent<Image>().sprite =
                        UpgradeImaging(curr);
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
                        WhiteAugments[0] = true;
                        upgrade = Instantiate(PU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 1:
                        WhiteAugments[1] = true;
                        upgrade = Instantiate(PU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 2:
                        WhiteAugments[2] = true;
                        upgrade = Instantiate(RU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 3:
                        WhiteAugments[3] = true;
                         upgrade = Instantiate(RU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 4:
                        WhiteAugments[4] = true;
                        upgrade = Instantiate(BU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 5:
                        WhiteAugments[5] = true;
                        upgrade = Instantiate(KU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 6:
                        WhiteAugments[6] = true;
                        upgrade = Instantiate(QU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 7:
                        WhiteAugments[7] = true;
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
                        BlackAugments[0] = true;
                        upgrade = Instantiate(PU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 1:
                        BlackAugments[1] = true;
                        upgrade = Instantiate(PU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 2:
                        BlackAugments[2] = true;
                        upgrade = Instantiate(RU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 3:
                        BlackAugments[3] = true;
                        upgrade = Instantiate(RU2).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 4:
                        BlackAugments[4] = true;
                        upgrade = Instantiate(BU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 5:
                        BlackAugments[5] = true;
                        upgrade = Instantiate(KU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 6:
                        BlackAugments[6] = true;
                        upgrade = Instantiate(QU1).GetComponent<Upgradeable>();
                        upgrade.Upgrade();
                        break;
                    case 7:
                        BlackAugments[7] = true;
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
                    return "Your pawns can transform into a captured piece";
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

        Sprite UpgradeImaging(int number)
        {
            switch (number)
            {
                case 0:
                    if (gameState.currentPlayer != "black")
                    {
                        return PU1.GetComponent<PawnUpgrade1>().bSP1;
                    }
                    else
                    {
                        return PU1.GetComponent<PawnUpgrade1>().wSP1;
                    }
                case 1:
                    if (gameState.currentPlayer != "black")
                    {
                        return PU2.GetComponent<PawnUpgrade2>().bSP2;
                    }
                    else
                    {
                        return PU2.GetComponent<PawnUpgrade2>().wSP2;
                    }
                case 2:
                    if (gameState.currentPlayer != "black")
                    {
                        return RU1.GetComponent<RookUpgrade1>().bSR1;
                    }
                    else
                    {
                        return RU1.GetComponent<RookUpgrade1>().wSR1;
                    }
                case 3:
                    if (gameState.currentPlayer != "black")
                    {
                        return RU2.GetComponent<RookUpgrade2>().bSR2;
                    }
                    else
                    {
                        return RU2.GetComponent<RookUpgrade2>().wSR2;
                    }
                case 4:
                    if (gameState.currentPlayer != "black")
                    {
                        return BU1.GetComponent<BishopUpgrade1>().bSB1;
                    }
                    else
                    {
                        return BU1.GetComponent<BishopUpgrade1>().wSB1;
                    }
                case 5:
                    if (gameState.currentPlayer != "black")
                    {
                        return KU1.GetComponent<KnightUpgrade1>().bSN1;
                    }
                    else
                    {
                        return KU1.GetComponent<KnightUpgrade1>().wSN1;
                    }
                case 6:
                    if (gameState.currentPlayer != "black")
                    {
                        return QU1.GetComponent<QueenUpgrade1>().bSQ1;
                    }
                    else
                    {
                        return QU1.GetComponent<QueenUpgrade1>().wSQ1;
                    }
                case 7:
                    if (gameState.currentPlayer != "black")
                    {
                        return BU2.GetComponent<BishopUpgrade2>().bSB2;
                    }
                    else
                    {
                        return BU2.GetComponent<BishopUpgrade2>().wSB2;
                    }
            }
            print("upgrade imaging error");
            return null;
        }
        AllocateUpgrade(1);
        AllocateUpgrade(2);
        AllocateUpgrade(3);
    }
}
