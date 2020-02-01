using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {

    [SerializeField]
    private GameData.PlayerNumber assignedPlayer;

    private Transform player;

    private void Awake () {
        switch (assignedPlayer) {
            case GameData.PlayerNumber.PLAYER_1:
                player = GameObject.FindGameObjectWithTag ("Player1").transform;
                break;
            case GameData.PlayerNumber.PLAYER_2:
                player = GameObject.FindGameObjectWithTag ("Player2").transform;
                break;
        }
    }

    private void Update () {
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
    }
}