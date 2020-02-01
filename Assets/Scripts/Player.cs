using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float thrust = 10.0f;
    public float speedMultiplier = 100f;
    public float maxSpeed = 4f;
    public float axisDampining = 0.1f;
    private Rigidbody2D rb2D;
    [SerializeField]
    private GameData.ToolType currentTool;

    void Start()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        currentTool = GameData.ToolType.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // set the max speed of the obj
        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }
        
        // player controls
        rb2D.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0) * thrust * Time.deltaTime * speedMultiplier);
        rb2D.AddForce(new Vector2(0,Input.GetAxis("Vertical")) * thrust * Time.deltaTime * speedMultiplier);

        // look direction
        if (Input.GetAxis("Horizontal") > 0 + axisDampining)
            FlipSprite();
        else if (Input.GetAxis("Horizontal") < 0 - axisDampining)
            FlipSprite(true);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventMessage.GravityOff, GravityOff);
        EventManager.StartListening(EventMessage.GravityOn, GravityOn);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.GravityOff, GravityOff);
        EventManager.StartListening(EventMessage.GravityOn, GravityOn);
    }

    void GravityOff() {
        this.rb2D.gravityScale = 0;
        this.rb2D.drag = 0;
    }

    void GravityOn()
    {
        this.rb2D.gravityScale = 1;
        this.rb2D.drag = 2;
    }

    void OnCollisionStay2D(Collision2D coll)
    {

    }

    void FlipSprite(bool IsLeft = false)
    {
        this.transform.localScale = new Vector3((IsLeft ? -1 : 1), 1, 1);
    }

    public GameData.ToolType CurrentTool
    {
        get
        {
            return currentTool;
        }

        set
        {
            currentTool = value;
        }
    }
}
