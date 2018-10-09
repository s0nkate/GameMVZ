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
	
	[RequireComponent(typeof(Faction))]
	public class Zoombie : MonoBehaviour 
	{
		public Transform prefab;
		public int money;
		public int score;
		public ZoombieType type;
		public GameObject explosion; 
	}
}
