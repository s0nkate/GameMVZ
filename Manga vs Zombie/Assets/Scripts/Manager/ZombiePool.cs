using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

[RequireComponent(typeof(ZombieSpawn))]
public class ZombiePool : Photon.MonoBehaviour 
{

	public List<GameObject> zombiePool;
	ZombieSpawn zombieSpawn;
	Transform spawnTransform;
	public int zombieCount = 6;
	// public PhotonView photonView;
	void Start()
	{
		zombieSpawn = GetComponent<ZombieSpawn>();
		spawnTransform = zombieSpawn.transform;
		// zombiePool = new List<GameObject>();
		// photonView = GetComponent<PhotonView>();
		// if(PhotonNetwork.isMasterClient)
		// {
		// 	for (int i = 0; i < zombieCount; i++) 
		// 	{
		// 		photonView.RPC("CreateZombie", PhotonTargets.AllBuffered);
		// 	}
		// }
	}

	public void ActiveZombie()
	{
		photonView.RPC("GetZombie", PhotonTargets.AllBuffered);
	}


	[PunRPC]
	public void GetZombie()
	{
		for(int i = 0; i < zombieCount; i++)
		{
			if(!zombiePool[i].activeInHierarchy)
			{
				Heath heath = zombiePool[i].GetComponent<Heath>();
				Faction faction = zombiePool[i].GetComponent<Faction>();

				faction.currentState = State.Walk;
				heath.value = heath.maxValue;
				zombiePool[i].transform.position = spawnTransform.position;
				zombiePool[i].SetActive(true);
				return;	
			}
		}
	}

	[PunRPC]
	void CreateZombie()
	{
			// GameObject	zombie = Instantiate(GetRandomZombiePrefab(zombieSpawn), transform.position, spawnTransform.localRotation, transform);
			// string name = "Prefabs/Zombie/" + GetRandomZombiePrefabname(zombieSpawn);
			GameObject	zombie = Instantiate(GetRandomZombiePrefab(zombieSpawn) , transform.position, spawnTransform.localRotation, spawnTransform);
			//Nếu zombie từ phải sang thì xoay thanh máu theo trục y 180 độ
			if(!spawnTransform.localRotation.Equals(new Vector3(0, 0, 0)))
			{
				zombie.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
			}
			zombie.SetActive(false);
			zombiePool.Add(zombie);
	}

	string GetRandomZombiePrefabname(ZombieSpawn zombieSpawn)
	{
		int max = zombieSpawn.list.Count;
		return zombieSpawn.list[Random.Range(0, max)].name;
	}

	GameObject GetRandomZombiePrefab(ZombieSpawn zombieSpawn)
	{
		int max = zombieSpawn.list.Count;
		return zombieSpawn.list[Random.Range(0, max)];
	}
	public void SetPoolSize(int size)
	{
		zombieCount = size;
	}
}
