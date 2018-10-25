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
	public GameObject finalPopup;
	// public GameObject[] instancePlayer;
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

	void Start()
	{
		// requestJoinPopup = GameObject.FindWithTag("requestjoin");
		// finalPopup = GameObject.FindWithTag("finalpopup");
		// instancePlayer = GameObject.FindGameObjectsWithTag("Player");
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

	public void GameOver()
	{
		finalPopup.SetActive(true);
		Time.timeScale = 0;
		
	}

	public void DisplayRequestPopup()
	{
		requestJoinPopup.SetActive(true);
	}

	public void PlayGame()
	{
		// SceneManager.LoadScene("GamePlayUI");
		playScene.SetActive(true);
	}

	public void ReturnMainMenu()
	{
		requestJoinPopup.SetActive(false);
		finalPopup.SetActive(false);
		SceneManager.LoadScene("GamePlayUI");
	}

	public void RestartGame(int level)
	{

	}

	public void ExitGame()
	{
		SaveData();
	}
}
