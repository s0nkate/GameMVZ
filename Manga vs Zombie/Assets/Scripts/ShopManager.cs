using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

	private static ShopManager _instance;
	public List<GameObject> listPlayer;
	public List<GameObject> listItem;
	public List<GameObject> listSkill;
	public List<GameObject> itemSelected;
	public static ShopManager Instance
	{
		get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ShopManager>();
            }

			if(_instance == null)
			{
				GameObject go = new GameObject();
				_instance = go.AddComponent<ShopManager>();
			}

            return _instance;
        }

	}

	void Awake()
	{
		GameObject tabPlayer = GameObject.FindGameObjectWithTag("listplayer");
		GameObject tabItem = GameObject.FindGameObjectWithTag("listitem");
		GameObject tabSkill = GameObject.FindGameObjectWithTag("listskill");

		listPlayer = tabPlayer.GetComponent<ShopTab>().ListItem;
		listItem = tabItem.GetComponent<ShopTab>().ListItem;
		listSkill = tabSkill.GetComponent<ShopTab>().ListItem;
		DontDestroyOnLoad(gameObject);
	}

	public void Click(ShopItems go)
	{
		ShopItems temp;
		go.isBought = true;
		foreach(GameObject item in listPlayer)
		{
			temp = item.GetComponent<ShopItems>();
			if(temp.isSelected)
			{
				if(go != temp)
				{
					go.isSelected = true;
					temp.isSelected = false;
				}
			}
		}

		foreach(GameObject item in listItem)
		{
			temp = item.GetComponent<ShopItems>();
			if(temp.isSelected)
			{
				if(go != temp)
				{
					go.isSelected = true;
					temp.isSelected = false;
				}
			}
		}

		foreach(GameObject item in listSkill)
		{
			temp = item.GetComponent<ShopItems>();
			if(temp.isSelected)
			{
				if(go != temp)
				{
					go.isSelected = true;
					temp.isSelected = false;
				}
			}
		}
	}
	void Update()
	{
		
	}
	public void Buy()
	{
		//kiem tra tien
		//itemSelected.GetComponent<ShopItems>().Buy();
	}



}
