using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Respawn Player");
            Respawn();
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventMessage.Respawn, Respawn);
    }

    void OnDisable()
    {
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
