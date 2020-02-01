using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBay : MonoBehaviour
{

    private bool respawnActive = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    void respawnPlayers() { 
        if(GameData.player1Dead){ 

        }
        if(GameData.player2Dead){ 
            
        }
        if(GameData.player3Dead){ 
            
        }
    }

    void onPlayerDeath() { 
        respawnActive = true;
    }

    void OnEnable()
    {
        EventManager.StartListening(EventMessage.Death, onPlayerDeath);
        EventManager.StartListening(EventMessage.Respawn, Respawn);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.Death, onPlayerDeath);
        EventManager.StopListening(EventMessage.Respawn, Respawn);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventMessage.Respawn, Respawn);
    }

    public void Respawn()
    {
        var p = Instantiate(GameManager.instance.Player, this.transform.position, GameManager.instance.Player.transform.rotation);
        p.gameObject.tag = "Player1";
        EventManager.TriggerEvent(EventMessage.ResetCamera);
    }
}
