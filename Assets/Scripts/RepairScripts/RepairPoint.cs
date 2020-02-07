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
    [SerializeField]
    protected GameData.RoomType setRoomType;
    [SerializeField]
    Sprite normalSprite;
    [SerializeField]
    Sprite DestroyedSprite;
    protected bool isBroken = false;
    protected float repairRate = 0.1f;
    protected float damageRate = 0.002f;
    [SerializeField]
    protected float health = 1;
    protected State currentState = State.NORMAL;
    protected bool inputDisplay;

    //private Player player;

    [SerializeField]
    Image radialTimer;
    [SerializeField]
    Image outLine;
    [SerializeField]
    Text toolMessage;

    protected virtual void SetUp()
    {
        playerTrigger = this.gameObject.GetComponent<BoxCollider2D>();
        //player = GetComponent<Player>();
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

        if (health == 0)
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

                    inputDisplay = true;
                    DisplayGesture(other);
                }
                else
                {
                    Debug.Log("Wrong Tool, you need the " + neededToolType);
                    toolMessage.text = "Wrong Tool!\n " + neededToolType.ToString() + " Needed";                    
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            Debug.Log("StopListening");

            EventManager.StopListening(string.Format("Player{0}{1}", (int)other.gameObject.GetComponent<Player>().PlayerNum, neededToolType), Repair);
        }

        if (other.gameObject.GetComponent<Player>().CurrentTool != null)
        {
            if (other.gameObject.GetComponent<Player>().CurrentTool.myType == neededToolType)
            {
                inputDisplay = false;
                DisplayGesture(other);
            }
        }
    }

    public bool BecomeDamaged()
    {
        if (currentState == State.NORMAL)
        {
            radialTimer.enabled = true;
            outLine.enabled = true;
            currentState = State.DAMAGED;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = DestroyedSprite;
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

            isBroken = true;
            UpdateRoomBreak();

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
        this.gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
        EventManager.TriggerEvent(string.Format("{0}Repaired", this.gameObject.name));

        isBroken = false;
        UpdateRoomBreak();
    }

    private void UpdateRoomBreak()
    {
        Debug.Log(setRoomType + " break is " + isBroken);
        isBroken = !isBroken;

        switch (setRoomType)
        {
            case GameData.RoomType.Thruster:
                ThrusterBroken();
                break;
            case GameData.RoomType.Gravity:
                GravityBroken();
                break;
            case GameData.RoomType.Ring:
                RingBroken();
                break;
            case GameData.RoomType.Camera:
                CameraBroken();
                break;
        }
    }

    private void ThrusterBroken()
    {
        if (isBroken)
        {
            GameData.shipSpeed = 0;
        }
        else
        {
            GameData.shipSpeed = 1;
        }
    }

    private void GravityBroken()
    {
        GameData.gravityIsWorking = isBroken;
    }

    private void RingBroken()
    {
        if (isBroken)
        {
            GameData.rotationSpeed = 0.3f;
        }
        else
        {
            GameData.rotationSpeed = 0.6f;
        }
    }

    private void CameraBroken()
    {
        //Switch room labels and UI info off on minimap
        Debug.Log("Camera's bbroke bro!");
    }

    private void DisplayGesture(Collider2D collider)
    {
        var currentTool = collider.GetComponent<Player>().currentTool;

        if (inputDisplay)
        {
            switch (currentTool.myType)
            {
                case GameData.ToolType.HAMMER:
                    toolMessage.text = "Tap the " + "A Button";
                    break;

                case GameData.ToolType.DRILL:
                    toolMessage.text = "Hold the " + "R Trigger";
                    break;

                case GameData.ToolType.SAW:
                    toolMessage.text = "Tap the " + "X Button";
                    break;

                case GameData.ToolType.SCREWDRIVER:
                    toolMessage.text = "Tap the " + "Y Button";
                    break;

                case GameData.ToolType.WRENCH:
                    toolMessage.text = "Tap the " + "B Button"; ;
                    break;
            }
        }   
        else
        {
            Debug.Log("Input unavaliable");
        }
    }
}