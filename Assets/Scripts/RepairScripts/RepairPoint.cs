using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    protected float health = 1;
    protected State currentState = State.NORMAL;
    [SerializeField]
    Image radialTimer;

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
            DealWithDamage();

            radialTimer.fillAmount = health;

            if (isPlayerInTrigger && hasCorrectTool)
            {
                DoingAction();
            }
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
            BecomeDestroyed();
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

    public bool BecomeDamaged()
    {
        if (currentState == State.NORMAL)
        {
            radialTimer.enabled = true;
            currentState = State.DAMAGED;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            EventManager.TriggerEvent(string.Format("{0}Damaged", this.gameObject.name));
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BecomeDestroyed()
    {
        if (currentState != State.DESTROYED)
        {
            currentState = State.DESTROYED;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDamaged()
    {
        return currentState != State.NORMAL;
    }

    void BecomeNormal()
    {
        radialTimer.enabled = false;
        currentState = State.NORMAL;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        EventManager.TriggerEvent(string.Format("{0}Repaired", this.gameObject.name));
    }
}
