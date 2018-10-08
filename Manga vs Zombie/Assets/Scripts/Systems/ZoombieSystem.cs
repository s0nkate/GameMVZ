using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
using Unity.Entities;

namespace ECSSystem
{
	public class ZoombieSystem : ComponentSystem 
	{
		struct Data
		{
			public Transform transform;
			public Zoombie zoombie;
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
					case ZoombieType.Walker:
						entity.heath.maxValue = 100;
						entity.move.speed = 1;
						entity.attack.damage = 10;
						entity.attack.timeDelay = 1;
						break;
					case ZoombieType.Runner:
						entity.heath.maxValue = 100;
						entity.move.speed = 2;
						entity.attack.damage = 10;
						entity.attack.timeDelay = 1;
						break;
					case ZoombieType.Hulker:
						entity.heath.maxValue = 300;
						entity.heath.value = 300;
						entity.move.speed = 1;
						entity.attack.damage = 20;
						entity.attack.timeDelay = 1.5f;
						entity.transform.localScale = new Vector3(.5f, .5f, 1);
						break;
					case ZoombieType.Exploder:
						entity.heath.maxValue = 100;
						entity.move.speed = 1;
						entity.attack.damage = 30;
						entity.attack.timeDelay = 1;
						ExploderType(entity);
						break;
				}
			}
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

