using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField]
    GameData.ToolType myType;
    Collider2D col;
    Player owner;

    void Start()
    {
        col = this.gameObject.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Touched tool");
            if (collision.gameObject.GetComponent<Player>().CurrentTool == GameData.ToolType.NONE)
            { 
                this.gameObject.transform.SetParent(collision.gameObject.transform);
                owner = collision.gameObject.GetComponent<Player>();
                owner.CurrentTool = myType;
                PickedUp();
                col.enabled = false;
            }
        }
    }

    public void DropTool()
    {
        if (this.gameObject.transform.parent != null)
        {
            if(!GameData.gravityIsWorking)
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity =
                    this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity;
            }
            this.gameObject.transform.SetParent(null);
            owner.CurrentTool = GameData.ToolType.NONE;
            owner = null;
            col.enabled = true;
        }
    }

    void PickedUp()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = !this.gameObject.GetComponent<SpriteRenderer>().enabled;
        col.enabled = !col.enabled;
        //Add some code that will set the local position to the players tool anchor
        //Add some code that will rotate to the correct player local tool rotation anchor
        //Add some code that will set the players tool type;
    }


}
