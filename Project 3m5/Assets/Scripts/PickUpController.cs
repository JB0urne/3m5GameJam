using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

	public GameController gameController;
	public bool playerIsHere;
	public string playerInput;
	public string consumeCode;
	public int anzahlKeys;

	void Start () {
		playerIsHere = false;
		playerInput = "";
		anzahlKeys = Random.Range(2, 5);
		int randomKey; 
		consumeCode = "";
		for (int i=0; i<anzahlKeys; i++) {
			randomKey = Random.Range(0, 4);
			if (randomKey == 0) {
				consumeCode += "w";
			} else if (randomKey == 1) {
				consumeCode += "a";
			} else if (randomKey == 2) {
				consumeCode += "s";
			} else if (randomKey == 3) {
				consumeCode += "d";
			}
		}
	}

	void Update () {
		if (playerIsHere) {
			if (Input.GetKeyDown ("w")) playerInput += "w";
			if (Input.GetKeyDown ("a")) playerInput += "a";
			if (Input.GetKeyDown ("s")) playerInput += "s";
			if (Input.GetKeyDown ("d")) playerInput += "d";
			if (playerInput.Contains (consumeCode))
				consumePickup ();
		}
	}

	void consumePickup() {
		this.gameObject.SetActive(false);
		damageNearWallElements(this.gameObject);
		gameController.ADDhealth(50);
		gameController.GetComponent<GameController> ().ADDscorePickUp ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) 
		{
			playerIsHere = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			playerIsHere = false;
			playerInput = "";
		}
	}

	void damageNearWallElements(GameObject pickUpObject) {
		GameObject[] wallElems = GameObject.FindGameObjectsWithTag("Wall");
		foreach (var ele in wallElems) {
			WallController elemContr = ele.GetComponent<WallController>();
			float distance = Vector3.Distance(pickUpObject.transform.position, ele.transform.position);
			if (distance <= 5.0f) {
				float damageMultiplier = 1.0f - (distance * 0.2f);
				elemContr.DealDamageWithPickup (damageMultiplier);
			}
		}
	}
}
