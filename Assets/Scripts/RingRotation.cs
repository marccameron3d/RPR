using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{

    private float rotationSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }
}
