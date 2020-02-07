using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishDetection : MonoBehaviour
{
    private bool touchingRing = false;
    private bool touchingTunnel = false;

    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "RingWall"){
            touchingRing = true;
        }
        if(other.gameObject.tag == "TunnelWall"){
            touchingTunnel = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "RingWall"){
            touchingRing = false;
        }
        if(other.gameObject.tag == "TunnelWall"){
            touchingTunnel = false;
        }
    }

    private void FixedUpdate() {
        if(touchingTunnel && touchingRing){
            player.Die();
        }
    }
}