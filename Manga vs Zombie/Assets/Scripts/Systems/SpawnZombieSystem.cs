using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
using Unity.Entities;

namespace ECSSystem
{
	public class SpawnZombieSystem : ComponentSystem 
	{

		struct Data
		{
			public ZombieSpawn zombieSpawn;
			public PhotonView photonView;
		}

		
		protected override void OnUpdate()
		{
			// foreach (var entity in GetEntities<Data>())
			// {
				// if(!GameManager.Instance.isPlaying)
				// {
				// 	entity.zombieSpawn.isActived = true;
				// }

				// if(PhotonNetwork.player.IsMasterClient && GameManager.Instance.isPlaying)
				// {
				// 	Debug.Log("SpawnZombie");
				// 	entity.photonView.RPC("DisableAllZombie", PhotonTargets.All);
				// 	// entity.zombieSpawn.SpawnZombie(entity.photonView);
				// 	// entity.zombieSpawn.StartCoroutine(Addzombie(entity));
				// 	entity.zombieSpawn.isActived = false;
				// }
			// }
		}

		// IEnumerator Addzombie(Data entity)
		// {
		// 	while(GameManager.Instance.isPlaying)
		// 	{
		// 		entity.photonView.RPC("GetZombie", PhotonTargets.All);
		// 		yield return new WaitForSeconds(entity.zombieSpawn.timeDelay);
		// 	}
		// }	
	}
}
