using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Option1Button;
    public GameObject Option2Button;
    public GameObject Option3Button;

    public GameObject Option1Text;
    public GameObject Option2Text;
    public GameObject Option3Text;

    public GameObject Option1Image;
    public GameObject Option2Image;
    public GameObject Option3Image;
    
    public void openPanel()
    {
        Panel.SetActive(true);
    }
}
