using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour
{
    public int damage = 10;
    public int infectedDmgInterval = 1;

    EnemyHealth enemyHealth;
    float timer;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    // Player take damage from the enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            if (FindObjectOfType<DropletBehavior>())
            {
                FindObjectOfType<DropletBehavior>().Splash();
            }
            enemyHealth.TakeDamage(other.GetComponent<DropletBehavior>().damage);
        }

        if (other.CompareTag("Doctor") && enemyHealth.currentHealth > 0)
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Inflicts infected damage on player for every *infectedDmgInterval* seconds of triggering the player
        if (other.CompareTag("Doctor") && enemyHealth.currentHealth > 0 && timer >= infectedDmgInterval)
        {
            timer = 0;
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeInfectedDamage(damage);
        }
    }
}
