

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ECSComponent;

public class GameManager : Photon.PunBehaviour
{

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
    public GameObject Backgournd;
    public GameObject Foregournd;
    public GameObject Tower;
    public GameObject Towerenemy;
    public GameObject Towerenemy1;
    public int i = 0;
    float t = 0;
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
    public bool isPlaying = false;
    public bool Gameover = false;
    public bool item1;
    public bool item2;
    public List<ShopItems> playerShopList;
    public List<ShopItems> itemShopList;
    public Text Textitem1;
    public Text Textitem2;
    public Image Imageitem1;
    public Image Imageitem2;
    public EffectType effectType = EffectType.None;
    public int masterIndex;
    public int clientIndex;
    public AudioClip buttonClick;
    public AudioClip soundBackground;
    public AudioSource sound;




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
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sound = GetComponent<AudioSource>();
        LoadData();
        YNUI.SetActive(false);
        MenuUI.SetActive(true);
        PauseUI.SetActive(false);
        ResultUI.SetActive(false);
        NextLvUI.SetActive(false);
        Highscore.SetActive(false);
        YNQuitUI.SetActive(false);


        //Backgournd = gameObject.GetComponent<SpriteRenderer>().sprite;

    }
    public void ExitRoom()

    {
        PhotonNetwork.LeaveRoom();
    }

    public void LoadData()
    {
        Gold = PlayerPrefs.GetInt("Gold");
        HighScore = PlayerPrefs.GetInt("HighScore");
        HighScoreText.text = HighScore.ToString();
    }

    public ShopItems GetSelectedPlayer()
    {
        ShopItems selectedPlayer = null;
        foreach (var player in playerShopList)
        {
            if (player.isSelected)
            {
                selectedPlayer = player;
                break;
            }

        }
        return selectedPlayer;
    }

    public List<ShopItems> GetSelectedItem()
    {
        List<ShopItems> selectedItem = new List<ShopItems>();
        foreach (var item in itemShopList)
        {
            if (item.isSelected)
                selectedItem.Add(item);
        }
        return selectedItem;
    }


    public void SaveData()
    {
        playerShopList = null;
        itemShopList = null;

    }

    public void GameOver()
    {
        if (isPlaying)
            finalPopup.SetActive(true);
        isPlaying = false;
        pause = true;
        Gameover = true;
    }

    public void DisplayRequestPopup()
    {
        requestJoinPopup.SetActive(true);
    }

    public void DisableRequestPopup()
    {
        requestJoinPopup.SetActive(false);
    }

    public void ExitGame()
    {
        SaveData();
    }

    void Update()
    {

        if (t >= 1 && isPlaying == true)
        {
            time--;
            t = 0;
        }
        else
            t += Time.deltaTime;

        scoreText.text = Score.ToString();
        timeText.text = time.ToString();
        goldText.text = Gold.ToString();

        if (isPlaying && time == 0 && i < scenelist.scenelist.Count - 1)
        {
            NextLv();
            UpLevel();
            LoadLevel();
        }

        if (time == 0 && i == scenelist.scenelist.Count - 1)
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


        ZombiePool.onNextLevel.Invoke();
        House.onNextLevel.Invoke();
        Score = PlayerPrefs.GetInt("Score");
        Gold = PlayerPrefs.GetInt("Gold");
    }
    public void EndGame()
    {
        ResultUI.SetActive(true);
        isPlaying = false;
        pause = true;
        SoundManager.Instance.volumeSilder.value = 0;
        ScoreWin.text = Score.ToString();
        GoldWin.text = "+" + Gold.ToString();
        PlayerPrefs.SetInt("Gold", Gold);
        HighScore = PlayerPrefs.GetInt("HighScore");
        if (HighScore < Score)
        {
            PlayerPrefs.SetInt("HighScore", Score);
            Highscore.SetActive(true);
        }

    }
    public void Pause()
    {
        if (isPlaying == true && NextLvUI.activeInHierarchy == false)
        {
            SoundBtn();
            PauseUI.SetActive(true);
            pause = true;

            SoundManager.Instance.volumeSilder.value = 0;
        }
    }
    public void Resume()
    {

        SoundManager.Instance.volumeSilder.value = PlayerPrefs.GetFloat("Sound");
        pause = false;
        PauseUI.SetActive(false);


    }

    public void NextLv()
    {
        pause = true;
        NextLvUI.SetActive(true);

    }
    public void NextLvRPC()
    {
        photonView.RPC("NextLV", PhotonTargets.All);
    }

    [PunRPC]

    public void NextLV()
    {
        SoundBtn();
        i++;
        pause = false;
        isPlaying = true;
        NextLvUI.SetActive(false);
        time = scenelist.scenelist[i].TimePlay;
        Backgournd.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[i].Backgournd;
        Foregournd.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[i].Foregournd;
        Tower.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[i].Tower;
        Towerenemy.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[i].Towerenemy;
        Towerenemy1.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[i].Towerenemy;
    }

    public void SoundBtn()
    {
        sound.PlayOneShot(buttonClick, SoundManager.Instance.volume * 5);
    }
    public void PlayGame()
    {
        PlayerPrefs.SetFloat("Sound", SoundManager.Instance.volumeSilder.value);
        i = 0;
        MenuUI.SetActive(false);
        ResultUI.SetActive(false);
        NextLvUI.SetActive(false);
        quitResult.SetActive(true);
        backResult.SetActive(true);
        Pausebtn.SetActive(true);
        loading.SetActive(false);
        Gameover = false;
        finalPopup.SetActive(false);
        playScene.SetActive(true);
        isPlaying = true;
        pause = false;
        item1 = true;
        item2 = true;
        House.onNextLevel.Invoke();
        time = scenelist.scenelist[0].TimePlay;
        Score = 0;
        Backgournd.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[0].Backgournd;
        Foregournd.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[0].Foregournd;
        Tower.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[0].Tower;
        Towerenemy.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[0].Towerenemy;
        Towerenemy1.GetComponent<SpriteRenderer>().sprite = scenelist.scenelist[0].Towerenemy;
    }
    public void BackMenu()
    {

        SoundBtn();
        PauseUI.SetActive(false);
        YNUI.SetActive(true);
        quitResult.SetActive(false);
        backResult.SetActive(false);
        Pausebtn.SetActive(false);
        finalPopup.SetActive(false);
    }
    public void Yesbtn()
    {
        SoundManager.Instance.volumeSilder.value = PlayerPrefs.GetFloat("Sound");
        SoundBtn();
        i = 0;
        YNUI.SetActive(false);
        MenuUI.SetActive(true);

        Highscore.SetActive(false);
        playScene.SetActive(false);
        HighScoreText.text = HighScore.ToString();
        ExitRoom();
        isPlaying = false;
        ResultUI.SetActive(false);

    }
    public void Nobtn()
    {
        SoundManager.Instance.volumeSilder.value = PlayerPrefs.GetFloat("Sound");
        SoundBtn();
        Pausebtn.SetActive(true);
        quitResult.SetActive(true);
        backResult.SetActive(true);
        YNUI.SetActive(false);
        pause = false;
        if (Gameover)
        {
            finalPopup.SetActive(true);
        }
    }
    public void QuitGame()
    {
        SoundBtn();
        YNQuitUI.SetActive(true);
        Pausebtn.SetActive(false);
        if (PauseUI.activeInHierarchy == true)
        {
            PauseUI.SetActive(false);
        }
        if (ResultUI.activeInHierarchy == true)
        {
            //quitResult.SetActive(false);
            //backResult.SetActive(false);
            ResultUI.SetActive(false);
        }
        if (MenuUI.activeInHierarchy == true)
        {
            start.SetActive(false);
            shop.SetActive(false);
            setting.SetActive(false);
            quit.SetActive(false);
        }
        if (finalPopup.activeInHierarchy == true)
        {
            finalPopup.SetActive(false);
        }
    }
    public void YesQbtn()
    {
        SoundBtn();
        Application.Quit();

    }
    public void NoQbtn()
    {
        SoundBtn();
        YNQuitUI.SetActive(false);
        if (isPlaying)
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
        if (Gameover)
        {
            finalPopup.SetActive(true);
        }
    }
}
