using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public static Hashtable levelDetails = new Hashtable();
    public Button[] levels;
    public GameObject levelSelect;

    int levelNumber = 1;

    private void Start()
    {
        while (levelNumber < 4)
        {
            if (!levelDetails.ContainsKey(levelNumber))
            {
                levelDetails.Add(levelNumber, false);
            }
            levelNumber++;
        }
    }

    private void Update()
    {

        if (levelSelect.activeSelf)
        {
            for (int i = 1; i < levels.Length; i++)
            {
                if (!(bool)levelDetails[i])
                {
                    levels[i].gameObject.SetActive(false);
                }
                else
                {
                    levels[i].gameObject.SetActive(true);
                }
            }
        }
    }

    public void StartL1()
    {
        SceneManager.LoadScene(1);
    }

    public void StartL2()
    {
        SceneManager.LoadScene(2);
    }

    public void StartL3()
    {
        SceneManager.LoadScene(3);
    }

    public void StartL4()
    {
        SceneManager.LoadScene(4);
    }

}
