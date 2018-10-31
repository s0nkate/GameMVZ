using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;
using UnityEngine.UI;

namespace ECSSystem
{
	public class HeathSystem : ComponentSystem 
	{
		struct Data
		{
			public Heath heath;
		}

		struct HouseData
		{
			public House house;
			public Heath heath;
			public PhotonView photonView;
		}

		struct ZombieData
		{
			public Zombie zoombie;
			public Animator animator;
			public Heath heath;
			public Faction faction;
		}
		protected override void OnUpdate()
		{
			CheckHeath();
			CheckDead();
			CheckHouse();
		}



		void CheckHouse()
		{
			foreach (var e in GetEntities<HouseData>())
			{
				if(e.heath.value <= 0 )
				{
					
					GameManager.Instance.GameOver();
				}
			}
		}

		void CheckHeath()
		{
			foreach (var e in GetEntities<Data>())
			{
				e.heath.heathSlider.maxValue = e.heath.maxValue;
				e.heath.heathSlider.value = e.heath.value;
				if(e.heath.OnInjured == null)
				{
					e.heath.OnInjured += this.OnInjured;
				}
			}
		}
		void CheckDead()
		{
			foreach (var e in GetEntities<ZombieData>())
			{
				if(e.heath.value <= 0 && e.faction.currentState != State.Dead)
				{
					e.faction.currentState = State.Dead;
					e.animator.SetInteger("stage", (int)State.Dead);

					// GameManager.Instance.AddMoney(e.zoombie.money);
					// GameManager.Instance.AddScore(e.zoombie.score);
				}
			}
		}

		private void OnInjured(Heath heath, int damage)
		{
			heath.value -= damage;
		}
	}
}

