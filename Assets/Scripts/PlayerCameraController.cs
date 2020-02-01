using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {

    [SerializeField]
    private GameData.PlayerNumber assignedPlayer;

    public Transform player;
    public float shakeTime = 3f;
    private float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    private void Awake () {
        AssignPlayer(assignedPlayer);
    }

    private void Update () {

        if (shakeDuration > 0)
        {
            transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10) + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10); 
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventMessage.ResetCamera, ResetCamera);
        EventManager.StartListening(EventMessage.CameraShake, CameraShake);

    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.ResetCamera, ResetCamera);
        EventManager.StopListening(EventMessage.CameraShake, CameraShake);

    }

    private void CameraShake()
    {
        Debug.Log("Camera Shake");
        shakeDuration = shakeTime;   
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