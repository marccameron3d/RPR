using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {

    [SerializeField]
    private GameData.PlayerNumber assignedPlayer;

    public Transform player;

    private void Awake () {
        AssignPlayer(assignedPlayer);
    }

    private void Update () {
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventMessage.ResetCamera, ResetCamera);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.ResetCamera, ResetCamera);
    }

    private void ResetCamera()
    {
        Debug.Log("Reset Camera");
        AssignPlayer(assignedPlayer);
    }

    private void AssignPlayer(GameData.PlayerNumber assignedPlayer)
    {
        switch (assignedPlayer)
        {
            case GameData.PlayerNumber.PLAYER_1:
                player = GameObject.FindGameObjectWithTag("Player1").transform;
                break;
            case GameData.PlayerNumber.PLAYER_2:
                player = GameObject.FindGameObjectWithTag("Player2").transform;
                break;
        }
    }
}