﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ECSComponent
{
	public class Heath : MonoBehaviour 
	{
		public int heath;
		public int maxHeath;
		public Slider heathSlider;
		public delegate void InjuredHander(Heath heath, int damage);
		public InjuredHander OnInjured;
		
		public void TakeDamage(int damage)
		{
			if(OnInjured != null)
			{
				OnInjured(this, damage);
			}
		}
	}	
}