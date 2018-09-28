using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour, IShopItems {

	public int damage;
	public int timeDelay;

    public string GetInfo()
    {
        return "\nDamage: "+ damage+ "\nTime Delay: "+ timeDelay;
    }

    void Start () {

	}

	void Update () {

	}
}