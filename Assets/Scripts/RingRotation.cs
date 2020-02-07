using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour {

    [SerializeField]
    private bool outerRing = false;

    void FixedUpdate() {
        transform.Rotate(new Vector3(0, 0, GameData.rotationSpeed * (outerRing ? -0.8f : 1)));
    }
}