using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBehavior : MonoBehaviour
{
    PlayerHealth playerHealth;
    public int projectileDamage = 10;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Doctor");
        playerHealth = player.GetComponent<PlayerHealth>();

        transform.LookAt(player.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Doctor"))
        {
            playerHealth.TakeDamage(projectileDamage);
        }
    }
}
