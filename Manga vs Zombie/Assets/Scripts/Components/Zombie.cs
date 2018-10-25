using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace ECSComponent
{
	
	public enum ZombieType
	{
			Walker,
			Runner,
			Hulker,
			Exploder
	}
	
	[RequireComponent(typeof(Faction))]
	public class Zombie : MonoBehaviour 
	{
		public Transform prefab;
		public int money;
		public int score;
		public ZombieType type;
		public GameObject explosion; 
	}
}
