using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyCamera : MonoBehaviour
{
    // Update is called once per frame
    public List<SpriteRenderer> blood = new List<SpriteRenderer>();

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
       // var b = Instantiate(blood[Random.Range(0,blood.Count)], new Vector3(this.transform.position.x, this.transform.position.y, 1f), this.transform.rotation);
        //b.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
       // b.transform.parent = this.transform;
    }
}
