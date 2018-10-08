using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
using Unity.Entities;

namespace ECSSystem
{
	public class ZoombieSystemSpawn : ComponentSystem 
	{
		

		struct Data
		{
			public Transform transform;
			public ZoombieSpawn zoombieSpawn;
		}
		protected override void OnUpdate()
		{
			foreach (var entity in GetEntities<Data>())
			{
				if(!entity.zoombieSpawn.isActived)
				{
					entity.zoombieSpawn.StartCoroutine(AddZoombie(entity));
					entity.zoombieSpawn.isActived = true;
				}
			}
		}

		IEnumerator AddZoombie(Data entity)
		{
			while(true)
			{
				Object.Instantiate(GetRandomZoombieGameObject(entity.zoombieSpawn), entity.transform.position, Quaternion.identity);
				yield return new WaitForSeconds(entity.zoombieSpawn.timeDelay);
			}
		}

		GameObject GetRandomZoombieGameObject(ZoombieSpawn zoombieSpawn)
		{
			System.Random rand = new System.Random();
			int max = zoombieSpawn.list.Count;
			return zoombieSpawn.list[rand.Next(0, max)];
		}
	}
}
