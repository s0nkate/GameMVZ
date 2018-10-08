using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECSComponent
{
	
	public enum ZoombieType
	{
			Walker,
			Runner,
			Hulker,
			Exploder
	}
	public class Zoombie : MonoBehaviour 
	{
		public Transform prefab;

		public ZoombieType type;
		public GameObject explosion; 
	}
}
