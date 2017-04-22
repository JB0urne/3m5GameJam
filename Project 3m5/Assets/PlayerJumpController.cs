using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour {


    public int groundContacts = 0;

    // Use this for initialization
    void Start () {
        Debug.Log("start");
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    public bool hasGroundContact()
    {
        if (groundContacts > 0)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("enter trigger " + coll.name);
        if(!coll.name.Equals("Player"))
            ++this.groundContacts;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");

        --this.groundContacts;
    }
}
