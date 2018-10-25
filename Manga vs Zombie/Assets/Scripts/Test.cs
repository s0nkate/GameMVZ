using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	List<GameObject> list;
	void Start () {
		
		switch (transform.tag)
		{
			case "listplayer" : 
				list = ShopManager.Instance.listPlayer;
				break;
			case "listitem" : 
				list = ShopManager.Instance.listItem;
				break;
			case "listskill" : 
				list = ShopManager.Instance.listSkill;
				break;
		}
		Debug.Log(transform.tag);
		foreach (var item in list)
		{
			if(item.GetComponent<ShopItems>().isBought)
			{
				GameObject copy = Instantiate(item);
				copy.name = item.name;
				copy.transform.parent = transform;
				copy.transform.localScale = new Vector3(1, 1, 1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
