using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItems : MonoBehaviour {

	public string price;
	public Sprite image;
	public bool isBought;
	public Color boughtColor;
	public bool isSelected = false;
	public Color selectedColor;
	public GameObject buyPopup;
	public Text priceObj;
	public Image imageObj;
	private GameObject pricePanel;
	private Color color;
	
	

	void Start () {

		color = GetComponent<Image>().color;

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
			GetComponent<Image>().color = boughtColor;
			if(isSelected)
			{
				GetComponent<Image>().color = selectedColor;
			}else
			{
			}
			
		}else
		{
			pricePanel.SetActive(true);
			GetComponent<Image>().color = color;
		}
		
				
		priceObj.text = price;
		imageObj.sprite = image;
	}

	public void Click()
	{	
		if(isBought)
		{
			ShopManager.Instance.Click(gameObject.GetComponent<ShopItems>());
		}else
			buyPopup.GetComponent<BuyPopup>().Click(gameObject);
	}

	public void Buy()
	{
		isBought = true;
		buyPopup.SetActive(false);
	}

}
