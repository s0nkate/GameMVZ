using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class Skill1trigger : MonoBehaviour {

    public Player1Controller p;

   private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Heath>().SendMessageUpwards("TakeDamage", p.dmg1);
        }
    }
}