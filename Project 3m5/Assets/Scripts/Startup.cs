﻿using System.Collections;
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
        var player = GameObject.Find("Player");
	    var controller = Instantiate(GameManagerPrefab);
	    var ui = Instantiate(UiCanvasPrefab);
        player.GetComponent<PickUpController>().gameController = controller;
        controller.healthbar = ui.GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}