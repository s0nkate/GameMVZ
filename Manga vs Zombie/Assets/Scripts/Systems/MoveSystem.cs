using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;
using Unity.Mathematics;


namespace ECSSystem
{
	public class MoveSystem : ComponentSystem 
	{
		struct Data
		{
			public Transform transform;
			public Move move;
			public Faction faction;
			public Animator animator;
		}

		protected override void OnUpdate()
		{
			foreach (var e in GetEntities<Data>())
			{
				if((int)e.move.direction == -1)
				{
					e.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
				}

				if(e.faction.currentState == State.Walk)
				{
					e.animator.SetInteger("stage", (int)State.Walk);
					e.transform.Translate(e.move.speed * Time.deltaTime, 0 , 0);
				}
			}
		}
	}
}

