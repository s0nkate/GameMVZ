using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IShopItems {
	public static int selectedCount = 0;
	void Start () {

	}

	void Update () {

	}

	public void DisplayInfo () {

	}

    public string GetInfo()
    {
        return "\nDamage: \nTime Delay: ";
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