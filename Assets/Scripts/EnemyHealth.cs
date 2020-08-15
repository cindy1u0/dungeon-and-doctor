using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 2;
    public int currentHealth;
    public Slider healthSlider;
    public AudioClip enemyDamaged;

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            AudioSource.PlayClipAtPoint(enemyDamaged, transform.position);

            currentHealth -= damage;
            healthSlider.value = currentHealth;
        }
    }
}
