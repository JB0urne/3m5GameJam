using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int health = 500;
	public int ScorePerPickUp = 50;
	public int ScorePerBlock = 10;
	public int scorePerSecond = 5;
	public Slider healthbar;
	public Text scoreText;
	public Text timeText;
	private int score;
	private float timer = 0.0f;
	private int seconds;


	// Use this for initialization
	void Start () {
		healthbar.value = health;	
		healthbar.maxValue = health;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 0) {
			health--;
		} else {
			//gameover
		}
		healthbar.value = health;

		//update time
		timer += Time.deltaTime;
		seconds = (int) timer;
		timeText.text = seconds.ToString();

		SetScoreText ();
	}

	public void ADDhealth (int amount) {
		health += amount;
	}

	public void SUBhealth (int amount) {
		health -= amount;
	}

	public void ADDscorePickUp () {
		score += ScorePerPickUp;
	}

	public void ADDscoreBlocks () {
		score += ScorePerBlock;
	}

	public void SUBscore (int amount) {
		score -= amount;
	}

	public void SetScoreText () {
		scoreText.text = (seconds*scorePerSecond + score).ToString();
	}
}
