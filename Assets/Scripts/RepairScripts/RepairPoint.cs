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
    [SerializeField]
    bool isPlayerInTrigger = false;
    [SerializeField]
    bool hasCorrectTool = false;
    protected GameData.ToolType neededToolType;
    protected float repairRate = 0.1f;
    protected float damageRate = 0.002f;
    protected float health = 1;
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
            if (isPlayerInTrigger )
            {
                DoingAction();
            }

            DealWithDamage();
        }
    }

    protected void Repair()
    {
        health = Mathf.Clamp(health + repairRate, 0, 1);

        Debug.Log(health);
        if (health == 1)
        {
            BecomeNormal();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            if(other.gameObject.GetComponent<Player>().CurrentTool == neededToolType)
            {
                hasCorrectTool = true;
            }

            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            hasCorrectTool = false;
            isPlayerInTrigger = false;
        }
    }

    public virtual bool DoingAction()
    {
        return false;
    }

    public void BecomeDamaged()
    {
        currentState = State.DAMAGED;
    }

    public bool IsDamaged()
    {
        return currentState != State.NORMAL;
    }

    void BecomeNormal()
    {
        currentState = State.NORMAL;
    }
}
