using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int health = 500;
	public Slider healthbar;
	// Use this for initialization
	void Start () {
		healthbar.value = health;	
		healthbar.maxValue = health;
	}
	
	// Update is called once per frame
	void Update () {
		if (healthbar.value<=0) {
			//gameover
		} {
			healthbar.value -= 1;
		}; 
	}

	public void ADDhealth (int amount) {
		health += amount;
	}

	public void SUBhealth (int amount) {
		health -= amount;
	}
}
