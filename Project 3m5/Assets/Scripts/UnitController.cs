﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class UnitController : MonoBehaviour
{

    private const float TOLERANCE = 0.01f;
    private readonly float sqrt2 = (float)Math.Sqrt(2);

    private Rigidbody2D body;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private CircleCollider2D collider;
    private bool SpaceIsDown = false;
    public float LinearMovementSpeed = 10.0f;
    public float AngularMovementSpeed = 10.0f;
    public float AirLinearSpeedMultiplier = 0.5f;
    public float AirAngularSpeedMultiplier = 0.5f;
    public float JumpStrength = 40;
    public float WallJumpStrengthMultiplier = 0.8f;
    public float JumpTouchDistanceTolerance = 0.05f;

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    SpaceIsDown |= Input.GetKeyDown("space");
	}
    

    void FixedUpdate()
    {
        List<GameObject> touchingWalls;
        Vector3? vNullable = GetVectorOfTheNearestCubeWithoutCollision(out touchingWalls);
        // jump
        if (SpaceIsDown)
        {
            SpaceIsDown = false;
            if (vNullable.HasValue)
            {
                float diagonal = JumpStrength / sqrt2 * WallJumpStrengthMultiplier;
                Vector3 v = vNullable.Value;
                if (v.y < 0 && Math.Abs(v.y) > Math.Abs(v.x) && v.y < v.x)
                {
                    // jump up
                    killVelocityY();
                    body.AddForce(new Vector2(0, JumpStrength));
                }
                else if (v.x < 0 && Math.Abs(v.y) < Math.Abs(v.x) && v.y > v.x)
                {
                    // jump right
                    killVelocityX();
                    body.angularVelocity *= -1f;
                    body.AddForce(new Vector2(diagonal, diagonal));
                }
                else if (v.x > 0 && Math.Abs(v.y) < Math.Abs(v.x) && v.y < v.x)
                {
                    // jump left
                    killVelocityX();
                    body.angularVelocity *= -1f;
                    body.AddForce(new Vector2(-diagonal, diagonal));
                }
                else if (v.y > 0 && Math.Abs(v.y) > Math.Abs(v.x) && v.y > v.x)
                {
                    // jump down
                    killVelocityY();
                    body.AddForce(new Vector2(0, -JumpStrength));
                }
            }
        }
        // move
        float horizontalInput = Input.GetAxis("Horizontal");
        float linear = horizontalInput * LinearMovementSpeed;
        float angular = horizontalInput * AngularMovementSpeed;
        if (!vNullable.HasValue)
        {
            linear *= AirLinearSpeedMultiplier;
            angular *= AirAngularSpeedMultiplier;
        }
        Vector2 movement = new Vector2(linear, 0);
        body.AddForce(movement);
        body.AddTorque(-angular);
        // damage walls
        foreach(GameObject wall in touchingWalls)
        {
            wall.GetComponent<WallController>().DealDamageWithTouch(Time.fixedDeltaTime);
        }
    }

    private void killVelocityX()
    {
        body.velocity.Set(0f, body.velocity.y);
    }

    private void killVelocityY()
    {
        body.velocity.Set(body.velocity.x, 0f);
    }

    private bool CollisionWithWall()
    {
        Collider2D[] result = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        if (collider.OverlapCollider(filter, result) > 0)
            return true;
        else
            return false;
    }

    private Vector3? GetVectorOfTheNearestCubeWithoutCollision(out List<GameObject> touchingWalls)
    {
        touchingWalls = new List<GameObject>();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        Vector3? answer = null;
        float playerRadius = GetComponentInParent<CircleCollider2D>().radius;
        float nearestDistance = playerRadius * 100;
        bool foundVertical = false;

        foreach (GameObject wall in walls)
        {
            Vector2 wallSize = wall.GetComponent<BoxCollider2D>().size / 2f;
            float dx = Math.Abs(wall.transform.position.x - transform.position.x);
            float dy = Math.Abs(wall.transform.position.y - transform.position.y);
            bool alignsHorizontally = dy < wallSize.y;
            bool alignsVertically = dx < wallSize.x;
            if (alignsHorizontally || alignsVertically)
            {
                float d = (alignsHorizontally ? dx - wallSize.x : dy - wallSize.y) - playerRadius;
                if (d <= JumpTouchDistanceTolerance && d < nearestDistance)
                {
                    touchingWalls.Add(wall);
                }
                if (d <= JumpTouchDistanceTolerance && (!foundVertical || alignsVertically || d < nearestDistance))
                {
                    foundVertical |= alignsVertically;
                    nearestDistance = d;
                    answer = wall.transform.position - transform.position;
                }
            }
        }
        return answer;
    }

    private Vector3 GetVectorOfTheNearestCube()
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
