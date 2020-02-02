using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var color = GameManager.CameraBloodOverlay.color;

        if (color.a > 0)
        {
            color.a -= 0.005f;
            GameManager.CameraBloodOverlay.color = color;
        }        
    }

    void OnEnable()
    {
        EventManager.StartListening(EventMessage.Blooded, BloodEffect);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.Blooded, BloodEffect);
    }

    void BloodEffect()
    {
        var color = GameManager.CameraBloodOverlay.color;
        color.a += 0.1f;
        GameManager.CameraBloodOverlay.color = color;
    }
}
