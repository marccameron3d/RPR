using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField]
    List<Transform> players;
    float minZoom = 3.0f, maxZoom = 7.5f, currentZoom, prevZoom;

    public float shakeTime = 3f;
    private float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (players.Count == 0)
        {
            Debug.LogError($"No players added to the players list on ZoomCamera {this.gameObject.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize = CalculateZoom();

        if (shakeDuration > 0)
        {
            transform.localPosition = CalculatePosition() + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            Camera.main.gameObject.transform.position = CalculatePosition();
        }
    }

    float CalculateZoom()
    {
        prevZoom = currentZoom;


        if (players.Count > 1)
        {
            currentZoom = Mathf.Clamp(Vector3.Distance(players[0].position, players[1].position), minZoom, maxZoom);
        }
        else
        {
            currentZoom = minZoom;
        }

        return currentZoom;
    }

    void OnEnable()
    {
        EventManager.StartListening(EventMessage.CameraShake, CameraShake);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.CameraShake, CameraShake);
    }

    private void CameraShake()
    {
        shakeDuration = shakeTime;
    }


    Vector3 CalculatePosition()
    {
        Vector3 cachedPos = Vector3.zero;

        int x = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                cachedPos += players[i].position;
                x++;
            }
        }

        cachedPos /= x;

        cachedPos.z = -10.0f;

        return cachedPos;
    }

    public void RegisterPlayer(Transform _playerToAdd)
    {
        players.Add(_playerToAdd);
    }

    public void DeregisterPlayer(Transform _playerToRemove)
    {
        players.Remove(_playerToRemove);
    }
}
