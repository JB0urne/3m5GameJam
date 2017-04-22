using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

	public GameController gameController;

	void OnTriggerEnter2D(Collider2D other) {
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);

			damageNearWallElements(other.gameObject);

			//Add one to the current value of our count variable.
			gameController.ADDhealth(50);

			//add points?
		}
	}

	void damageNearWallElements(GameObject pickUpObject) {
		GameObject[] wallElems = GameObject.FindGameObjectsWithTag("Wall");
		foreach (var ele in wallElems) {
			WallController elemContr = ele.GetComponent<WallController>();
			float distance = Vector3.Distance(pickUpObject.transform.position, ele.transform.position);
			if (distance < 5.0f) {
				elemContr.decHealth(20);
			} else if (distance < 10.0f) {
				elemContr.decHealth(10);
			}
		}
	}
}
