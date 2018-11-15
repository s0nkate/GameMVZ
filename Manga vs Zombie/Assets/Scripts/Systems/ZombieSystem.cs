using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
using Unity.Entities;

namespace ECSSystem
{
	public class ZombieSystem : ComponentSystem 
	{
		struct Data
		{
			public Transform transform;
			public Zombie zoombie;
			public Attack attack;
			public Heath heath;
			public Move move;
		}
		protected override void OnUpdate()
		{
			foreach (var entity in GetEntities<Data>())
			{
				switch (entity.zoombie.type)
				{
					case ZombieType.Walker:
						break;
					case ZombieType.Runner:
						break;
					case ZombieType.Hulker:
						entity.transform.localScale = new Vector3(.5f, .5f, 1);
						break;
					case ZombieType.Exploder:
						ExploderType(entity);
						break;
				}
			}
		}

		void ExploderType(Data entity)
		{
			if(entity.heath.value <= 0)
			{
				GameObject explore = Object.Instantiate(entity.zoombie.explosion, entity.transform.position, Quaternion.identity);
				Object.Destroy(entity.transform.gameObject);
				Object.Destroy(explore);
			}
		}
	}
}