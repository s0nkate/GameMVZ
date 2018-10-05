using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	public class Faction : MonoBehaviour
    {
        public enum FactionType
        {
            Player = 0,
            Zoombie = 1
        }

        public FactionType Value;
    }

}