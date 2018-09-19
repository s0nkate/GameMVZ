using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPopup : MonoBehaviour {

	private Text priceObj;
	private Image imageObj;	
	private GameObject item;
	void Start () {
		foreach (Transform child in transform)
		{
			
			if(child.tag == "price")
			{
				priceObj = child.GetComponent<Text>();
			}

			if(child.tag == "image")
			{
				imageObj = child.GetComponent<Image>();
			}
	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Click(GameObject item)
	{
		gameObject.SetActive(true);
		this.item = item;
		UpdateItem(item);
	}

	void UpdateItem(GameObject item)
	{
		priceObj.text = item.GetComponent<ShopItems>().priceObj.text;
		imageObj.sprite = item.GetComponent<ShopItems>().imageObj.sprite;
	}

	
}
