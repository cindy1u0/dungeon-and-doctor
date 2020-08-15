using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    public bool isFloating = true;
    public float floatAmount = 0.5f;

    private Vector3 originalPos;
    private Vector3 targetPos;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (isFloating)
        {
            targetPos = originalPos;
            targetPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI) * floatAmount;
            transform.position = targetPos;
        }
    }
}
