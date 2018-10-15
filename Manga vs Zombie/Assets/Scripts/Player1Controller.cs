﻿using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Player1Controller : MonoBehaviour
{
    public bool faceright = true;
    public static float attackdelay;
    public bool attacking = false;
    public bool attacking1 = false;
    public Animator anim;
    public Collider2D trigger;
    float h = 0;
    public bool skill1 = false;
    public float skill1delay = 1;
    public bool skill2 = false;
    public float skill2delay = 1;
    public Collider2D trigger1;
    public Collider2D trigger2;
    public Image imageColldown1;
    public bool isCooldown1 = false;
    public Image imageColldown2;
    public bool isCooldown2 = false;
    public static float dl;
    public static float Editcool1;
    public static float Editcool2;
    public static float Delay;
    public static int i=0;
    public Text text1;
    public Text text2;

    public InventoryPlayerList playerlist;
  
    protected AnimatorOverrideController animatorOverrideController;
    protected AnimationClipOverrides clipOverrides;

    public float dmg;
    public float dmg1;
    public float dmg2;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;
        
    }
    void Start()
    {
       

        LoadData();
        dmg = playerlist.playerList[i]._Dmg;
        dmg1 = playerlist.playerList[i]._DmgSkill1;
        dmg2 = playerlist.playerList[i]._DmgSkill2;
        Debug.Log("Id player:" +  i);
        text1.text = "Damage:     " + playerlist.playerList[i]._DmgSkill1.ToString();
        text2.text = "Damage:     " + playerlist.playerList[i]._DmgSkill2.ToString();

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

                trigger.enabled = false;

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
                trigger.enabled = false;

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
                trigger1.enabled = false;
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
                trigger2.enabled = false;
            }
        }
        if (isCooldown1)
        {
            imageColldown1.fillAmount += 1 / playerlist.playerList[i]._Cooldown1 * Time.deltaTime;


            if (imageColldown1.fillAmount >= 1)
            {
                imageColldown1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
        if (isCooldown2)
        {

            imageColldown2.fillAmount += 1 / playerlist.playerList[i]._Cooldown2 * Time.deltaTime;

            if (imageColldown2.fillAmount >= 1)
            {
                imageColldown2.fillAmount = 0;
                isCooldown2 = false;
            }
        }




       
        anim.SetBool("Skill1", skill1);
        anim.SetBool("Skill2", skill2);
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Attacking1", attacking1);

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
        trigger.enabled = true;

        dl =playerlist.playerList[i]._Delay;

        if (random == 0)
        {
            attacking = true;
        }
        else
        {
            attacking1 = true;
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
    }
    public void Skill1()

    {

        if (isCooldown1 == false && skill2 == false && attacking == false && attacking1 == false && skill1 == false)
        {
            isCooldown1 = true;
            skill1 = true;
            skill1delay = 1;
            trigger1.enabled = true;




        }
    }
    public void Skill2()
    {
        if (isCooldown2 == false && skill1 == false && attacking == false && attacking1 == false && skill2 == false)
        {
            isCooldown2 = true;
            skill2 = true;
            skill2delay = 1;
            trigger2.enabled = true;




        }
    }

    public  void LoadData()
    {

        i = playerlist.selectedPlayerindex;
    }
    
  
}


