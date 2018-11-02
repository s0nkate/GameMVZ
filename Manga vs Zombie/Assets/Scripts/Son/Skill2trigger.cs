using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class Skill2trigger : MonoBehaviour {

    public Player1Controller p;

   


   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Heath>().SendMessageUpwards("TakeDamage", p.dmg2);
            col.GetComponent<Heath>().idAttack = transform.parent.GetComponent<Player>().id;

        }
    }
}
