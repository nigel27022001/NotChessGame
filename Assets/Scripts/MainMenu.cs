using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().StopMusic();
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
