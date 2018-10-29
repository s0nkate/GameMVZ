using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ECSComponent
{
	public class House : MonoBehaviour 
	{
		public int defend;
		public InventorySceneList inventorySenceList;

		public static UnityEvent onNextLevel;

		void Start()
		{
			if (onNextLevel == null)
			{
            	onNextLevel = new UnityEvent();
			
			}
			onNextLevel.AddListener(LoadData);
		}

		public void LoadData()
		{
			int maxHeath = inventorySenceList.scenelist[GameManager.Instance.i].Healtower;
			GetComponent<Heath>().maxValue = maxHeath;
			GetComponent<Heath>().value = maxHeath;
		}


	}


}

