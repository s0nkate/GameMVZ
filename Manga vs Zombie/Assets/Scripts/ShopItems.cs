using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItems : MonoBehaviour {

	public string price;
	public Sprite image;
	public bool isBought;

	private Text priceObj;
	private Image imageObj;
	private GameObject pricePanel;

	void Start () {

		
		foreach (Transform child in transform)
		{
			

			if(child.tag == "price")
			{
				pricePanel = child.gameObject;
				priceObj = child.Find("price").GetComponent<Text>();
			}

			if(child.tag == "image")
			{
				imageObj = child.GetComponent<Image>();
			}
			
		}
	}
	
	// Update is called once per frame
	void Update () {

		if( isBought)
		{
			pricePanel.SetActive(false);
			GetComponent<Image>().color = Color.blue;
		}else
		{
			pricePanel.SetActive(true);
			GetComponent<Image>().color = new Color(236, 236, 236);
		}

		priceObj.text = price;
		imageObj.sprite = image;
	}

	public void Click()
	{
		GetComponent<Image>().color = new Color(6, 217, 27);
	}


}
