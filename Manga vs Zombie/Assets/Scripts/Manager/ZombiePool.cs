using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

[RequireComponent(typeof(ZombieSpawn))]
public class ZombiePool : Photon.MonoBehaviour 
{

	// public static ZombiePool Instance = null;
	public List<GameObject> zombiePool;
	ZombieSpawn zombieSpawn;
	Transform spawnTransform;
	public int zombieCount = 20;
	void Start()
	{
		zombieSpawn = GetComponent<ZombieSpawn>();
		spawnTransform = zombieSpawn.transform;
		zombiePool = new List<GameObject>();
		
		for (int i = 0; i < zombieCount; i++) 
		{
  			GameObject zombie = (GameObject) CreateZombie();
			if(zombie != null)
			{
				zombie.SetActive(false); 
  				zombiePool.Add(zombie);
			}
		}
	}

	[PunRPC]
	void ActiveZombie(int i)
	{
		zombiePool[i].SetActive(true);
	}

	public GameObject GetZombie()
	{
		for(int i = 0; i < zombieCount; i++)
		{
			if(!zombiePool[i].activeInHierarchy)
			{
				// zombiePool[i].SetActive(true);
				Heath heath = zombiePool[i].GetComponent<Heath>();
				Faction faction = zombiePool[i].GetComponent<Faction>();

				faction.currentState = State.Walk;
				heath.value = heath.maxValue;
				// zombiePool[i].transform.parent = spawnTransform;
				zombiePool[i].transform.position = spawnTransform.position;
				// zombiePool[i].SetActive(true);
				photonView.RPC("ActiveZombie", PhotonTargets.AllBuffered, i);
				return zombiePool[i];
			}
		}

		return null;
	}

	GameObject CreateZombie()
	{
			// GameObject	zombie = Instantiate(GetRandomZombiePrefab(zombieSpawn), transform.position, spawnTransform.localRotation, transform);
			string name = "Prefabs/Zombie/" + GetRandomZombiePrefabname(zombieSpawn);
			GameObject	zombie = PhotonNetwork.Instantiate(name , transform.position, spawnTransform.localRotation, 0);
			zombie.transform.parent = spawnTransform;
			//Nếu zombie từ phải sang thì xoay thanh máu theo trục y 180 độ
			if(!spawnTransform.localRotation.Equals(new Vector3(0, 0, 0)))
			{
				zombie.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
			}
			zombie.SetActive(false);
			return zombie;
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
