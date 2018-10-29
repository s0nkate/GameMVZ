using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ECSComponent;

public class ShopManager : MonoBehaviour {

	private static ShopManager _instance;
	public Text moneyText;
	public int itemBoughtCount;
	public int playerBoughtCount;
	public List<ShopItems> listPlayer;
	public List<ShopItems> listItem;
	// public Player selectedPlayer;
	// public List<Item> selcectedItems;
	public InventoryPlayerList inventoryPlayerList;
	public InventoryItemList inventoryItemList;
	public GameObject prefab;
	public GameObject buyPopup;
	public GameObject tabPlayer;
	public GameObject tabItem;
	public static ShopManager Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<ShopManager> ();
			}

			if (_instance == null) {
				GameObject go = new GameObject ();
				_instance = go.AddComponent<ShopManager> ();
			}

			return _instance;
		}

	}

	void Start () {
		listPlayer = new List<ShopItems>();
		listItem = new List<ShopItems>();
		LoadPlayer(tabPlayer);
		LoadItem(tabItem);
		GameManager.Instance.playerShopList = ShopManager.Instance.listPlayer;
        GameManager.Instance.itemShopList = ShopManager.Instance.listItem;
		CheckBuyAndSelect();
		// Load();
		// Display(false);
		//DontDestroyOnLoad(gameObject);
	}

	void LoadPlayer(GameObject tabPlayer)
	{
		for(int i = 0; i < inventoryPlayerList.playerList.Count; i++)
		{
			GameObject playerPrefab = Instantiate(prefab, transform.position, transform.localRotation, tabPlayer.transform) as GameObject;
			ShopItems shopItems = playerPrefab.GetComponent<ShopItems>();
			shopItems.index = i;
			shopItems.price = inventoryPlayerList.playerList[i].Price;
			Texture2D  texture = inventoryPlayerList.playerList[i]._image;
			shopItems.image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			shopItems.name = inventoryPlayerList.playerList[i]._Name;
			string info = "Name: " + inventoryPlayerList.playerList[i]._Name +"\nDamage: " + inventoryPlayerList.playerList[i]._Dmg + "\nTime delay:" + inventoryPlayerList.playerList[i]._Delay;
			shopItems.info = info;
			shopItems.type = ShopItemType.Player;
			shopItems.buyPopup = buyPopup;
			listPlayer.Add(shopItems);
		}
	}

	void LoadItem(GameObject tabItem)
	{
		for(int i = 0; i < inventoryItemList.itemlist.Count; i++)
		{
			GameObject itemPrefab = Instantiate(prefab, transform.position, transform.localRotation, tabItem.transform) as GameObject;
			ShopItems shopItems = itemPrefab.GetComponent<ShopItems>();
			shopItems.index = i;
			shopItems.price = inventoryItemList.itemlist[i].price;
			Texture2D  texture = inventoryItemList.itemlist[i].image;
			shopItems.image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			shopItems.name = inventoryItemList.itemlist[i].nameItem;
			string info = inventoryItemList.itemlist[i].effect;
			shopItems.info = info;
			shopItems.type = ShopItemType.Item;
			shopItems.buyPopup = buyPopup;
			listItem.Add(shopItems);
		}
	
	}


	public void Buy (ShopItems go) {
		//kiem tra tien
		if (go.price <= GameManager.Instance.Gold && go.price >= 0 && !go.isBought) {
			GameManager.Instance.Gold -= go.price;
			go.isBought = true;
			Save();
		}
	}

	
	void Update () {
		// moneyText = GameObject.FindWithTag ("coin").GetComponent<Text> ();
		//coin = int.Parse (coinText.text);
		moneyText.text = GameManager.Instance.Gold.ToString ();
		CheckItem(listItem);
		CheckItem(listPlayer);
	}
	public void EnterGame () 
	{

	}

	//
	public void CheckItem(List<ShopItems> list)
	{
		foreach (ShopItems item in list) {
			if(!item.isBought)
			{	
				if(item.price > GameManager.Instance.Gold)
					item.canBuy = false;
				else
					item.canBuy = true;
			}
		}
	}

	void SaveAList(List<ShopItems> list)
	{
		
		int itemSelected = 0;
		foreach (var item in list)
		{
			if(item.isBought)
			{
				if(item.type == ShopItemType.Player)
				{
					PlayerPrefs.SetInt("PlayerBought"  + playerBoughtCount++, item.index);
				}
				else
				{
					PlayerPrefs.SetInt("ItemBought"  + itemBoughtCount++, item.index);
				}
			}

			if(item.isSelected)
			{
				if(item.type == ShopItemType.Player)
					PlayerPrefs.SetInt("PlayerSelected", item.index);
				else
					PlayerPrefs.SetInt("ItemSelected" + itemSelected++, item.index);
			}
		}


	}

	void CheckBuyAndSelect()
	{
		itemBoughtCount = PlayerPrefs.GetInt("ItemBoughtCount", -1);		
		playerBoughtCount = PlayerPrefs.GetInt("PlayerBoughtCount", -1);
		//Set player and item bought
		for(int i = 0; i<playerBoughtCount; i++)
		{
			int index = PlayerPrefs.GetInt("PlayerBought" + i);
			GameManager.Instance.playerShopList[index].isBought = true;
		}

		for(int i = 0; i<itemBoughtCount; i++)
		{
			int index = PlayerPrefs.GetInt("ItemBought" + i);
			GameManager.Instance.itemShopList[index].isBought = true;
		}

		//Set player and item selected
		int playerSelected = PlayerPrefs.GetInt("PlayerSelected", -1);
		if(playerSelected != -1)
		{
			GameManager.Instance.playerShopList[playerSelected].isSelected = true;
		}

		int itemSelected0 = PlayerPrefs.GetInt("ItemSelected0", -1);
		if(itemSelected0 != -1)
		{
			ShopItems.itemSelected++;
			GameManager.Instance.itemShopList[itemSelected0].isSelected = true;
		}

		int itemSelected1 = PlayerPrefs.GetInt("ItemSelected1", -1);
		if(itemSelected1 != -1)
		{
			ShopItems.itemSelected++;
			GameManager.Instance.itemShopList[itemSelected1].isSelected = true;
		}
	}
	public void Save()
	{
		PlayerPrefs.DeleteAll();
		itemBoughtCount = 0;
		playerBoughtCount = 0;
		SaveAList(listPlayer);
		SaveAList(listItem);

		PlayerPrefs.SetInt("ItemBoughtCount", itemBoughtCount);
		PlayerPrefs.SetInt("PlayerBoughtCount", playerBoughtCount);
		// PlayerPrefs.SetInt("Money", GameManager.Instance.money);
		PlayerPrefs.Save();
	}
}