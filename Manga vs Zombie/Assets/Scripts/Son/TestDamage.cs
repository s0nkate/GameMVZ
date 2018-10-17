using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDamage : MonoBehaviour {
    
    public int Health = 1000;
    
    
    
    void Update () {
        if (Health <= 0)
        {
            Destroy(gameObject);
            
        }
      
    }
    void Damage(int damage)
    {
        Health -= damage;
        
    }
    
    
}
