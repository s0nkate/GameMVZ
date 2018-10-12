using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopItems : MonoBehaviour {

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

	void Start () {
		

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
		info = GetComponent<IShopItems> ().GetInfo();
		if (isBought) {
			pricePanel.SetActive (false);
			GetComponent<Image> ().color = boughtColor;
			if (isSelected) {
				GetComponent<Image> ().color = selectedColor;
			} else { }

		} else {
			pricePanel.SetActive (true);
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
			buyPopup.GetComponent<BuyPopup> ().Click (gameObject);
		} 
	}

	public void Buy () {
		isBought = true;
		buyPopup.SetActive (false);
	}

}