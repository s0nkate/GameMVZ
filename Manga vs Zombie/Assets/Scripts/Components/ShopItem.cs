using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	public enum ShopItemType : int
	{
		Player = 0,
		Item = 1,
		Skill =2
	}

	[RequireComponent(typeof(ShopItem))]
	public class ShopItem : MonoBehaviour 
	{
		public int price;
		public bool isBought;
		public bool isSelected;
		public ShopItemType type;
	}
}

