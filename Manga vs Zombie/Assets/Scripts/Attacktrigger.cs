﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacktrigger : MonoBehaviour
{
    public Player1Controller p;
   


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.SendMessageUpwards("Damage", p.Editdmg);

        }
    }
}
