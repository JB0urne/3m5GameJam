using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

	public float maxChunkHealth = 100.0f;
	public float chunkHealth;

    public float TouchHealthLossPerSecond = 1f;
    public float MaxPickupHealthLoss = 40f;

	// Use this for initialization
	void Start () {
		chunkHealth = maxChunkHealth;
	}
	
	// Update is called once per frame
	void Update () {
		float colorChannel = (chunkHealth/maxChunkHealth);
		this.GetComponentInParent<SpriteRenderer>().color = new Color(colorChannel, colorChannel, colorChannel);
	}

	public void incHealth(float amount) {
		chunkHealth += amount;
	}

	public void decHealth(float amount) {
		chunkHealth -= amount;
	}

    public void DealDamageWithTouch(float touchTimeInSeconds)
    {
        chunkHealth -= TouchHealthLossPerSecond * touchTimeInSeconds;
    }

    public void DealDamageWithPickup(float damageMultiplier)
    {
        chunkHealth -= MaxPickupHealthLoss * damageMultiplier;
    }
}
