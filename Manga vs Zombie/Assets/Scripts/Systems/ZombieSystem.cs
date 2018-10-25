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
						ChangeProperties(entity, 100, 1, 10, 1);
						break;
					case ZombieType.Runner:
						ChangeProperties(entity, 100, 2, 10, 1);
						break;
					case ZombieType.Hulker:
						ChangeProperties(entity, 300, 1, 20, 1.5f);
						entity.transform.localScale = new Vector3(.5f, .5f, 1);
						break;
					case ZombieType.Exploder:
						ChangeProperties(entity, 100, 1, 30, 1);
						ExploderType(entity);
						break;
				}
			}
		}

		void ChangeProperties(Data entity, int maxHeath, float speed, int damage, float timeDelay)
		{
			entity.heath.maxValue = maxHeath;
			entity.move.speed = speed;
			entity.attack.damage = damage;
			entity.attack.timeDelay = timeDelay;
		}

		void ExploderType(Data entity)
		{
			if(entity.heath.value <= 0)
			{
				Object.Instantiate(entity.zoombie.explosion, entity.transform.position, Quaternion.identity);
				Object.Destroy(entity.transform.gameObject);
			}
		}
	}
}