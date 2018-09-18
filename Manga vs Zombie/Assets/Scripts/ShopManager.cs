using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	public GameObject tab1;
	public GameObject tab2;
	public GameObject tab3;
	void Start () {
		tab1 = GetComponent<GameObject>();
		tab2 = GetComponent<GameObject>();
		tab3 = GetComponent<GameObject>();

		tab1.SetActive(true);
		tab1.SetActive(false);
		tab1.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
