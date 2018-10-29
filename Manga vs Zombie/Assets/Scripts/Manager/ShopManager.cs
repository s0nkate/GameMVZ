using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ECSComponent;

public class ShopManager : MonoBehaviour {

	private static ShopManager _instance;
	public Text moneyText;
	public int itemSaveCount;
	public int itemSelectCount;
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
		// inventoryPlayerList.selectedPlayerindex = 2;
		// GameObject tabPlayer = GameObject.FindGameObjectWithTag ("listplayer");
		// GameObject tabItem = GameObject.FindGameObjectWithTag ("listitem");
		// moneyText = GameObject.FindWithTag ("coin").GetComponent<Text> ();
		listPlayer = new List<ShopItems>();
		listItem = new List<ShopItems>();
		LoadPlayer(tabPlayer);
		LoadItem(tabItem);
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
	public void EnterGame () {

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
		foreach (var item in list)
		{
			if(item.isBought)
			{
				PlayerPrefs.SetString("Item"+ itemSaveCount++, item.name);
			}

			if(item.isSelected)
			{
				PlayerPrefs.SetString("Select"+ itemSelectCount++, item.name);
				Debug.Log("save item selcected");

			}
		}
	}
	public void Save()
	{
		PlayerPrefs.DeleteAll();
		itemSaveCount = 0;
		itemSelectCount = 0;
		
		SaveAList(listPlayer);
		SaveAList(listItem);

		// PlayerPrefs.SetInt("ItemSaveCount", itemSaveCount);
		// PlayerPrefs.SetInt("ItemSelectCount", itemSelectCount);
		// PlayerPrefs.SetInt("Money", GameManager.Instance.money);
		PlayerPrefs.Save();
	}

	// public void Load()
	// {

	// 	GameManager.Instance.money = PlayerPrefs.GetInt("Money");
	// 	itemSaveCount = PlayerPrefs.GetInt("ItemSaveCount");		
	// 	for(var i = 0; i< itemSaveCount; i++)
	// 	{
	// 		string itemName = PlayerPrefs.GetString("Item"+i);
	// 		GameObject.Find(itemName).GetComponent<ShopItems>().isBought = true;
	// 	}
	// 	SaveItemSelected();
	// }


	// public void SaveItemSelected()
	// {
	// 	itemSelectCount = PlayerPrefs.GetInt("ItemSelectCount");
	// 	for(var i = 0; i< itemSelectCount; i++)
	// 	{
	// 		string itemName = PlayerPrefs.GetString("Select"+i);
	// 		GameObject item = GameObject.Find(itemName);
	// 		item.GetComponent<ShopItems>().isSelected = true;
	// 		if(item.GetComponent<Player>() != null)
	// 		{
	// 			selectedPlayer = item.GetComponent<Player>();
	// 		}else if(item.GetComponent<Item>() != null)
	// 		{
	// 			selcectedItems.Add(item.GetComponent<Item>());
	// 		}
	// 	}
	// }


}