using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	private static ShopManager _instance;
	private Text _coinText;
	public int coin;
	public List<GameObject> listPlayer;
	public List<GameObject> listItem;
	public List<GameObject> listSkill;
	public List<GameObject> itemSelected;
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

	void Awake () {
		GameObject tabPlayer = GameObject.FindGameObjectWithTag ("listplayer");
		GameObject tabItem = GameObject.FindGameObjectWithTag ("listitem");
		GameObject tabSkill = GameObject.FindGameObjectWithTag ("listskill");

		listPlayer = tabPlayer.GetComponent<ShopTab> ().ListItem;
		listItem = tabItem.GetComponent<ShopTab> ().ListItem;
		listSkill = tabSkill.GetComponent<ShopTab> ().ListItem;
		
		//DontDestroyOnLoad(gameObject);
	}

	public void Buy (ShopItems go) {
		//kiem tra tien
		if (go.price <= coin && go.price >= 0 && !go.isBought) {
			coin = coin - go.price;
			go.isBought = true;
			return;
		}
	}

	void Update () {
		_coinText = GameObject.FindWithTag ("coin").GetComponent<Text> ();
		//coin = int.Parse (_coinText.text);
		_coinText.text = coin.ToString ();
		CheckItem(listItem);
		CheckItem(listPlayer);
		CheckItem(listSkill);
	}

	public void EnterGame () {

	}

	public void CheckItem(List<GameObject> list)
	{
		ShopItems temp;
		foreach (GameObject one in list) {
			temp = one.GetComponent<ShopItems> ();
			if(!temp.isBought)
			{	
				if(temp.price > coin)
					temp.canBuy = false;
				else
					temp.canBuy = true;
			}
		}
	}

}