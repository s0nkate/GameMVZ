using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;
using UnityEngine.UI;
using System;

namespace ECSSystem
{
    public class EffectSystem : ComponentSystem
    {
		private const int hpUpValue = 100;
		private const int damageDownValue = 5;
		private const float timeEffect = 5;
		public float t = 0;
        struct HouseData
        {
			public House house;
            public Heath heath;
        }

		struct ZombieData
		{
			public Attack attack;
			public Zombie zombie;
		}

        
        protected override void OnUpdate()
        {
            switch(GameManager.Instance.effectIndex)
			{
				case 0:
					HPUp();
					break;
				case 1:
					HouseDeffent();
					break;
				case 2:
					DamageDown();
					break;
				default:
					break;
			}
			
        }

		void HPUp()
		{
			foreach (var entity in GetEntities<HouseData>())
			{
				int newHP = entity.heath.value + hpUpValue;
				entity.heath.value = newHP > entity.heath.maxValue ? entity.heath.maxValue : newHP;	
			}
			GameManager.Instance.effectIndex = -1;
		}

		void DamageDown()
		{
			foreach (var entity in GetEntities<ZombieData>())
			{
				entity.attack.damage = Math.Abs(entity.attack.damage - damageDownValue);
				// Debug.Log(entity.attack.damage);
				Debug.Log("DamageDown" + entity.attack.damage);
			}
			
			CleanEffect();
		}

		void HouseDeffent()
		{
			
			foreach (var entity in GetEntities<ZombieData>())
			{
				entity.attack.damage = 0;
				Debug.Log("DamageDown" + entity.attack.damage);
			}
			
			
			CleanEffect();
		}

		void CleanEffect()
		{
			if(t < timeEffect)
			{
				
				t += Time.deltaTime;
			}
			else
			{
				Debug.Log("time " + t);
				foreach (var entity in GetEntities<ZombieData>())
				{
					
					entity.attack.damage += damageDownValue;
					Debug.Log("damage " + entity.attack.damage);
				}
				
				t = 0;
				GameManager.Instance.effectIndex = -1;
			}
			
		}



    }
}

