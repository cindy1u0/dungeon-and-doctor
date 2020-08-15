using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsBehavior : MonoBehaviour
{
    public Transform playerPos;
    public float textDistance = 5f;
    public float activationDistance = 2.5f;
    public int healthAmount = 2;
    public int bulletAmount = 5;
    public int defenseAmount = 0;
    public AudioClip pickupSFX;
    public AudioClip dropSFX;

    GameObject player;
    Transform floatingText;
    PlayerHealth playerHealth;


    void Start()
    {
        floatingText = transform.Find("FloatingText");

        if (dropSFX != null)
        {
            AudioSource.PlayClipAtPoint(dropSFX, Camera.main.transform.position);
        }

        if (playerPos == null)
        {
            playerPos = GameObject.FindGameObjectWithTag("Doctor").transform;
            player = playerPos.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        floatingText.transform.LookAt(playerPos);

        floatingText.Rotate(Vector3.up, 180);

        // if the player is near us
        if (Vector3.Magnitude(playerPos.position - transform.position) <= textDistance)
        {
            floatingText.gameObject.SetActive(true);

            if (Vector3.Magnitude(playerPos.position - transform.position) <= activationDistance)
            {
                // if the player decides to use the item
                if (Input.GetKey(KeyCode.E))
                {
                    gameObject.SetActive(false);

                    playerHealth.TakeHealth(healthAmount);

                    if (defenseAmount != 0)
                    {
                        playerHealth.IncreaseDefense(defenseAmount);
                        playerHealth.TakeHealth(100);
                        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).gameObject.SetActive(true);
                    }

                    LevelManager.bulletLeft += bulletAmount;

                    // play pickup sound
                    AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);

                    // destroy this gameobject
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            floatingText.gameObject.SetActive(false);
        }
    }
}
