using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBay : MonoBehaviour {

    private bool respawnActive = false;

    // Start is called before the first frame update
    void Start() {

    }

    public void respawnPlayers() {
        if (respawnActive) {
            if (!GameData.player1Alive) {
                Respawn("Player1", GameData.PlayerNumber.PLAYER_1);
                Debug.Log("Respawning player 1");
            }
            if (!GameData.player2Alive) {
                Respawn("Player2", GameData.PlayerNumber.PLAYER_2);
                Debug.Log("Respawning player 2");
            }
            if (!GameData.player3Alive) {
                Respawn("Player3", GameData.PlayerNumber.PLAYER_3);
                Debug.Log("Respawning player 3");
            }
        }
        respawnActive = false;
    }

    void onPlayerDead() {
        respawnActive = true;
    }

    void OnEnable() {
        EventManager.StartListening(EventMessage.Death, onPlayerDead);

    }

    void OnDisable() {
        EventManager.StopListening(EventMessage.Death, onPlayerDead);
    }

    private void OnDestroy() {
        EventManager.StopListening(EventMessage.Death, onPlayerDead);
    }

    public void Respawn(string tag, GameData.PlayerNumber number) {
        var p = Instantiate(GameManager.instance.Player, this.transform.position, GameManager.instance.Player.transform.rotation);
        p.gameObject.tag = tag;
        p.gameObject.GetComponent<Player>().selectedPlayer = number;
        EventManager.TriggerEvent(EventMessage.ResetCamera);
    }
}