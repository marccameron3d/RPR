using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpin : MonoBehaviour
{
    public float particleSpinSpeed = 0.2f;
    // Update is called once per frame
    void Update()
    {
       transform.Rotate(0.0f, 0.0f, particleSpinSpeed, Space.Self);
    }
}
