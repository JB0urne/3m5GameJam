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
		if (health > 10) {
			health--;
		} else {
			//gameover
		}
		healthbar.value = health;
	}

	public void ADDhealth (int amount) {
		health += amount;
	}

	public void SUBhealth (int amount) {
		health -= amount;
	}
}
