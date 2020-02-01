using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    List<RepairPoint> repairPoints;

    void OnEnable()
    {
        for(int i = 0; i <repairPoints.Count; i++)
        {
            if (repairPoints[i] != null)
            {
                EventManager.StartListening(string.Format("{0}Repaired", repairPoints[i].name), PointRepaired);
                EventManager.StartListening(string.Format("{0}Damaged", repairPoints[i].name), PointDamaged);
            }
        }
        BreakRandomPoint();
    }

    void OnDisable()
    {
        for (int i = 0; i < repairPoints.Count; i++)
        {
            if (repairPoints[i] != null)
            {
                EventManager.StopListening(string.Format("{0}Repaired", repairPoints[i].name), PointRepaired);
                EventManager.StopListening(string.Format("{0}Damaged", repairPoints[i].name), PointDamaged);
            }
        }
    }

    bool BreakRandomPoint()
    {
        int rand = Random.Range(0, repairPoints.Count);
        return BreakARepairPoint(rand);
    }

    bool BreakARepairPoint(int _index)
    {
        if(_index < repairPoints.Count)
        {
            return repairPoints[_index].BecomeDamaged();
            GameData.shipHealth -= GameData.repairPointValue;
        }
        else
        {
            Debug.LogError("Repair Point index out of length");
            return false;
        }
    }

    void PointDamaged()
    {
        Debug.Log("GameCont: Point damaged");
        GameData.shipHealth -= GameData.repairPointValue;
    }

    void PointRepaired()
    {
        Debug.Log("GameCont: Point Repaied");
        GameData.shipHealth += GameData.repairPointValue;
    }
}
