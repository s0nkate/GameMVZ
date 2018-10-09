using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int money;
	public int score;
	public int heighScore = 10;
	public static GameManager Instance = null;
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


	public void AddMoney(int amount)
	{
		money += amount;
	}

	public void AddScore(int amount)
	{
		score += amount;
	}

	public void LoadData()
	{

	}


	public void SaveData()
	{

	}

	public void PlayGame()
	{
		// SceneManager.LoadScene("GamePlayUI");
	}

	public void ExitGame()
	{
		SaveData();
	}
}
