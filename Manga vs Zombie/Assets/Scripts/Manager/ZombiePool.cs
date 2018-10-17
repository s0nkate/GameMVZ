using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
public class ZombiePool : Photon.MonoBehaviour 
{

	// public static ZombiePool Instance = null;
	public List<GameObject> zombiePool;
	public ZombieSpawn zombieSpawn;
	public Transform spawnTransform;
	// public PhotonView photonView;
	public int zombieCount = 20;

	void Start()
	{
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
				zombiePool[i].transform.parent = spawnTransform;
				zombiePool[i].transform.position = spawnTransform.position;
				return zombiePool[i];
			}
		}

		return null;
	}

	GameObject CreateZombie()
	{
		// GameObject zombieContainer = GameObject.FindWithTag("ZombiePool");

		string name = "Prefabs/Zombie/"+GetRandomZombiePrefabname(zombieSpawn);
		if(photonView.isMine)
		{
			GameObject zombie =  (GameObject)PhotonNetwork.Instantiate(name, spawnTransform.position, spawnTransform.localRotation, 0);
			zombie.transform.parent = this.transform;
			//Nếu zombie từ phải sang thì xoay thanh máu theo trục y 180 độ
			if(!spawnTransform.localRotation.Equals(new Vector3(0, 0, 0)))
			{
				zombie.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
			}
			return zombie;
		}
		return null;
	}

	string GetRandomZombiePrefabname(ZombieSpawn zombieSpawn)
	{
		int max = zombieSpawn.list.Count;
		return zombieSpawn.list[Random.Range(0, max)].name;
	}
	public void SetPoolSize(int size)
	{
		zombieCount = size;
	}
}
