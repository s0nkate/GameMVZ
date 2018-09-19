using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

	private static ShopManager _instance;
	public List<GameObject> listPlayer;
	public List<GameObject> listItem;
	public List<GameObject> listSkill;
	public GameObject itemSelected;
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
		DontDestroyOnLoad(gameObject);
	}

	public void Buy()
	{
		//kiem tra tien
		itemSelected.GetComponent<ShopItems>().Buy();
	}



}
