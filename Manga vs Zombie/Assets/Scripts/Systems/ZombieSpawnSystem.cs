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
			public PhotonView photonView;
		}
		
		protected override void OnUpdate()
		{
			foreach (var entity in GetEntities<Data>())
			{
				if(!entity.zombieSpawn.isActived)
				{
					entity.zombieSpawn.StartCoroutine(Addzombie(entity));
					entity.zombieSpawn.isActived = true;
				}
			}
		}

		IEnumerator Addzombie(Data entity)
		{
			while(true)
			{
				entity.zombiePool.GetZombie();	
				yield return new WaitForSeconds(entity.zombieSpawn.timeDelay);
			}
		}	
	}
}
