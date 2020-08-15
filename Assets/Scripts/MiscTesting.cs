using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.localEulerAngles = Vector3.Lerp(new Vector3(90, 0, 0), new Vector3(0, 0, 0), Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
