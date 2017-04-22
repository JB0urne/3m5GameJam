using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

	public int myint;
	private int myint2;
	public GameObject myobject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("w"))
        {
            playerMoveUp();
        }
        if (Input.GetKey("a"))
        {
            playerMoveLeft();
        }
        if (Input.GetKey("d"))
        {
            playerMoveRight();
        }
    }

    private void playerMoveUp()
    {
        transform.Translate(Vector3.up * 4 * Time.deltaTime, Space.World);
    }

    private void playerMoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime, Space.World);
    }

    private void playerMoveRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime, Space.World);
    }
}
