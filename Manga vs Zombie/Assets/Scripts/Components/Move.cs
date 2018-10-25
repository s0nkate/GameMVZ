using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	
	public enum Direction : int
	{
			Left = -1,
			Right = 1
	}
	public class Move : MonoBehaviour 
	{
		public float speed;
		public Direction direction;
	}

}
