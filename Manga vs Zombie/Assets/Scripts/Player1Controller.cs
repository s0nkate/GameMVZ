using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour
{

    public bool faceright = true;
    public float attackdelay = 0.4f;
    public bool attacking = false;
    public bool attacking1 = false;
    public Animator anim;
    public Collider2D trigger;
    public bool skill1 = false;
    public float skill1delay = 1;
    public bool skill2 = false;
    public float skill2delay = 0.5f;
    public Collider2D trigger1;
    public Collider2D trigger2;
    public Image imageColldown1;
    public float cooldown1 = 5;
    public bool isCooldown1=false;
    public Image imageColldown2;
    public float cooldown2 = 0.5f;
    public bool isCooldown2 = false;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;

    }


    void FixedUpdate()
    {
        float h = 0;
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
       
        if (isCooldown1)
            {
                imageColldown1.fillAmount += Time.deltaTime;
            }
            if (imageColldown1.fillAmount >= 1)
            {
                imageColldown1.fillAmount = 0;
                isCooldown1 = false;
            }
        if (isCooldown2)
        {
            imageColldown2.fillAmount += cooldown2*Time.deltaTime;
        }
        if (imageColldown2.fillAmount >= 1)
        {
            imageColldown2.fillAmount = 0;
            isCooldown2 = false;
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
        

      
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Attacking1", attacking1);
        anim.SetBool("Skill1", skill1);
        anim.SetBool("Skill2", skill2);


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
        int random = Random.Range(0, 2);
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
        trigger.enabled = true;
        attackdelay = 0.3f;
    }
    public void AttackRight()
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
        trigger.enabled = true;
        attackdelay = 0.3f;
    }
    public void Skill1()
    {
        isCooldown1 = true;
        skill1 = true;
        skill1delay = 1;
        trigger1.enabled = true;
        
        

    }
    public void Skill2()
    {
        isCooldown2 = true;
        skill2 = true;
        skill2delay = 0.5f;
        trigger2.enabled = true;

    }
   
}
    

