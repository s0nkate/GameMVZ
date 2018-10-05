using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	public enum SkillType : int
	{
		Skill1 = 1,
		Skill2 = 2
	}

	public class Skill : MonoBehaviour 
	{
		public bool isUse;
		public int damage;
		public float timeDelay;
		public SkillType type;	
	}
}

