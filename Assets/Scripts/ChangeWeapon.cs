using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject weaponPanel;

    int selectedWeapon = 0;

    GameObject currentWeapon;

    Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons = weaponPanel.GetComponentsInChildren<Button>();
        UpdateSpellUI();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSpell = selectedWeapon;
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // scrolling down
            if(selectedWeapon >= weapons.Length -1)
            {
                selectedWeapon = 0;
            } else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // scrolling up
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weapons.Length - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }


        changeWeapon(selectedWeapon);

        if (previousSpell != selectedWeapon)
        {
            UpdateSpellUI();
        }
    }

    void UpdateSpellUI()
    {
        int i = 0;
        foreach (Button icon in buttons)
        {
            if( i == selectedWeapon)
            {
                icon.transform.localScale *= 1.25f;
            } else
            {
                icon.transform.localScale = Vector3.one;
            }

            i++;
        }
    }

    void changeWeapon(int idx)
    {
        if (idx == 1)
        {
            LevelManager.bulletTextCount.gameObject.SetActive(true);
        } else
        {
            LevelManager.bulletTextCount.gameObject.SetActive(false);
        }
        currentWeapon = weapons[idx];
        currentWeapon.SetActive(true);
        var otherWeapon = weapons[1 - idx];
        otherWeapon.SetActive(false);

        Gun.weaponIndex = idx;
        Gun.currentWeapon = currentWeapon;
    }
}
