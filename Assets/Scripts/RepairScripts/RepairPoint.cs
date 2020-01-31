using UnityEngine;

public class RepairPoint : MonoBehaviour
{
    protected enum State
    {
        NORMAL,
        DAMAGED,
        DESTROYED,
        NULL
    }

    BoxCollider2D playerTrigger;
    bool isPlayerInTrigger = false;
    bool hasCorrectTool = false;
    protected GameData.ToolType neededToolType;
    protected float repairRate;
    protected float damageRate;
    protected float damageLevel;
    protected float health;
    protected State currentState = State.NORMAL;

    void Start()
    {
        SetUp();
    }

    protected virtual void SetUp()
    {
        playerTrigger = this.gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (currentState != State.NORMAL)
        {
            if (isPlayerInTrigger)
            {
                DoingAction();
            }

            DealWithDamage();
        } 
    }

    void DealWithDamage()
    {
        health = Mathf.Clamp(health - damageRate, 0, 1);

        if(health == 0)
        {
            currentState = State.DESTROYED;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    public virtual bool DoingAction()
    {
        return false;
    }
}
