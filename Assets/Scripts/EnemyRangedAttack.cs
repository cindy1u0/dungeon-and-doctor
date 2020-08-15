using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    public Transform attackOriginPos; // where it is attacking from
    public GameObject attackProjectile; // the object used to attack player
    //public float throwForce = 2f;
    public float projectileSpeed = 2f;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Doctor");
    }

    public void RangedAttack()
    {
        GameObject projectile = Instantiate(attackProjectile,
                   attackOriginPos.position + attackOriginPos.forward, attackOriginPos.rotation) as GameObject;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 dirToPlayer = player.transform.position - projectile.transform.position;

        dirToPlayer.Normalize();

        rb.AddForce(dirToPlayer * projectileSpeed, ForceMode.Impulse);
    }
}
