using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int StartHealth = 500;

    public int health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0) onDeath();
        }
    }

    private int _health = 500;
    public GameObject Player;
    public int ScorePerPickUp = 50;
	public int ScorePerBlock = 10;
	public int scorePerSecond = 5;
	public Slider healthbar;
	public Text scoreText;
	public Text timeText;
	private int score;
	private float timer = 0.0f;
	private int seconds;

    public static string finalScoreText;
    public static string finalTimeText;

    public void onDeath()
    {
        Debug.Log("dead");
        finalScoreText = scoreText.text;
        finalTimeText = timeText.text;
        SceneManager.LoadScene("GameOverScene");
    }

	// Use this for initialization
	void Start ()
	{
	    health = StartHealth;
		healthbar.value = health;	
		healthbar.maxValue = health;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 0) {
			health--;
		}
	    if (health > 0 && Player.transform.position.y < 0)
	    {
	        health = 0;
	    }
		healthbar.value = health;

		//update time
		timer += Time.deltaTime;
		seconds = (int) timer;
	    timeText.text = "TIME: " + seconds;

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

	public void SetScoreText ()
	{
	    scoreText.text = "SCORE: " + (seconds * scorePerSecond + score).ToString();
	}
}
