using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimeScript : MonoBehaviour
{
    public static float playertime = 0.0f;
    public Text timePlayed;

    int minutes = 0;
    int seconds = 0;

    private void Update()
    {
        if (timePlayed != null)
        {
            seconds = (int)(playertime % 60);
            minutes = (int)((playertime / 60) % 60);
            timePlayed.text = "Time played: " + minutes + " mins " + seconds + " secs";
        }
    }
}
