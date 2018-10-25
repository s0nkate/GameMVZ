using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseButton : MonoBehaviour {

	private ShopItems parent;
	void Start () {
		parent = transform.parent.GetComponent<ShopItems>();

	}
	
	void Update () {
		if(parent.isSelected)
		{
			gameObject.GetComponentInChildren<Text>().text = "Cancel";
		}
		else
		{
			gameObject.GetComponentInChildren<Text>().text = "Use";
		}
	}
}
