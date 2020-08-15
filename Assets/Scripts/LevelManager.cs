using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static bool isGameOver = false;
    public int bulletCount = 20;
    public string nextLevel;
    public Text gameText;
    public Text bulletText;
    public int levelNumber;

    public static Text bulletTextCount;
    public static int bulletLeft;

    void Start()
    {
        isGameOver = false;
        bulletTextCount = bulletText;
        bulletLeft = bulletCount;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            if (!PauseMenuBehavior.isGamePaused)
            {
                PlayerTimeScript.playertime += Time.deltaTime;
            }
            SetSanitizerBulletText();
        }
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER!";
        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "YOU WIN!";

        if (LevelSelect.levelDetails.ContainsKey(levelNumber))
        {
            if (!(bool)LevelSelect.levelDetails[levelNumber])
            {
                LevelSelect.levelDetails[levelNumber] = true;
            }
        }

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadLevel", 2);
        }
    }

    public void SetSanitizerBulletText()
    {
        if (bulletText)
        {
            bulletText.text = "GAME OVER!";
            bulletText.text = bulletLeft.ToString();
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
