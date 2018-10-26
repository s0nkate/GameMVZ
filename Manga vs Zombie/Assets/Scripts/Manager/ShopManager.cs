using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	private static ShopManager _instance;
	private Text moneyText;
	public int itemSaveCount;
	public int itemSelectCount;
	public List<GameObject> listPlayer;
	public List<GameObject> listItem;
	public List<GameObject> listSkill;
	public Player selectedPlayer;
	public List<Item> selcectedItems;
	public List<Skill> selcectedSkills;
	public InventoryPlayerList inventoryPlayerList;
	public InventoryItemList inventoryItemList;
	public GameObject prefab;
	public GameObject buyPopup;
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
		GameObject tabPlayer = GameObject.FindGameObjectWithTag ("listplayer");
		GameObject tabItem = GameObject.FindGameObjectWithTag ("listitem");
		moneyText = GameObject.FindWithTag ("coin").GetComponent<Text> ();
		
		LoadPlayer(tabPlayer);
		// Load();
		// Display(false);
		//DontDestroyOnLoad(gameObject);
	}

	void LoadPlayer(GameObject tabPlayer)
	{
		foreach (var player in inventoryPlayerList.playerList)
		{
			GameObject playerPrefab = Instantiate(prefab, transform.position, transform.localRotation, tabPlayer.transform) as GameObject;
			// playerPrefab.AddComponent<>();
			ShopItems shopItems = playerPrefab.GetComponent<ShopItems>();
			shopItems.price = player.Price;
			Texture2D  texture = player._image;
			shopItems.image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			shopItems.name = player._Name;
			string info = "Name: " + player._Name +"\nDamage: " + player._Dmg + "\nTime delay:" + player._Delay;
			shopItems.info = info;
			shopItems.buyPopup = buyPopup;
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
		moneyText = GameObject.FindWithTag ("coin").GetComponent<Text> ();
		//coin = int.Parse (coinText.text);
		moneyText.text = GameManager.Instance.Gold.ToString ();
		CheckItem(listItem);
		CheckItem(listPlayer);
		CheckItem(listSkill);
	}
	public void EnterGame () {

	}

	//
	public void CheckItem(List<GameObject> list)
	{
		ShopItems temp;
		foreach (GameObject one in list) {
			temp = one.GetComponent<ShopItems> ();
			if(!temp.isBought)
			{	
				if(temp.price > GameManager.Instance.Gold)
					temp.canBuy = false;
				else
					temp.canBuy = true;
			}
		}
	}

	// display shop panel
	public void Display(bool status)
	{
		if(status == false)
			transform.localScale = new Vector3(0, 0, 0);
		else
			transform.localScale = new Vector3(1, 1, 1);
	}

	void SaveAList(List<GameObject> list)
	{
		foreach (var item in list)
		{
			ShopItems shopItem = item.GetComponent<ShopItems>();
			if(shopItem.isBought)
			{
				PlayerPrefs.SetString("Item"+ itemSaveCount++, item.name);
			}

			if(shopItem.isSelected)
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
		SaveAList(listSkill);

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


	public void SaveItemSelected()
	{
		itemSelectCount = PlayerPrefs.GetInt("ItemSelectCount");
		for(var i = 0; i< itemSelectCount; i++)
		{
			string itemName = PlayerPrefs.GetString("Select"+i);
			GameObject item = GameObject.Find(itemName);
			item.GetComponent<ShopItems>().isSelected = true;
			if(item.GetComponent<Player>() != null)
			{
				selectedPlayer = item.GetComponent<Player>();
			}else if(item.GetComponent<Item>() != null)
			{
				selcectedItems.Add(item.GetComponent<Item>());
			}else
			{
				selcectedSkills.Add(item.GetComponent<Skill>());
			}
		}
	}


}