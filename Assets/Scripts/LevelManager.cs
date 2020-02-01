using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    float shipProgress = 0.0f;
    float shipGoalDistance = 100.0f;
    struct RepairEvent
    {
        public int numberToBreak;
        public float distanceToBreakAt;
    }

    Queue<RepairEvent> repairEvents;

    // Start is called before the first frame update
    void Start()
    {
        repairEvents = new Queue<RepairEvent>();
        RepairEvent newEvent = new RepairEvent();
        newEvent.numberToBreak = 0;
        newEvent.distanceToBreakAt = 0.5f;

        repairEvents.Enqueue(newEvent);
    }

    // Update is called once per frame
    void Update()
    {
        shipProgress = Mathf.Clamp(shipProgress + GameData.shipSpeed * Time.deltaTime, 0, shipGoalDistance);
        Debug.Log("Ship progress" + shipProgress);

        if (repairEvents.Count > 0)
        {
            if (shipProgress >= repairEvents.Peek().distanceToBreakAt)
            {
                RepairEvent activeEvent = repairEvents.Dequeue();

            }
        }
    }
}
