using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

	public int maxChunkHealth = 50;
	public int chunkHealth;

	// Use this for initialization
	void Start () {
		chunkHealth = maxChunkHealth;
	}
	
	// Update is called once per frame
	void Update () {
		float colorChannel = (chunkHealth/(float)maxChunkHealth);
		this.GetComponentInParent<SpriteRenderer>().color = new Color(colorChannel, colorChannel, colorChannel);
	}

	public void incHealth(int amount) {
		chunkHealth += amount;
	}

	public void decHealth(int amount) {
		chunkHealth -= amount;
	}
}
