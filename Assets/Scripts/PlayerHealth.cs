using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public Slider healthSlider;
    public int startingImmunity = 100;
    public Slider immunitySlider;
    public AudioClip attackSFX;
    public AudioClip dropSFX;
    public float attackVolume = 0.6f;
    public int diseaseInterval = 1;
    public int intervalBeforeRecovery = 5;
    public int recoveryRate = 10;

    int currentHealth;
    int defenseAmount;
    int currentImmunity;
    float diseaseTimer = 0;
    float recoveryTimer = 0;


    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        currentImmunity = startingImmunity;
        immunitySlider.value = currentImmunity;
    }

    void Update()
    {
        diseaseTimer += Time.deltaTime;
        recoveryTimer += Time.deltaTime;

        // Player takes 5 damage every *diseaseInterval* seconds if immunity is below or equals to 0
        if (currentImmunity <= 0 && diseaseTimer >= diseaseInterval)
        {
            diseaseTimer = 0;
            TakeDamage(5);
        }

        if (recoveryTimer >= intervalBeforeRecovery)
        {
            currentImmunity = Mathf.Clamp(currentImmunity + recoveryRate, 0, startingImmunity);
            immunitySlider.value = currentImmunity;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        int damage = damageAmount - defenseAmount;

        if (damage < 0)
        {
            damage = 0;
        }

        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            AudioSource.PlayClipAtPoint(attackSFX, Camera.main.transform.position, attackVolume);
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }

    public void TakeInfectedDamage(int damageAmount)
    {
        recoveryTimer = 0;

        int damage = damageAmount - defenseAmount;

        if (damage < 0)
        {
            damage = 0;
        }

        if (currentImmunity > 0)
        {
            currentImmunity = Mathf.Clamp(currentImmunity - damage, 0, startingImmunity);
            immunitySlider.value = currentImmunity;
            // Add a SFX or VFX for this would be bestt
        }
    }

    public void TakeHealth(int health)
    {
        if (currentHealth < startingHealth)
        {
            currentHealth += health;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, startingHealth);
        }
    }

    public void IncreaseDefense(int defense)
    {

        StartCoroutine(WaitDefense(defense));
    }

    IEnumerator WaitDefense(int defense)
    {
        defenseAmount += defense;

        yield return new WaitForSeconds(20f);

        defenseAmount -= defense;

        AudioSource.PlayClipAtPoint(dropSFX, Camera.main.transform.position);

        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).gameObject.SetActive(false);
    }

    void PlayerDies()
    {
        transform.Rotate(-90, 0, 0, Space.Self);
        FindObjectOfType<LevelManager>().LevelLost();
    }
}
