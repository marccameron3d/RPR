﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class InputHandler : MonoBehaviour {

    float drillPos = 0.0f;
    bool completeGesture = false;

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
        switch (player.CurrentTool)
        {
            case GameData.ToolType.HAMMER: 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    completeGesture = true;
            }; break;

            case GameData.ToolType.DRILL:; break;

            case GameData.ToolType.SAW:; break;

            case GameData.ToolType.SCREWDRIVER:; break;

            case GameData.ToolType.WRENCH:; break;
            
        }

        if (completeGesture)
        {
            EventManager.TriggerEvent(string.Format("Player{0}{1}", (int)player.PlayerNum, player.CurrentTool));
            completeGesture = false;
        }
    }
}