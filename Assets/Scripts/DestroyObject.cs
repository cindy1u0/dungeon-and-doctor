using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float destroyDuration = 5;

    void Start()
    {
        Destroy(gameObject, destroyDuration);
    }
}
