using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	[RequireComponent(typeof(ShopItem))]
	public class Player : MonoBehaviour 
	{	
		public int damage;
		public float timeDelay;
	}

}
