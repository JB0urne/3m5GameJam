using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {
    
	private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float speed = 10.0F;				//Floating point variable to store the player's movement speed.
    public float moveVertical;
    
    public int groundContacts;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Vector3 vCollObj = new Vector2(collision.transform.position.x, collision.transform.position.y - collision.transform.localScale.y / 2.0f);

        Vector2 v = this.transform.position - collision.transform.position;

        if (v.y > 0)
            ++this.groundContacts;
        else
            this.groundContacts = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (this.groundContacts > 0)
            --this.groundContacts;
    }

    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        // wenn boden berührt

        if (this.groundContacts > 0)
            moveVertical = Input.GetAxis("Vertical");
        else
            moveVertical = 0;

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical * 10);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
}
