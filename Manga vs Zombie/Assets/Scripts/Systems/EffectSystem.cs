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
		private const int damageDownValue =10;
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
            switch(GameManager.Instance.effectType)
			{
				case EffectType.HeathUp:
					HPUp();
					break;
				case EffectType.HouseDeffent:
					HouseDeffent();
					break;
				case EffectType.DamageDown:
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
			
			GameManager.Instance.effectType = EffectType.None;
		}

		void DamageDown()
		{
			foreach (var entity in GetEntities<ZombieData>())
			{
				entity.attack.damage = Math.Abs(entity.zombie.tempDamage - damageDownValue);
			}
			
			CleanEffect();
		}

		void HouseDeffent()
		{
			
			foreach (var entity in GetEntities<ZombieData>())
			{
				entity.attack.damage = 0;
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
					
					entity.attack.damage = entity.zombie.tempDamage;
				}
				t = 0;
				GameManager.Instance.effectType = EffectType.None;
			}		
		}
    }
}

