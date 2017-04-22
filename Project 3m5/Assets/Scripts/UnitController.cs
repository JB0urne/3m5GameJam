using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    private readonly float sqrt2 = (float)Math.Sqrt(2);
	private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private CircleCollider2D collider;
    public float speed = 10.0F;				//Floating point variable to store the player's movement speed.
    public float jumpStrength = 40;
    public float wallJumpMultiplier = 0.8F;

    public float moveVertical;
    
    public int groundContacts;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

    }
	
	// Update is called once per frame
	void Update () {
    }
    

    void FixedUpdate()
    {
        if (Input.GetKeyDown("space"))
//        if (Input.GetKey("space"))
        {
            Vector3 v = getVectorOfTheNearestCube();
            
            if (v.x != 0 && v.y != 0)
            {
                if (v.y < 0 && Math.Abs(v.y) > Math.Abs(v.x) && v.y < v.x)
                    rb2d.AddForce(new Vector2(0, jumpStrength) * speed);
                else if (v.x < 0 && Math.Abs(v.y) < Math.Abs(v.x) && v.y > v.x)
                    rb2d.AddForce(new Vector2(jumpStrength / sqrt2 * wallJumpMultiplier, jumpStrength / sqrt2 * wallJumpMultiplier) * speed);
                else if (v.x > 0 && Math.Abs(v.y) < Math.Abs(v.x) && v.y < v.x)
                    rb2d.AddForce(new Vector2(-jumpStrength / sqrt2 * wallJumpMultiplier, jumpStrength / sqrt2 * wallJumpMultiplier) * speed);
                else if (v.y > 0 && Math.Abs(v.y) > Math.Abs(v.x) && v.y > v.x)
                    rb2d.AddForce(new Vector2(0, -jumpStrength) * speed);
            }
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            if (!collisionWithWall())
                moveHorizontal /= 4;
            Vector2 movement = new Vector2(moveHorizontal, 0);
            rb2d.AddForce(movement * speed);
        }
    }

    private bool collisionWithWall()
    {
        Collider2D[] result = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        if (collider.OverlapCollider(filter, result) > 0)
            return true;
        else
            return false;
    }

    private Vector3 getVectorOfTheNearestCube()
    {
        Collider2D[] result = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        int count = collider.OverlapCollider(filter, result);
        Vector3 nearest = new Vector3(0, 0, 0);

        if (count > 0)
        {
            foreach(Collider2D collider in result)
            {
                if (collider != null)
                {
                    if (nearest.x == 0 && nearest.y == 0)
                        nearest = collider.transform.position;
                    else if (Vector3.Distance(this.transform.position, nearest) < Vector3.Distance(this.transform.position, collider.transform.position))
                        nearest = collider.transform.position;
                }
            }
            return nearest - this.transform.position;
        }
        else
        {
            return new Vector3();
        }
    }
}
