using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int money;
	public int score;
	public int heighScore = 10;
	public GameObject playScene;
	public GameObject requestJoinPopup;
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

	public void DisplayRequestPopup()
	{
		requestJoinPopup.SetActive(true);
	}

	public void PlayGame()
	{
		playScene.SetActive(true);
	}

	public void ExitGame()
	{
		SaveData();
	}
}
