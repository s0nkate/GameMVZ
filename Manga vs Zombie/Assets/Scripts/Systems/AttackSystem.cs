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
		float time = 0;
		struct Data
		{
			public Zoombie zoombie;
			public Attack attack;
			public Animator animator;
		}

		protected override void OnUpdate()
		{
			foreach (var e in GetEntities<Data>())
			{	
				if(e.zoombie.currentState == State.Attack)
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
				TakeDamage(data);
			}
		}



		void TakeDamage(Data data)
		{
			if(time >= data.attack.timeDelay)
			{
				data.attack.target.TakeDamage(data.attack.damage);
				time = 0;
			}else
			{
				time += Time.deltaTime;
			}
		}
	}
}

