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
			public PhotonView photonView;
		}
		
		protected override void OnUpdate()
		{
			foreach (var entity in GetEntities<Data>())
			{
				if(!entity.zombieSpawn.isActived && PhotonNetwork.player.IsMasterClient)
				{
					entity.zombieSpawn.StartCoroutine(Addzombie(entity));
					entity.zombieSpawn.isActived = true;
				}
			}
		}

		// [PunRPC]
		IEnumerator Addzombie(Data entity)
		{
			GameObject zombieContainer = GameObject.FindWithTag("zombieobject");
			while(true)
			{
				string name = "Prefabs/Zombie/"+GetRandomZombiePrefabname(entity.zombieSpawn);
				// Debug.Log(name);
				// GameObject zombie = Object.Instantiate(GetRandomZombieGameObject(entity.zombieSpawn), entity.transform.position, entity.transform.localRotation) as GameObject;
				
				
				if(entity.photonView.isMine)
				{
					GameObject zombie =  (GameObject)PhotonNetwork.Instantiate(name,  entity.transform.position, entity.transform.localRotation, 0);
					zombie.transform.parent = zombieContainer.transform;
					//Nếu zombie từ phải sang thì xoay thanh máu theo trục y 180 độ
					if(!entity.transform.localRotation.Equals(new Vector3(0, 0, 0)))
					{
						zombie.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
					}
				}
				yield return new WaitForSeconds(entity.zombieSpawn.timeDelay);
			}
		}

		// GameObject GetRandomZombieGameObject(ZombieSpawn zombieSpawn)
		// {
		// 	int max = zombieSpawn.list.Count;
		// 	return zombieSpawn.list[Random.Range(0, max)];
		// }

		string GetRandomZombiePrefabname(ZombieSpawn zombieSpawn)
		{
			int max = zombieSpawn.list.Count;
			return zombieSpawn.list[Random.Range(0, max)].name;
		}
	}
}
