using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ShopItemType : int
	{
		Player = 0,
		Item = 1,
	}
public class ShopItems : MonoBehaviour {
	public int index;
	public ShopItemType type;
	public int price;
	public Sprite image;
	public bool isBought;
	public Color boughtColor;
	public bool isSelected = false;
	public Color selectedColor;
	public GameObject buyPopup;
	public Text priceObj;
	public Image imageObj;
	public string info;
	public bool canBuy = true;
	public Text infoObj;
	private GameObject pricePanel;
	private Color color;
	public GameObject useButton;
	public string name;

	void Start () {
		
		useButton = transform.Find("UseButton").gameObject;
		color = GetComponent<Image> ().color;

		foreach (Transform child in transform) {

			if (child.tag == "price") {
				pricePanel = child.gameObject;
				priceObj = child.Find ("price").GetComponent<Text> ();
			}

			if (child.tag == "image") {
				imageObj = child.GetComponent<Image> ();
			}

			if (child.tag == "info") {
				infoObj = child.GetComponent<Text> ();
			}

		}
	}

	// Update is called once per frame
	void Update () {
		
		if (isBought) {
			pricePanel.SetActive (false);
			useButton.SetActive(true);
			GetComponent<Image> ().color = boughtColor;
			if (isSelected) {
				GetComponent<Image> ().color = selectedColor;
			} else
			{
				
			}

		} else {
			pricePanel.SetActive (true);
			useButton.SetActive(false);
			GetComponent<Image> ().color = color;
		}

		if(!canBuy)
		{
			GetComponent<Image> ().color = Color.gray;
			pricePanel.GetComponent<Image>().color = Color.gray;
		}else
		{
			pricePanel.GetComponent<Image>().color = Color.white;
		}

		priceObj.text = price.ToString ();
		imageObj.sprite = image;
		infoObj.text = info;

	}

	public void Click () {
		if (!isBought && canBuy) {
			Debug.Log("click buy");
			buyPopup.GetComponent<BuyPopup> ().Click (gameObject);
		} 
	}

	public void Buy () {
		isBought = true;
		buyPopup.SetActive (false);
	}

	public void ChangeSelected()
	{	

		//unselect		
		if(isSelected)
		{
			if(type == ShopItemType.Player)
			{
				return;
			}
			else 
			{
				isSelected = false;
			}		
		}
		//select
		else
		{
			if(type == ShopItemType.Player)
			{
				isSelected = true;
			}
			else
			{
				isSelected = true;
			}	
		}		
		// Debug.Log(ShopManager.Instance.G);
		ShopManager.Instance.Save();	
	}
	

}