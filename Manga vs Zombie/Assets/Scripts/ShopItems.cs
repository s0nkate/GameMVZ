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
		ShopManager.Instance.itemSelected = gameObject;
		if(isBought)
		{
			if(isSelected)
			{
				
			}else
			{	
				ShopItems pre = ShopManager.Instance.itemSelected.GetComponent<ShopItems>();
				pre.isSelected = false;
				Debug.Log("Prename: " + pre.name);
				Debug.Log("Pre isSelected: " + pre.isSelected);
				Debug.Log("Pre Color: " + pre.GetComponent<Image>().color);
				isSelected = true;
			}
			
		}else
		{
			GetComponent<Image>().color = new Color(255, 217, 27);
			buyPopup.GetComponent<BuyPopup>().Click(this.gameObject);
		}		
	}

	public void Buy()
	{
		isBought = true;
		buyPopup.SetActive(false);
	}

}
