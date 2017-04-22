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

        this.followPlayer();
	}

    private void followPlayer()
    {
        Vector3 v = new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y, -10);
        transform.position = v;
    }
}
