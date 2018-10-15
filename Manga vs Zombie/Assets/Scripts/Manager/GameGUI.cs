using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour 
{
	public Text moneyText;
	public Text scoreText;
	public Text heighScoreText;
	public Text timeText;
	public static GameGUI Instance = null;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		moneyText.text = GameManager.Instance.money.ToString();
		scoreText.text = GameManager.Instance.score.ToString();
		// heighScoreText.text = GameManager.Instance.heighScore.ToString();
	}
}
