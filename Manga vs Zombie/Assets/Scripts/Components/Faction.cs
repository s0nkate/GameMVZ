using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	public enum FactionType
    {
    	Player = 0,
        Zombie = 1
    }

	public enum State : int
	{
		Idle = 0,
		Walk = 1,
		Attack = 2,		
		Dead = 3
	}
	public class Faction : MonoBehaviour
    {
        public FactionType value;
		public State currentState;

    }

}