using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{

    public GameController GameManagerPrefab;
    public GameObject UiCanvasPrefab;

	// Use this for initialization
	void Start ()
	{
	    var controller = Instantiate(GameManagerPrefab);
	    var ui = Instantiate(UiCanvasPrefab);
		GameObject[] pickUpElems = GameObject.FindGameObjectsWithTag("PickUp");
		foreach (var pickup in pickUpElems) {
			pickup.GetComponent<PickUpController>().gameController = controller;
		}
        controller.healthbar = ui.GetComponentInChildren<Slider>();
	    controller.scoreText = ui.GetComponentsInChildren<Text>()[0];
        controller.timeText = ui.GetComponentsInChildren<Text>()[1];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
