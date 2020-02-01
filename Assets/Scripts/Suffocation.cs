using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suffocation : MonoBehaviour {

    [SerializeField]
    private float suffocationLimit;

    private float suffocationTimer = 0f;
    private bool inSpace = false;
    private Player player;

    private void Awake () {
        player = GetComponent<Player> ();
    }

    private void Update () {
        suffocationTimer = Mathf.Clamp ((inSpace ? Time.deltaTime : -Time.deltaTime) + suffocationTimer, 0, suffocationLimit);
        if (suffocationTimer >= suffocationLimit) {
            player.Die();
        }
        Debug.Log (suffocationTimer);
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Space") {
            inSpace = true;
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if (other.gameObject.tag == "Space") {
            inSpace = false;
        }
    }
}