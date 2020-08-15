using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Update is called once per frame
    public void ExitGame()
    {
        PlayerTimeScript.playertime = 0.0f;
        Application.Quit();
    }
}
