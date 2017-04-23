using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject playerPosition;
	private Vector3 movementDirection;
	private Vector3 nextCameraPosition;
	private float moveSpeed;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        this.followPlayer();
		nextCameraPosition = playerPosition.transform.position + new Vector3(0,0,-10);
	}

    private void followPlayer()
    {
        //old camera movement pattern
		//Vector3 v = new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y, -10);
        //transform.position = v;

		//new camera movement pattern
		float x = Mathf.Abs(this.transform.position.x - nextCameraPosition.x);
		float y = Mathf.Abs(this.transform.position.y - nextCameraPosition.y);
		moveSpeed = Mathf.Max (x, y);
			
			this.transform.position = Vector3.Lerp(this.transform.position, nextCameraPosition, Time.deltaTime * moveSpeed);
		//transform.position = nextCameraPosition + new Vector3(0,0,-10);
    }
}
