using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour
{
    public bool faceright = true;
    public static float  attackdelay;
    public bool attacking = false;
    public bool attacking1 = false;
    public Animator anim;
    public Collider2D trigger;
    public Player1Skill p;
    float h = 0;
    public  int dmg;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        p = gameObject.GetComponent<Player1Skill>();
        trigger.enabled = false;
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
            if (attackdelay > 0)
            {
                attackdelay -= Time.deltaTime;
            }
            else
            {
                attacking1 = false;

                trigger.enabled = false;

            }
        }
        if (attacking)
        {
            if (attackdelay > 0)
            {
                attackdelay -= Time.deltaTime;
            }
            else
            {

                attacking = false;
                trigger.enabled = false;

            }
        }

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
        dmg = 20;
        attackdelay = 1;
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
        if (attacking1 == false && attacking == false)
        {
            float h = -1;
            if (h > 0 && !faceright && p.skill1 == false && p.skill2 == false)
            {
                Flip();
            }
            if (h < 0 && faceright && p.skill1 == false && p.skill2 == false)
            {
                Flip();
            }
            Attack();

            
        }
    }
    public void AttackRight()
    {
        if (attacking1 == false && attacking == false)
        {
            float h = 1;
            if (h > 0 && !faceright && p.skill1 == false && p.skill2 == false)
            {
                Flip();
            }
            if (h < 0 && faceright && p.skill1 == false && p.skill2 == false)
            {
                Flip();
            }
            Attack();
            
        }
    }

    public static void UpdateABC(float value)
    {
        attackdelay = value;
        Debug.Log("Value = " + attackdelay);
    }
}


