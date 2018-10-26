using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject playScene;
    public GameObject loading;
	public GameObject requestJoinPopup;
	public GameObject finalPopup;
	public int Score;
    public int time;
    public int Gold;
    public int HighScore;
    public Text scoreText;
    public Text timeText;
    public Text goldText;
    public Text HighScoreText;
    public Text ScoreWin;
    public Text GoldWin;

    public InventorySceneList scenelist;
    public Texture2D Backgournd;
    public Texture2D Foregournd;
    public Texture2D Tower;
    public Texture2D Towerenemy;
    public int i=0;
    float t=0;
    private bool pause = true;
    public GameObject PauseUI;
    public GameObject ResultUI;
    public GameObject NextLvUI;
    public GameObject MenuUI;
    public GameObject YNUI;
    
    public GameObject Highscore;
    public GameObject YNQuitUI;
    public GameObject start;
    public GameObject shop;
    public GameObject setting;
    public GameObject quit;
    public GameObject quitResult;
    public GameObject backResult;
    public GameObject Pausebtn;
    private bool endgame = false;
    bool isPlaying = false;
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

	void Start () {

        YNUI.SetActive(false);
        //Player.SetActive(false);
        MenuUI.SetActive(true);
        PauseUI.SetActive(false);
        ResultUI.SetActive(false);
        NextLvUI.SetActive(false);
        Highscore.SetActive(false);
        YNQuitUI.SetActive(false);
        HighScore = PlayerPrefs.GetInt("HighScore");
        HighScoreText.text = HighScore.ToString();
        
    
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

	void Update () {
       
            if (t >= 1 && endgame == false)
            {
                time--;
                t = 0;
            }
            else
                t += Time.deltaTime;
        
        scoreText.text = Score.ToString();
        timeText.text = time.ToString();
        goldText.text = Gold.ToString();
        
       
      

        if (isPlaying && time==0 && endgame==false && i< scenelist.scenelist.Count-1)
        {
            NextLv();
            UpLevel();
            LoadLevel();
        }
      
        if(time==0 && i== scenelist.scenelist.Count - 1)
        {
            
            EndGame();

        }

        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
	}

    public void UpLevel()
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("Gold", Gold);
    }
    public void LoadLevel()
    {
  
        i++;
        time = scenelist.scenelist[i].TimePlay;
        Backgournd = scenelist.scenelist[i].Backgournd;
        Foregournd = scenelist.scenelist[i].Foregournd;
        Tower = scenelist.scenelist[i].Tower;
        Towerenemy = scenelist.scenelist[i].Towerenemy;
        
        Score = PlayerPrefs.GetInt("Score");
        Gold = PlayerPrefs.GetInt("Gold");
    }
    public void EndGame()
    {
        ResultUI.SetActive(true);
        endgame = true;
        pause = true;

        ScoreWin.text = Score.ToString();
        GoldWin.text = "+" + Gold.ToString();
        HighScore = PlayerPrefs.GetInt("HighScore");
        if (HighScore < Score)
        {
            PlayerPrefs.SetInt("HighScore", Score);
            Highscore.SetActive(true);
        }
        Debug.Log(HighScore);
    }
    public void Pause()
    {
        if (endgame == false && NextLvUI.activeInHierarchy==false)
        {
            PauseUI.SetActive(true);
            pause = true;
        }
    }
    public void Resume()
    {
        pause = false;
        PauseUI.SetActive(false);
    }
   
    public void NextLv()
    {
        pause = true;
        NextLvUI.SetActive(true);

    }
    public void NextLV()
    {
        pause = false;
        NextLvUI.SetActive(false);
    }
    public void PlayGame()
    {
       // Player.SetActive(true);
        MenuUI.SetActive(false);
        ResultUI.SetActive(false);
        NextLvUI.SetActive(false);
        quitResult.SetActive(true);
        backResult.SetActive(true);
        Pausebtn.SetActive(true);
        loading.SetActive(false);
        
		playScene.SetActive(true);
        isPlaying = true;
        pause = false;
        endgame = false;
        time = scenelist.scenelist[0].TimePlay;
        Score = 0;
        Gold = 0;
        Backgournd = scenelist.scenelist[0].Backgournd;
        Foregournd = scenelist.scenelist[0].Foregournd;
        Tower = scenelist.scenelist[0].Tower;
        Towerenemy = scenelist.scenelist[0].Towerenemy;
    }
    public void BackMenu()
    {
        PauseUI.SetActive(false);
        YNUI.SetActive(true);
        quitResult.SetActive(false);
        backResult.SetActive(false);
        Pausebtn.SetActive(false);
    }
    public void Yesbtn()
    {
        
        i = 0;
        YNUI.SetActive(false);
        MenuUI.SetActive(true);
        endgame = true;
        //Player.SetActive(false);
        Highscore.SetActive(false);
      
        HighScoreText.text = HighScore.ToString();
        
     
    }
    public void Nobtn()
    {
        Pausebtn.SetActive(true);
        quitResult.SetActive(true);
        backResult.SetActive(true);
        YNUI.SetActive(false);
        pause = false;
    }
    public void QuitGame()
    {
        YNQuitUI.SetActive(true);
        Pausebtn.SetActive(false);
        if (PauseUI.activeInHierarchy == true)
            { 
            PauseUI.SetActive(false);
        }
        if (ResultUI.activeInHierarchy == true)
        {
            quitResult.SetActive(false);
            backResult.SetActive(false);
        }
        if (MenuUI.activeInHierarchy == true)
        {
            start.SetActive(false);
            shop.SetActive(false);
            setting.SetActive(false);
            quit.SetActive(false);
        }
    }
    public void YesQbtn()
    {
        Application.Quit();

    }
    public void NoQbtn()
    {
        YNQuitUI.SetActive(false);
        if(isPlaying)
            Pausebtn.SetActive(true);
        if (MenuUI.activeInHierarchy == true)
        {
            start.SetActive(true);
            shop.SetActive(true);
            setting.SetActive(true);
            quit.SetActive(true);
        }
        if (PauseUI.activeInHierarchy == false)
        {
            pause = false;
        }
        if (ResultUI.activeInHierarchy == true)
        {
            quitResult.SetActive(true);
            backResult.SetActive(true);
        }


    }
}
