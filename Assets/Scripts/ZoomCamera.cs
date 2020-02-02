using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField]
    List<Transform> players;
    float minZoom = 3.0f, maxZoom = 7.5f, currentZoom, prevZoom;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize = CalculateZoom();
        Camera.main.gameObject.transform.position = CalculatePosition();
    }

    float CalculateZoom()
    {
        Vector3 minPos = Vector3.zero; 
        Vector3 maxPos = Vector3.zero;
        int x = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                if(players[i].transform.position.x <= minPos.x)
                {
                    minPos.x = players[i].transform.position.x;
                }

                if (players[i].transform.position.x >= maxPos.x)
                {
                    maxPos.x = players[i].transform.position.x;
                }

                if (players[i].transform.position.x <= minPos.y)
                {
                    minPos.y = players[i].transform.position.y;
                }

                if (players[i].transform.position.x >= maxPos.y)
                {
                    maxPos.y = players[i].transform.position.y;
                }

                x++;
            }
        }


        prevZoom = currentZoom;


        if (x > 1)
        {
            currentZoom = Mathf.Clamp(Vector3.Distance(minPos, maxPos), minZoom, maxZoom);
        }
        else
        {
            currentZoom = minZoom;
        }

        Mathf.Lerp(prevZoom, currentZoom, Time.deltaTime);

        return currentZoom;
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

    void RegisterPlayer(Transform _playerToAdd)
    {
        players.Add(_playerToAdd);
    }

    void DeregisterPlayer(Transform _playerToRemove)
    {
        players.Remove(_playerToRemove);
    }
}
