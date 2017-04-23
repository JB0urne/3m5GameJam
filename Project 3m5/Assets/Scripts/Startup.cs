using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{

    public GameController GameManagerPrefab;
    public GameObject UiCanvasPrefab;

	// Use this for initialization
	void Start ()
	{
        var player = GameObject.Find("Player");
	    var controller = Instantiate(GameManagerPrefab);
	    var ui = Instantiate(UiCanvasPrefab);
        player.GetComponent<PickUpController>().gameController = controller;
        controller.healthbar = ui.GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("r"))
        {
            //GameObject wall = getWallByName("Cube (139)");
            GameObject wall = getWallByName("Cube (35)");
            WallController controller = wall.GetComponent<WallController>();
            controller.destroy();

        }
    }

    private GameObject getWallByName(string name)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Wall");
        foreach(GameObject obj in objs)
        {
            if (obj.name.Equals(name))
                return obj;
        }
        return null;
    }
}
