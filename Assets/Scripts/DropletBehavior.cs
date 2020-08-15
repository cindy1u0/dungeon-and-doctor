using UnityEngine;
using System.Collections;

public class DropletBehavior : MonoBehaviour
{
    public int damage = 10;
    public GameObject splashEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Splash();
    }

    private void OnTriggerEnter(Collider other)
    {
        Splash();
    }

    public void Splash()
    {
        gameObject.SetActive(false);

        Destroy(gameObject, 3);

        GameObject effect = Instantiate(splashEffect, transform.position, transform.rotation) as GameObject;

        Destroy(effect, 1);
    }
}
