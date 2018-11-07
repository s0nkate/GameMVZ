using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
using Unity.Entities;

namespace ECSSystem
{
	public class ZombieSpawnSystem : ComponentSystem 
	{
		struct Data
		{
			public Transform transform;
			public ZombieSpawn zombieSpawn;
			public ZombiePool zombiePool;
			
		}
		
		protected override void OnUpdate()
		{
			foreach (Data entity in GetEntities<Data>())
			{
				if(!GameManager.Instance.isPlaying)
				{
					entity.zombieSpawn.isActived = true;
				}

				if(entity.zombieSpawn.isActived && GameManager.Instance.isPlaying)
				{
					entity.zombieSpawn.StartCoroutine(Addzombie(entity));
					entity.zombieSpawn.isActived = false;
				}
			}
		}

		IEnumerator Addzombie(Data entity)
		{
			while(GameManager.Instance.isPlaying)
			{
				entity.zombiePool.GetZombie();	
				yield return new WaitForSeconds(entity.zombieSpawn.timeDelay);
			}
		}	
	}
}
