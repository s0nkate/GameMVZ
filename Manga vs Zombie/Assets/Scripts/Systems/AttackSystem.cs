using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;
using Unity.Mathematics;


namespace ECSSystem
{
	public class AttackSystem : ComponentSystem 
	{
		struct Data
		{
			public Faction faction;
			public Attack attack;
			public Animator animator;
		}

		protected override void OnUpdate()
		{
			foreach (var e in GetEntities<Data>())
			{	
				if(e.faction.currentState == State.Attack)
				{
					ChangeToAttack(e);					
				}
			}
		}


		void ChangeToAttack(Data data)
		{		
			if(data.attack.isAttack)
			{
				data.animator.SetInteger("stage", (int)State.Attack);
				// TakeDamage(data);
				if(data.faction.value == FactionType.Player)
				{
					data.attack.isAttack = false;
					data.faction.currentState = State.Idle;
				}
					
			}
		}

		void TakeDamage(Data data)
		{
				foreach (var item in data.attack.target)
				{
					if(data.faction.currentState == State.Dead)
					{
						data.attack.target.Remove(item);
						continue;
					}
					if(item != null)
					{
						item.TakeDamage(data.attack.damage);
					}
				}
		}

		// void TakeDamage(Data data)
		// {
		// 	if(time >= data.attack.timeDelay)
		// 	{
		// 		foreach (var item in data.attack.target)
		// 		{
		// 			if(data.faction.currentState == State.Dead)
		// 			{
		// 				data.attack.target.Remove(item);
		// 				continue;
		// 			}
		// 			item.TakeDamage(data.attack.damage);
		// 		}
		// 		time = 0;
		// 	}else
		// 	{
		// 		time += Time.deltaTime;
		// 	}
		// }
	}
}

