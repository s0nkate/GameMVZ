using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
using Unity.Entities;

namespace ECSSystem
{
	public class PlayerSpawnSystem : ComponentSystem 
	{
		struct Data
		{
			public Transform transform;
			public PlayerSpawn playerSpawn;
			public PhotonView photonView;
		}

		protected override void OnUpdate()
		{
			foreach (var entity in GetEntities<Data>())
			{
				Debug.Log(entity.playerSpawn.isActived);
				Debug.Log("isMine " + entity.photonView.isMine);
				if(!entity.playerSpawn.isActived)
				{
					Debug.Log("instance player");
					GameObject player = PhotonNetwork.Instantiate("Prefabs/Player/Player", entity.transform.position, entity.transform.localRotation, 0) as GameObject;
					entity.playerSpawn.isActived = true;
				}
			}
		}
	}
}
