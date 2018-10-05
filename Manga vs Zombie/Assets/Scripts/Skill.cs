using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour, IShopItems {

    public static int selectedCount = 0;
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
    public bool SelectItem()
    {
		if(selectedCount >= 2)
		{
			return false;
		}
		selectedCount++;
		return true;
    }

    public void UnSelectItem()
	{
		if(selectedCount > 0)
			selectedCount--;
	}
}