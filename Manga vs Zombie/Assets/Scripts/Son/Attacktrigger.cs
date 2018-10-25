using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class Attacktrigger : MonoBehaviour
{
    public Player1Controller p;
    

     void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Heath>().SendMessageUpwards("TakeDamage", p.dmg);

        }
    }
}