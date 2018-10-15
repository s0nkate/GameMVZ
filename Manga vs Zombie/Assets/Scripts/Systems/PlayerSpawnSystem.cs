﻿using System.Collections;
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
				if(!entity.playerSpawn.isActived && entity.photonView.isMine)
				{
					PhotonNetwork.Instantiate("Prefabs/Player/Player", entity.transform.position, entity.transform.localRotation, 0);
					entity.playerSpawn.isActived = true;
				}
			}
		}
	}
}
