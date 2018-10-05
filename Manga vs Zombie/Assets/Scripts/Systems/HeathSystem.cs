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


		struct ZoombieData
		{
			public Zoombie zoombie;
			public Animator animator;
			public Heath heath;
		}
		protected override void OnUpdate()
		{
			CheckHeath();
			CheckDead();
		}

		void CheckHeath()
		{
			foreach (var e in GetEntities<Data>())
			{
				e.heath.heathSlider.maxValue = e.heath.maxHeath;
				e.heath.heathSlider.value = e.heath.heath;
				if(e.heath.OnInjured == null)
				{
					e.heath.OnInjured += this.OnInjured;
				}

			}
		}

		void CheckDead()
		{
			foreach (var e in GetEntities<ZoombieData>())
			{
				if(e.heath.heath <= 0)
				{
					e.animator.SetInteger("stage", (int)State.Dead);
				}
			}
		}

		private void OnInjured(Heath heath, int damage)
		{
			heath.heath -= damage;
		}
	}
}

