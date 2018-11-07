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
			foreach (Data entity in GetEntities<Data>())
			{
				if(!GameManager.Instance.isPlaying)
				{
					entity.zombieSpawn.isActived = true;
				}

				if(PhotonNetwork.player.IsMasterClient && entity.zombieSpawn.isActived && GameManager.Instance.isPlaying)
				{
					entity.photonView.RPC("DisableAllZombie", PhotonTargets.All);
					entity.zombieSpawn.StartCoroutine(Addzombie(entity));
					entity.zombieSpawn.isActived = false;
				}
			}
		}

		IEnumerator Addzombie(Data entity)
		{
			while(GameManager.Instance.isPlaying)
			{
				entity.photonView.RPC("GetZombie", PhotonTargets.All);
				yield return new WaitForSeconds(entity.zombieSpawn.timeDelay);
			}
		}	
	}
}
