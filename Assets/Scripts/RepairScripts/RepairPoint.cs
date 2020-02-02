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
    [SerializeField]
    protected GameData.ToolType neededToolType;
    protected float repairRate = 0.1f;
    protected float damageRate = 0.002f;
    [SerializeField]
    protected float health = 1;
    protected State currentState = State.NORMAL;
    
    [SerializeField]
    Image radialTimer;
    [SerializeField]
    Image outLine;
    [SerializeField]
    Text toolMessage;
    
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
        }
    }

    protected void Repair()
    {
        if (currentState != State.NORMAL)
        {
            health = Mathf.Clamp(health + repairRate, 0, 1);

            if (health == 1)
            {
                BecomeNormal();
            }
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
            if (other.gameObject.GetComponent<Player>().CurrentTool != null)
            {
                if (other.gameObject.GetComponent<Player>().CurrentTool.myType == neededToolType)
                {
                    Debug.Log("StartListening");
                    EventManager.StartListening(string.Format("Player{0}{1}", (int)other.gameObject.GetComponent<Player>().PlayerNum, neededToolType), Repair);
                }
                else
                {
                    Debug.Log("Wrong tool, need " + neededToolType);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            Debug.Log("StopListening");
            toolMessage.text = neededToolType.ToString() + " Needed";
            EventManager.StopListening(string.Format("Player{0}{1}", (int)other.gameObject.GetComponent<Player>().PlayerNum, neededToolType), Repair);
        }
    }

    public bool BecomeDamaged()
    {
        if (currentState == State.NORMAL)
        {
            radialTimer.enabled = true;
            outLine.enabled = true;
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
            GameData.shipHealth -= GameData.repairPointValue / 4.0f;
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
        outLine.enabled = false;
        currentState = State.NORMAL;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        EventManager.TriggerEvent(string.Format("{0}Repaired", this.gameObject.name));
    }
}
