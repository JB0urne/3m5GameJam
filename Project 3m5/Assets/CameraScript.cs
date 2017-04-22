using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject playerPosition;

	// Use this for initialization
	void Start ()
    {
        this.followPlayer();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("space"))
        {
            print("space key was pressed");
            playerMoveUp();
        }

        this.followPlayer();
	}

    private void playerMoveUp()
    {        
        playerPosition.transform.Translate(Vector3.left * Time.deltaTime);
        playerPosition.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }

    private void followPlayer()
    {
        Vector3 v = new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y, -10);
        transform.position = v;
    }
}
