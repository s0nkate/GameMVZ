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
		GameObject tabPlayer = GameObject.FindGameObjectWithTag ("listplayer");
		GameObject tabItem = GameObject.FindGameObjectWithTag ("listitem");
		GameObject tabSkill = GameObject.FindGameObjectWithTag ("listskill");
		moneyText = GameObject.FindWithTag ("coin").GetComponent<Text> ();

		listPlayer = tabPlayer.GetComponent<ShopTab> ().ListItem;
		listItem = tabItem.GetComponent<ShopTab> ().ListItem;
		listSkill = tabSkill.GetComponent<ShopTab> ().ListItem;
		Load();
		//DontDestroyOnLoad(gameObject);
	}

	public void Buy (ShopItems go) {
		//kiem tra tien
		if (go.price <= GameManager.Instance.money && go.price >= 0 && !go.isBought) {
			GameManager.Instance.money -= go.price;
			go.isBought = true;
			Save();
		}
	}


	void Update () {
		moneyText = GameObject.FindWithTag ("coin").GetComponent<Text> ();
		//coin = int.Parse (coinText.text);
		moneyText.text = GameManager.Instance.money.ToString ();
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
				if(temp.price > GameManager.Instance.money)
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

		PlayerPrefs.SetInt("ItemSaveCount", itemSaveCount);
		PlayerPrefs.SetInt("ItemSelectCount", itemSelectCount);
		PlayerPrefs.SetInt("Money", GameManager.Instance.money);
		PlayerPrefs.Save();
	}

	public void Load()
	{
		GameManager.Instance.money = PlayerPrefs.GetInt("Money");
		itemSaveCount = PlayerPrefs.GetInt("ItemSaveCount");		
		for(var i = 0; i< itemSaveCount; i++)
		{
			string itemName = PlayerPrefs.GetString("Item"+i);
			GameObject.Find(itemName).GetComponent<ShopItems>().isBought = true;
		}
		SaveItemSelected();
	}


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