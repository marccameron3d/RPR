using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<RepairPoint> repairPoints;

    public static GameManager instance = null;
    public static GameObject ChunkManager;
    public static GameObject CloneBay;

    float shipProgress = 0.0f;
    float shipGoalDistance = 100.0f;
    struct RepairEvent
    {
        public int numberToBreak;
        public float distanceToBreakAt;
    }

    [SerializeField]
    Slider progressSlider, healthSlider;
    Queue<RepairEvent> repairEvents;


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


        repairEvents = new Queue<RepairEvent>();
        RepairEvent newEvent = new RepairEvent();
        newEvent.numberToBreak = 0;
        newEvent.distanceToBreakAt = 10.0f;

        repairEvents.Enqueue(newEvent);
    }

    //Update is called every frame.
    void Update()
    {
        shipProgress = Mathf.Clamp(shipProgress + GameData.shipSpeed * Time.deltaTime, 0, shipGoalDistance);

        progressSlider.value = shipProgress/shipGoalDistance;
        healthSlider.value = GameData.shipHealth;

        if (repairEvents.Count > 0)
        {
            if (shipProgress >= repairEvents.Peek().distanceToBreakAt)
            {
                Debug.Log(string.Format("breaking point: {0}   at Progress {1}   ship is at {2}", repairEvents.Peek().numberToBreak, repairEvents.Peek().distanceToBreakAt, shipProgress));
                BreakARepairPoint(repairEvents.Dequeue().numberToBreak);
            }
        }
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