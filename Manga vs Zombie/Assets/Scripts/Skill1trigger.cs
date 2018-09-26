using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1trigger : MonoBehaviour {

     Player1Skill p;

    void Awake()
    {
        p = gameObject.transform.parent.GetComponent<Player1Skill>();
    }




    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.SendMessageUpwards("Damage", p.dmg1);
            
        }


    }
}
