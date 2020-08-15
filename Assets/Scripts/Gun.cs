using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun: MonoBehaviour
{
    public GameObject[] weapons;
    public float speed = 20;
    public GameObject droplet;
    public Image reticleImage;
    public Color reticleColor;
    public AudioClip splatSFX;
    public AudioClip swingSFX;
    public int swordDamage = 10;
    public float attachDis = 1.5f;

    GameObject currentEffect;

    Color originalReticle;

    public static GameObject currentWeapon;

    public static int weaponIndex;

    Animation anim;

    // Start is called before the first frame update
    void Start() {
        originalReticle = reticleImage.color;
        currentEffect = droplet;
        weaponIndex = 0;
        currentWeapon = weapons[weaponIndex];

        anim = weapons[0].GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update() {
        if (!PauseMenuBehavior.isGamePaused)
        {
            reticleImage.enabled = true;
            if (!LevelManager.isGameOver)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (weaponIndex == 1)
                    {
                        if (LevelManager.bulletLeft > 0)
                        {
                            GameObject proj = Instantiate(currentEffect, transform.position + transform.forward, droplet.transform.rotation) as GameObject;

                            proj.SetActive(true);

                            Rigidbody rb = proj.GetComponent<Rigidbody>();

                            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

                            AudioSource.PlayClipAtPoint(splatSFX, Camera.main.transform.position);

                            proj.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

                            LevelManager.bulletLeft--;
                        }

                    }
                    else
                    {
                        anim.Play("Swing");
                        AudioSource.PlayClipAtPoint(swingSFX, Camera.main.transform.position);
                        DamageEnemy();
                    }
                }
            }
        } else
        {
            reticleImage.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (!PauseMenuBehavior.isGamePaused)
        {
            Shoot();
        }
    }

    private void DamageEnemy()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, attachDis))
        {
            if (hit.collider.CompareTag("Virus"))
            {
                var enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(swordDamage);
            }
        }
    }

    // raycast mechanism
    void Shoot() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Virus"))
            {

                reticleImage.color = Color.Lerp(reticleImage.color, reticleColor, Time.deltaTime * 3);

                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1),
                    Time.deltaTime * 2);
            }
            else
            {

                reticleImage.color = Color.Lerp(reticleImage.color, originalReticle, Time.deltaTime * 3);

                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one,
                    Time.deltaTime * 2);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, originalReticle, Time.deltaTime * 3);

            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one,
                Time.deltaTime * 2);
        }
    }
}
