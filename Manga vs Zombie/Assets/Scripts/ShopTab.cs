using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTab : MonoBehaviour {

	public List<GameObject> ListItem
	{
		get
		{	
			List<GameObject> list = new List<GameObject>();
			foreach(Transform child in transform)
				list.Add(child.gameObject);
			return list; 
		}
	}
}
