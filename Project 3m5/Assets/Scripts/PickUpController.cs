using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

	public GameController gameController;
	public bool playerIsHere;
	public string playerInput;

	void Start () {
		playerIsHere = false;
		playerInput = "";
	}

	void Update () {
		if (playerIsHere) {
			if (Input.GetKeyDown ("w")) playerInput += "w";
			if (Input.GetKeyDown ("a")) playerInput += "a";
			if (Input.GetKeyDown ("s")) playerInput += "s";
			if (Input.GetKeyDown ("d")) playerInput += "d";
			if (playerInput.Contains ("asd"))
				consumePickup ();
		}
	}

	void consumePickup() {
		this.gameObject.SetActive(false);
		damageNearWallElements(this.gameObject);
		gameController.ADDhealth(50);
		//TODO: add points
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
