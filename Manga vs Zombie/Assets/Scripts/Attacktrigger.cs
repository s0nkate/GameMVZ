using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacktrigger : MonoBehaviour {

    Player1Controller c;
    void Awake()
    {
        c= gameObject.transform.parent.GetComponent<Player1Controller>();
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if ( col.CompareTag("Enemy"))
        {
            col.SendMessageUpwards("Damage", c.dmg);
        }
    

    }

    
}
