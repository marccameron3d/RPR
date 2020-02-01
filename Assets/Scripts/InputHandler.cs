using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    [SerializeField]
    private GameData.PlayerNumber selectedPlayer;

    private Player player;

    private void Awake () {
        player = GetComponent<Player> ();
    }

    private void FixedUpdate () {
        float xInput = 0;
        float yInput = 0;
        switch (selectedPlayer) {
            case GameData.PlayerNumber.PLAYER_1:
                xInput = Input.GetAxis ("Horizontal1");
                yInput = Input.GetAxis ("Vertical1");
                break;
            case GameData.PlayerNumber.PLAYER_2:
                xInput = Input.GetAxis ("Horizontal2");
                yInput = Input.GetAxis ("Vertical2");
                break;
        }
        player.TakeInput (xInput, yInput);
    }

}