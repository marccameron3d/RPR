using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<RepairPoint> repairPoints;

    public static GameManager instance = null;
    public static GameObject ChunkManager;
    public static GameObject CloneBay;

    public GameObject Player;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void InitGame()
    {
        ChunkManager = GameObject.FindGameObjectWithTag("ChunkManager");
        CloneBay = GameObject.FindGameObjectWithTag("CloneBay");
        if (Player == null)
            Debug.LogError("You need to add the player prefab to the Game Manager Inspector bro");
    }

    //Update is called every frame.
    void Update()
    {

    }

    void OnEnable()
    {
        for (int i = 0; i < repairPoints.Count; i++)
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
        if (_index < repairPoints.Count)
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