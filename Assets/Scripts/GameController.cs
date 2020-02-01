using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    List<RepairPoint> repairPoints;
    void Start()
    {
        BreakARepairPoint(0);
    }

    bool BreakARepairPoint(int _index)
    {
        if(_index < repairPoints.Count)
        {
            return repairPoints[_index].BecomeDamaged();
        }
        else
        {
            Debug.LogError("Repair Point index out of length");
            return false;
        }
    }
}
