using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float thrust = 10.0f;
    public float speedMultiplier = 100f;
    public float maxSpeed = 4f;
    public float axisDampining = 0.1f;
    private Rigidbody2D rb2D;
    public GameObject Chunks;
    public int chunkCount = 2;
    public bool isDead = false;
    [SerializeField]
    private GameData.ToolType currentTool = GameData.ToolType.NONE;
    private Vector3 defaultScale;
    private float bloodSplash = 0.3f;
    
    [SerializeField]
    GameData.PlayerNumber selectedPlayer;
    public float chunkForce = 0.2f;

    private void Awake()
    {
        this.defaultScale = this.transform.localScale;
    }
    void Start()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        currentTool = GameData.ToolType.NONE;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EventManager.TriggerEvent(EventMessage.Death);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            EventManager.TriggerEvent(EventMessage.GravityOn);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            EventManager.TriggerEvent(EventMessage.GravityOff);
        }
    }

    public void TakeInput(float x, float y)
    {
        // player controls
        rb2D.AddForce(new Vector2(x, 0) * thrust * Time.deltaTime * speedMultiplier);
        rb2D.AddForce(new Vector2(0, y) * thrust * Time.deltaTime * speedMultiplier);

        // look direction
        if (x > 0 + axisDampining)
            FlipSprite();
        else if (y < 0 - axisDampining)
            FlipSprite(true);
    }

    void FixedUpdate()
    {
        // set the max speed of the obj
        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }
        

    }

    void OnEnable()
    {
        EventManager.StartListening(EventMessage.GravityOff, GravityOff);
        EventManager.StartListening(EventMessage.GravityOn, GravityOn);
        EventManager.StartListening(EventMessage.Death, Die);
        EventManager.StartListening(EventMessage.ResetCamera, ResetCamera);

    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.GravityOff, GravityOff);
        EventManager.StopListening(EventMessage.GravityOn, GravityOn);
        EventManager.StopListening(EventMessage.Death, Die);
        EventManager.StopListening(EventMessage.ResetCamera, ResetCamera);

    }

    private void Explode(GameObject part)
    {
        var ps = part.GetComponent<ParticleSystem>();
        if (ps != null)
            ps.Play();

        var rb2d = part.GetComponent<Rigidbody2D>();
        rb2d.simulated = true;
        rb2d.AddForce(new Vector2(Random.Range(-chunkForce, chunkForce), Random.Range(-chunkForce, chunkForce)) * thrust * Time.deltaTime * speedMultiplier);
        rb2d.gravityScale = this.rb2D.gravityScale;
        rb2d.drag = this.rb2D.drag;
        rb2d.transform.parent = GameManager.ChunkManager.transform;
        part.GetComponent<Collider2D>().enabled = true;
    }

    public void Die()
    {
        //remove player collision
        this.GetComponent<Collider2D>().enabled = false;
        //explode player
        var count = transform.GetChildCount();
        for (int i=0; i<count; ++i)
        {
            Explode(transform.GetChild(0).gameObject);
        }
        //spawn chunks,
        for (int i = 0; i<chunkCount; ++i)
        {           
            var chunk = Instantiate(Chunks, this.transform.position+new Vector3(Random.Range(-bloodSplash, bloodSplash),
                                                                            Random.Range(-bloodSplash, bloodSplash), 0.0f), this.transform.rotation);
            Explode(chunk);
        }
        
        EventManager.TriggerEvent(EventMessage.GravityOn);
        isDead = true;
    }

    void ResetCamera()
    {
        if(isDead)
        {

        }
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
        this.transform.localScale = new Vector3((IsLeft ? -this.defaultScale.x : this.defaultScale.x), this.defaultScale.y, this.defaultScale.z);
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

    public GameData.PlayerNumber PlayerNum
    {
        get
        {
            return selectedPlayer;
        }
    }
}
