using UnityEngine;

public class Player : MonoBehaviour {
    // Start is called before the first frame update

    public float thrust = 10.0f;
    public float speedMultiplier = 100f;
    public float maxSpeed = 4f;
    public float axisDampining = 0.1f;
    private Rigidbody2D rb2D;
    public GameObject Chunks;
    public int chunkCount = 5;
    [SerializeField]
    private GameData.ToolType currentTool = GameData.ToolType.NONE;

    void Start () {
        this.rb2D = GetComponent<Rigidbody2D> ();
        currentTool = GameData.ToolType.NONE;
    }

    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate () {
        // set the max speed of the obj
        if (rb2D.velocity.magnitude > maxSpeed) {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }

        if (Input.GetKeyDown (KeyCode.Z)) {
            EventManager.TriggerEvent (EventMessage.Death);
        }
        if (Input.GetKeyDown (KeyCode.X)) {
            EventManager.TriggerEvent (EventMessage.GravityOn);
        }
        if (Input.GetKeyDown (KeyCode.C)) {
            EventManager.TriggerEvent (EventMessage.GravityOff);
        }
    }

    public void takeInput (float x, float y) {
        // player controls
        rb2D.AddForce (new Vector2 (x, 0) * thrust * Time.deltaTime * speedMultiplier);
        rb2D.AddForce (new Vector2 (0, y) * thrust * Time.deltaTime * speedMultiplier);

        // look direction
        if (x > 0 + axisDampining)
            FlipSprite ();
        else if (y < 0 - axisDampining)
            FlipSprite (true);

    }

    void OnEnable () {
        EventManager.StartListening (EventMessage.GravityOff, GravityOff);
        EventManager.StartListening (EventMessage.GravityOn, GravityOn);
        EventManager.StartListening (EventMessage.Death, Die);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.GravityOff, GravityOff);
        EventManager.StopListening(EventMessage.GravityOn, GravityOn);

    }

    public void Die () {
        //spawn chunks,
        for (int i = 0; i < chunkCount; ++i) {
            var s = Instantiate (Chunks, this.transform.position, this.transform.rotation);
            var rb2d = s.GetComponent<Rigidbody2D> ();
            rb2d.AddForce (new Vector2 (Random.Range (-1, 1), Random.Range (-1, 1)) * thrust * Time.deltaTime * speedMultiplier);
            rb2d.gravityScale = this.rb2D.gravityScale;
            rb2d.drag = this.rb2D.drag;
        }
        //remove player
    }

    void GravityOff () {
        this.rb2D.gravityScale = 0;
        this.rb2D.drag = 0;
    }

    void GravityOn () {
        this.rb2D.gravityScale = 1;
        this.rb2D.drag = 2;
    }

    void OnCollisionStay2D (Collision2D coll) {

    }

    void FlipSprite (bool IsLeft = false) {
        this.transform.localScale = new Vector3 ((IsLeft ? -1 : 1), 1, 1);
    }

    public GameData.ToolType CurrentTool {
        get {
            return currentTool;
        }

        set {
            currentTool = value;
        }
    }
}