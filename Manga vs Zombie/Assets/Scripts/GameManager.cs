using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int Score;
    public int time;
    public int Gold;
    public int HighScore;
    public Text scoreText;
    public Text timeText;
    public Text goldText;
    public Text HighScoreText;
    public InventorySceneList scenelist;
    public Texture2D Backgournd;
    public Texture2D Foregournd;
    public Texture2D Tower;
    public Texture2D Towerenemy;
    public int i=0;
    float t=0;
    private bool pause = false;
    public GameObject PauseUI;
    public GameObject ResultUI;
    private bool endgame = false;
    void Start () {
        PauseUI.SetActive(false);
        ResultUI.SetActive(false);
        time = scenelist.scenelist[i].TimePlay;
        Score = 0;
        Gold = 0;
        Backgournd = scenelist.scenelist[i].Backgournd;
        Foregournd = scenelist.scenelist[i].Foregournd;
        Tower = scenelist.scenelist[i].Tower;
        Towerenemy = scenelist.scenelist[i].Towerenemy;
        
    
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
        
       
      

        if (time==0 && endgame==false && i< scenelist.scenelist.Count-1)
        {
           
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
        
        HighScore = PlayerPrefs.GetInt("HighScore");
        if (HighScore < Score)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
        Debug.Log(HighScore);
    }
    public void Pause()
    {
        PauseUI.SetActive(true);
        pause = true;
    }
    public void Resume()
    {
        pause = false;
        PauseUI.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
