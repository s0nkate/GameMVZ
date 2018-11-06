using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ECSComponent;

public class Player1Controller : Photon.MonoBehaviour
{
    public bool faceright = true;
    public static float attackdelay;
    public bool attacking = false;
    public bool attacking1 = false;
    public Animator anim;
    public GameObject trigger;
    float h = 0;
    public bool skill1 = false;
    public float skill1delay = 1;
    public bool skill2 = false;
    public float skill2delay = 1;
    public GameObject trigger1;
    public GameObject trigger2;
    public Image imageColldown1;
    public bool isCooldown1 = false;
    public Image imageColldown2;
    public bool isCooldown2 = false;
    public static float dl;
    public static float Editcool1;
    public static float Editcool2;
    public static float Delay;
    public int i=0;
    public Text text1;
    public Text text2;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Text Textitem1;
    public Text Textitem2;
    public Image Imageitem1;
    public Image Imageitem2;
    public SkillGUI skillGUI;
    public AudioClip SoundPunch;
    public AudioClip SoundKick;
    public AudioClip SoundSkill1;
    public AudioClip SoundSkill2;
    public AudioSource audiosrc;
    //public SoundManager soundmng;
    public  List<ShopItems> listItem;
  
    public InventoryPlayerList playerlist;
  
    protected AnimatorOverrideController animatorOverrideController;
    protected AnimationClipOverrides clipOverrides;

    public float dmg;
    public float dmg1;
    public float dmg2;
    public PlayerBehaviour playerBehaviour;

    void Awake()
    {
        
        LoadData();
		GameObject playerSpawn = GameObject.FindWithTag("PlayerSpawn");
        transform.parent = playerSpawn.transform;
        trigger.SetActive(false);
        trigger1.SetActive(false);
        trigger2.SetActive(false);
      
    }
    void Start()
    {
        
        audiosrc = GetComponent<AudioSource>();
        listItem = GameManager.Instance.GetSelectedItem();
        
        if(photonView.isMine)
        {
            playerBehaviour = GameObject.FindWithTag("SkillGUI").GetComponent<PlayerBehaviour>();
            skillGUI = GameObject.FindWithTag("SkillGUI").GetComponent<SkillGUI>();
            text1= skillGUI.text1;
            text2= skillGUI.text2;
            GetComponent<Player>().playerBehaviour = playerBehaviour;
            imageColldown1 = skillGUI.imageColldown1;
            imageColldown2 = skillGUI.imageColldown2;
            image1 = skillGUI.image1;
            image2 = skillGUI.image2;
            image3 = skillGUI.image3;
            image4 = skillGUI.image4;
            Textitem1 = skillGUI.Textitem1;
            Textitem2 = skillGUI.Textitem2;
            Imageitem1 = skillGUI.Imageitem1;
            Imageitem2 = skillGUI.Imageitem2;

            text1.text = "Damage:     " + playerlist.playerList[i]._DmgSkill1.ToString();
            text2.text = "Damage:     " + playerlist.playerList[i]._DmgSkill2.ToString();
            image1.sprite= playerlist.playerList[i]._Image1;
            image2.sprite = playerlist.playerList[i]._Image1;
            image3.sprite = playerlist.playerList[i]._Image2;
            image4.sprite = playerlist.playerList[i]._Image2;

            switch (listItem.Count) 
            {
               case 1:
                    Textitem1.text = listItem[0].info;
                    Imageitem1.sprite = listItem[0].image;
                    break;
                case 2:
                    Textitem1.text = listItem[0].info;
                    Imageitem1.sprite = listItem[0].image;
                    Textitem2.text = listItem[1].info;
                    Imageitem2.sprite = listItem[1].image;                
                    break;
                default:
                    break;
            }
            
        }else
        {
            gameObject.AddComponent<PlayerBehaviour>();
            playerBehaviour = GetComponent<PlayerBehaviour>();
            GetComponent<Player>().playerBehaviour = playerBehaviour;
        }  
        dmg = playerlist.playerList[i]._Dmg;
        dmg1 = playerlist.playerList[i]._DmgSkill1;
        dmg2 = playerlist.playerList[i]._DmgSkill2;
        
        SoundPunch = playerlist.playerList[i]._SoundPunch;
        SoundKick = playerlist.playerList[i]._SoundKick;
        SoundSkill1 = playerlist.playerList[i]._SoundSkill1;
        SoundSkill2 = playerlist.playerList[i]._SoundSkill2;

        anim = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;
        clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(clipOverrides);
        clipOverrides["Player1 Idle"] = playerlist.playerList[i].playIdle;
        clipOverrides["Player1 Kick"] = playerlist.playerList[i].playattack1;
        clipOverrides["Player1 Punch"] = playerlist.playerList[i].playattack2;
        clipOverrides["Player1 Skill1"] = playerlist.playerList[i].playskill1;
        clipOverrides["Player1 Skill2"] = playerlist.playerList[i].playskill2;
        animatorOverrideController.ApplyOverrides(clipOverrides);
        GetComponent<Animator>().runtimeAnimatorController = animatorOverrideController;
        
        
    }


