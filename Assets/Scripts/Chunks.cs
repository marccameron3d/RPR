using UnityEngine;

public class Chunks : MonoBehaviour
{
    private Rigidbody2D rb2D;
    void Start()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        EventManager.StartListening(EventMessage.GravityOff, GravityOff);
        EventManager.StartListening(EventMessage.GravityOn, GravityOn);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventMessage.GravityOff, GravityOff);
        EventManager.StopListening(EventMessage.GravityOn, GravityOn);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventMessage.GravityOff, GravityOff);
        EventManager.StopListening(EventMessage.GravityOn, GravityOn);
    }

    void GravityOff()
    {
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
        if(coll.gameObject.tag.Contains("Player"))
        { 
            Destroy(this.gameObject);
            Debug.Log("BLOODED");

        }
        if(coll.gameObject.name.Contains("Chunk"))
        {
            var chunkPhysicsScript = GameManager.ChunkManager.GetComponent<ChunkPhysics>();
            chunkPhysicsScript.Chunks.Add(this.gameObject);
        }
    }
}
