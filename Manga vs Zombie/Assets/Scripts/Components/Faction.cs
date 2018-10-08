using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	public enum FactionType
    {
    	Player = 0,
        Zoombie = 1
    }

	public enum State : int
	{
		Walk = 1,
		Attack = 2,		
		Dead = 3
	}
	public class Faction : MonoBehaviour
    {
        

        public FactionType Value;
		public State currentState;

    }

}