    void Update()
    {
       
        if (h > 0 && !faceright)
        {
            Flip();
        }
        if (h < 0 && faceright) 
        {
            Flip();
        }
        if (attacking1)
        {

            if (dl > 0)
            {
                dl -= Time.deltaTime;

            }
            else
            {
                attacking1 = false;

                trigger.SetActive(false);

            }
        }
        if (attacking)
        {

            if (dl > 0)
            {
                dl -= Time.deltaTime;
                
            }
            else
            {

                attacking = false;
                trigger.SetActive(false);

            }
        }
        if (skill1)
        {
            if (skill1delay > 0)
            {
                skill1delay -= Time.deltaTime;
            }
            else
            {
                skill1 = false;
                trigger1.SetActive(false);
            }
        }
        if (skill2)
        {
            if (skill2delay > 0)
            {
                skill2delay -= Time.deltaTime;
            }
            else
            {
                skill2 = false;
                trigger2.SetActive(false);
            }
        }
        if (isCooldown1 && photonView.isMine)
        {
            imageColldown1.fillAmount += 1 / playerlist.playerList[i]._Cooldown1 * Time.deltaTime;


            if (imageColldown1.fillAmount >= 1)
            {
                imageColldown1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
        if (isCooldown2 && photonView.isMine)
        {

            imageColldown2.fillAmount += 1 / playerlist.playerList[i]._Cooldown2 * Time.deltaTime;

            if (imageColldown2.fillAmount >= 1)
            {
                imageColldown2.fillAmount = 0;
                isCooldown2 = false;
            }
        }


        if(!photonView.isMine)
        {
            isCooldown1 = false;
            isCooldown2 = false;
        }
        
        anim.SetBool("Skill1", skill1);
        anim.SetBool("Skill2", skill2);
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Attacking1", attacking1);
    }


    public void StartDame()
    {
        // photonView.RPC("StartSkill", PhotonTargets.All);
        StartSkill();

    }
    
    public void StartDame1()
    {
        // photonView.RPC("StartSkill1", PhotonTargets.All);
        StartSkill1();
    }

    [PunRPC]
    public void StartSkill1()
    {
        trigger2.SetActive(true);
        if (skill2delay > 0)
        {
            skill2delay -= Time.deltaTime;
        }
        else
        {
            skill2 = false;
            trigger2.SetActive(false);
        }
    }

    [PunRPC]
    public void StartSkill()
    {
        trigger1.SetActive(true);
        if (skill1delay > 0)
        {
            skill1delay -= Time.deltaTime;
        }
        else
        {
            skill1 = false;
            trigger1.SetActive(false);
        }
    }

    public void Flip()
    {
        faceright = !faceright;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;

    }
    public void Attack()
    {
        int random = UnityEngine.Random.Range(0, 2);
        trigger.SetActive(true);

        dl =playerlist.playerList[i]._Delay;

        if (random == 0)
        {
            attacking = true;
            audiosrc.PlayOneShot(SoundKick,SoundManager.Instance.volume);
        }
        else
        {
            attacking1 = true;
            audiosrc.PlayOneShot(SoundPunch, SoundManager.Instance.volume);
        }

    }
    public void AttackLeft()
    {
    
        if (attacking1 == false && attacking == false && skill1 == false && skill2 == false)
        {
            float h = -1;
            if (h > 0 && !faceright)
            {
                Flip();
            }
            if (h < 0 && faceright)
            {
                Flip();
            }
            Attack();


        }
        Invoke("Idle", 0.2f);
    }
    public void AttackRight()
    {
        if (attacking1 == false && attacking == false && skill1 == false && skill2 == false)
        {
            float h = 1;
            if (h > 0 && !faceright)
            {
                Flip();
            }
            if (h < 0 && faceright)
            {
                Flip();
            }
            Attack();

        }
        Invoke("Idle", 0.2f);
    }
    public void Skill1()
    {
        if (isCooldown1 == false && skill2 == false && attacking == false && attacking1 == false && skill1 == false)
        {
            isCooldown1 = true;
            skill1 = true;
            skill1delay = 0.7f;
            audiosrc.PlayOneShot(SoundSkill1, SoundManager.Instance.volume);
        }
        Invoke("Idle", 0.2f);
    }
    public void Skill2()
    {
        if (isCooldown2 == false && skill1 == false && attacking == false && attacking1 == false && skill2 == false)
        {
            isCooldown2 = true;
            skill2 = true;
            skill2delay = 0.7f;
            audiosrc.PlayOneShot(SoundSkill2, SoundManager.Instance.volume);
        }
        Invoke("Idle", 0.2f);

        
    }

    void Idle()
    {
        playerBehaviour.SetIdle();
    }
    public void UseItem1()
    {
        Textitem2.text = skillGUI.DefauTextItem;
        Imageitem2.sprite = skillGUI.DefaultItem;
        if (GameManager.Instance.item1)
        {
            Effect(listItem[1].index);
            GameManager.Instance.item1 = false;
        }
       
    }
    public void UseItem2()
    {
        Textitem1.text = skillGUI.DefauTextItem;
        Imageitem1.sprite = skillGUI.DefaultItem;
        if (GameManager.Instance.item2)
        {
            Effect(listItem[0].index);
            GameManager.Instance.item2 = false;
        }
       

    }
    public void Effect(int index)
    {
        switch (index)
        {
            case 0:

                Debug.Log("tang mau");
                break;
            case 1:
                Debug.Log("tang thu");
                break;
            case 2:
                Debug.Log("giam dame");
                break;

        }

    }

  
    
    public  void LoadData()
    {
        i = (int)photonView.instantiationData[0];
    }
  
}



