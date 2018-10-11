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
				GameObject zombie = Object.Instantiate(GetRandomZoombieGameObject(entity.zoombieSpawn), entity.transform.position, entity.transform.localRotation) as GameObject;
				//Nếu zoombie từ phải sang thì xoay thanh máu theo trục y 180 độ
				if(!entity.transform.localRotation.Equals(new Vector3(0, 0, 0)))
				{
					zombie.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
				}
				yield return new WaitForSeconds(entity.zoombieSpawn.timeDelay);
			}
		}

		GameObject GetRandomZoombieGameObject(ZoombieSpawn zoombieSpawn)
		{
			System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
			int max = zoombieSpawn.list.Count;
			return zoombieSpawn.list[rand.Next(0, max)];
		}
	}
}
