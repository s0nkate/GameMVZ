using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	public enum State : int
	{
			Walk = 1,
			Attack = 2,
			Dead = 3
	}
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

		public State currentState;
		public ZoombieType type; 
	}
}
