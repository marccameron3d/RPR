using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (player.PlayerNum == GameData.PlayerNumber.NULL) {
            Debug.LogError ("Your player number is null");
        }
    }

    private void FixedUpdate () {
        float xInput = 0;
        float yInput = 0;
        if (player != null)
        {
            if (player.PlayerNum != GameData.PlayerNumber.NULL)
            {
                xInput = Input.GetAxis(string.Format("Horizontal{0}", (int)player.PlayerNum));
                yInput = Input.GetAxis(string.Format("Vertical{0}", (int)player.PlayerNum));
                player.TakeInput(xInput, yInput);

                if (player.CurrentTool != null)
                {
                    CalculateGesture();
                    if (Input.GetKeyDown(KeyCode.Y))
                    {
                        player.CurrentTool.DropTool();
                    }
                }
            }
        }
    }

     private void Update () {
        if (player.PlayerNum != GameData.PlayerNumber.NULL) {
            if (Input.GetButtonDown (string.Format ("ButtonA{0}", (int) player.PlayerNum))) {
                onAPressed ();
            }
        }
    }

    void CalculateGesture()
    {
        switch (player.CurrentTool.myType)
        {
            case GameData.ToolType.HAMMER:
                InputHammer();
                break;
               
            case GameData.ToolType.DRILL:
                InputDrill();
                break;

            case GameData.ToolType.SAW:
                InputSaw();
                break;

            case GameData.ToolType.SCREWDRIVER:
                InputScrewdriver();
                break;

            case GameData.ToolType.WRENCH:
                InputWrench();
                break;
        }
               
        if (completeGesture)
        {
            EventManager.TriggerEvent(string.Format("Player{0}{1}", (int)player.PlayerNum, player.CurrentTool.myType));
            completeGesture = false;
        }
    
    }

    void onAPressed () {
        Debug.Log ("A pressed " + player.PlayerNum);
    }

    void onBPressed () {
        Debug.Log ("B pressed " + player.PlayerNum);
    }

    private void InputScrewdriver()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Input screwdriver controls");
            completeGesture = true;
        }        
    }

    private void InputHammer()
    {        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Input hammer controls");
            completeGesture = true;
        }
    }

    private void InputSaw()
    {       
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Input saw controls");
            completeGesture = true;
        }
    }

    private void InputDrill()
    {        
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Input drill controls");
            completeGesture = true;
        }
    }
    private void InputWrench()
    {        
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Input wrench controls");
            completeGesture = true;
        }
    }
}