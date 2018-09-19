using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {

    public bool faceright = true;
    public float attackdelay = 0.3f;
    public bool attacking = false;
    public bool attacking1 = false;
    public Animator anim;
    public Collider2D trigger;
   
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;
        
    }

    
    void Update () {
        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !faceright)
        {
            Flip();


        }
        if (h < 0 && faceright)
        {
            Flip();


        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)&& !attacking)
        {
            Attack();
            trigger.enabled = true;
            attackdelay = 0.3f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !attacking)
        {
            Attack();
            trigger.enabled = true;
            attackdelay = 0.3f;
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
        Scale= transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
    public void Attack()
    {
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            attacking = true;
        }
        else 
        {
            attacking1 = true;
        }


    }
}
