using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed = .5f;

    void Start() { }

    void FixedUpdate() {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }
}