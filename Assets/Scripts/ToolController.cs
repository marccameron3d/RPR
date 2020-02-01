using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    private GameData.ToolType selectedTool;

    public void ToolSelector()
    {
        switch(selectedTool)
        {
            case GameData.ToolType.SCREWDRIVER:
                InputScrewdriver();
                break;
            case GameData.ToolType.HAMMER:
                InputHammer();
                break;
            case GameData.ToolType.SAW:
                InputSaw();
                break;
            case GameData.ToolType.DRILL:
                InputDrill();
                break;
            case GameData.ToolType.WRENCH:
                InputWrench();
                break;
        }
    }

    private void InputScrewdriver()
    {
        Debug.Log("Input screwdriver controls");
    }

    private void InputHammer()
    {
        Debug.Log("Input screwdriver controls");
    }

    private void InputSaw()
    {
        Debug.Log("Input screwdriver controls");
    }

    private void InputDrill()
    {
        Debug.Log("Input screwdriver controls");
    }
    private void InputWrench()
    {
        Debug.Log("Input screwdriver controls");
    }
}
