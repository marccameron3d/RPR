﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    enum GenstureType
    {
        HAMMER, 
        CIRCLE,
        TRIGGER_PULL
    }

    private Player player;

    private void Awake () {
        player = GetComponent<Player> ();

        if(player.PlayerNum == GameData.PlayerNumber.NULL)
        {
            Debug.LogError("Your player number is null");
        }
    }

    private void FixedUpdate()
    {
        float xInput = 0;
        float yInput = 0;
        if (player.PlayerNum != GameData.PlayerNumber.NULL)
        { 
            xInput = Input.GetAxis(string.Format("Horizontal{0}", (int)player.PlayerNum));
            yInput = Input.GetAxis(string.Format("Vertical{0}", (int)player.PlayerNum));
            player.TakeInput(xInput, yInput);
            CalculateGesture();
        }
    }

    void CalculateGesture()
    {
        //if(Input.GetButtonUp(string.Format("Horizontal{0}", (int)player.PlayerNum)))
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.TriggerEvent(string.Format("Player{0}{1}", (int)player.PlayerNum, GameData.ToolType.HAMMER));
        }
    }

}