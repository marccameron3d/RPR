using UnityEngine;

public class Player : MonoBehaviour {
    // Start is called before the first frame update

    public float thrust = 10.0f;
    public float speedMultiplier = 100f;
    public float maxSpeed = 4f;
    public float axisDampining = 0.1f;
    private Rigidbody2D rb2D;
    public GameObject Chunks;
    public int chunkCount = 2;
    //[SerializeField]
    public Tool currentTool;
    private Vector3 defaultScale;
    private float bloodSplash = 0.3f;

    [SerializeField]
    public GameData.PlayerNumber selectedPlayer;
    public float chunkForce = 0.2f;

    private void Awake() {
        this.defaultScale = this.transform.localScale;
    }
    void Start() {
        this.rb2D = GetComponent<Rigidbody2D>();
        currentTool = null;
        setAlive(true);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            Die();
        }
    }

    public void TakeInput(float x, float y) {
        // player controls
        rb2D.AddForce(new Vector2(x, 0) * thrust * Time.deltaTime * speedMultiplier);
        rb2D.AddForce(new Vector2(0, y) * thrust * Time.deltaTime * speedMultiplier);

        // look direction
        if (x > 0 + axisDampining)
            FlipSprite();
        else if (y < 0 - axisDampining)
            FlipSprite(true);
    }

    void FixedUpdate() {
        // set the max speed of the obj
        if (rb2D.velocity.magnitude > maxSpeed) {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }

    }

    void OnEnable() {
        EventManager.StartListening(EventMessage.GravityOff, GravityOff);
        EventManager.StartListening(EventMessage.GravityOn, GravityOn);
        EventManager.StartListening(EventMessage.ResetCamera, ResetCamera);

    }

    void OnDisable() {
        EventManager.StopListening(EventMessage.GravityOff, GravityOff);
        EventManager.StopListening(EventMessage.GravityOn, GravityOn);
        EventManager.StopListening(EventMessage.ResetCamera, ResetCamera);

    }

    private void Explode(GameObject part) {
        part.gameObject.tag = "Untagged";

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

        EventManager.TriggerEvent(EventMessage.CameraShake);
    }

    public void dropTool()  {
        if(currentTool != null) {
            currentTool.DropTool();
        }
    }

    public void Die() {
        //remove player collision
        this.GetComponent<Collider2D>().enabled = false;
        //explode player
        var count = transform.childCount;
        for (int i = 0; i < count; ++i) {
            if (transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Explode(transform.GetChild(0).gameObject);
            }
        }
        //spawn chunks,
        for (int i = 0; i < chunkCount; ++i) {
            var chunk = Instantiate(Chunks, this.transform.position + new Vector3(Random.Range(-bloodSplash, bloodSplash),
                Random.Range(-bloodSplash, bloodSplash), 0.0f), this.transform.rotation);
            Explode(chunk);
        }
        setAlive(false);
        EventManager.TriggerEvent(EventMessage.Death);
        dropTool();
    }

    void setAlive(bool isAlive) {
        switch (PlayerNum) {
            case GameData.PlayerNumber.PLAYER_1:
                GameData.player1Alive = isAlive;
                break;
            case GameData.PlayerNumber.PLAYER_2:
                GameData.player2Alive = isAlive;
                break;
            case GameData.PlayerNumber.PLAYER_3:
                GameData.player3Alive = isAlive;
                break;
        }
    }

    void ResetCamera() { }

    void GravityOff() {
        this.rb2D.gravityScale = 0;
        this.rb2D.drag = 0;
    }

    void GravityOn() {
        this.rb2D.gravityScale = 1;
        this.rb2D.drag = 2;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "CloneBay") {
            other.gameObject.GetComponent<CloneBay>().respawnPlayers();
        }
    }

    void FlipSprite(bool IsLeft = false) {
        this.transform.localScale = new Vector3((IsLeft ? -this.defaultScale.x : this.defaultScale.x), this.defaultScale.y, this.defaultScale.z);
    }

    public Tool CurrentTool {
        get {
            return currentTool;
        }

        set {
            currentTool = value;
        }
    }

    public GameData.PlayerNumber PlayerNum {
        get {
            return selectedPlayer;
        }
    }
}