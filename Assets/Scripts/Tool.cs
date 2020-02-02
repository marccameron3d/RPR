using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public GameData.ToolType myType;
    Collider2D col;
    [SerializeField]
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
            if (collision.gameObject.GetComponent<Player>().CurrentTool == null)
            {
                Debug.Log("Gets here 1");
                this.gameObject.transform.SetParent(collision.gameObject.transform);
                Debug.Log("Gets here 2");
                owner = collision.gameObject.GetComponent<Player>();
                owner.CurrentTool = this;
                Debug.Log("Gets here 3");
                PickedUp();
                col.enabled = false;
            }
        }
    }

    public void DropTool()
    {
        if (this.gameObject.transform.parent != null)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.transform.SetParent(null);
            owner.CurrentTool = null;
            owner = null;

            Invoke("ColliderTimer", 0.5f);
        }
    }

    void PickedUp()
    {
        this.gameObject.GetComponent<Rigidbody2D>().simulated = false;

        col.enabled = !col.enabled;

        this.gameObject.transform.parent = owner.gameObject.transform.Find("ToolAnchor");
        this.gameObject.transform.localPosition = Vector3.zero;
        this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void ColliderTimer()
    {
        col.enabled = true;
        this.gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

}
