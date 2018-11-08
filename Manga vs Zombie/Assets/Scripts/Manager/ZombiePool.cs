using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
using UnityEngine.Events;


[RequireComponent(typeof(ZombieSpawn))]
public class ZombiePool : Photon.PunBehaviour 
{

	public Queue<GameObject> zombieQueue;
    public List<GameObject> zombiePool;
    public InventorySceneList inventorySceneList; 
	ZombieSpawn zombieSpawn;
	Transform spawnTransform;
	public int zombieCount = 6;
	public static UnityEvent onNextLevel;
    public InventoryEnemyList inventoryEnemyList;

	void Awake()
	{
		zombieSpawn = GetComponent<ZombieSpawn>();
        zombieQueue = new Queue<GameObject>();
        spawnTransform = zombieSpawn.transform;
		if (onNextLevel == null)
		{
            onNextLevel = new UnityEvent();
			
		}
		onNextLevel.AddListener(LoadLevel);
    }

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom by Zombiepool");
        
        onNextLevel.Invoke();
        zombieSpawn.isActived = true;
    }



	void LoadLevel()
	{
		float time = inventorySceneList.scenelist[GameManager.Instance.i].DelayEnemy;
		zombieSpawn.SetTimeDelay(time);
		photonView.RPC("DisableAllZombie", PhotonTargets.AllBuffered);
		Debug.Log("loadlevel");
	}

	public void ActiveZombie()
	{
		photonView.RPC("GetZombie", PhotonTargets.AllBuffered);
	}

    private void FixedUpdate()
    {
        AddToQueue();
    }

    [PunRPC]
    void AddDataToZombie(int viewID, int index)
    {

        GameObject zombie = PhotonView.Find(viewID).gameObject;
        zombie.GetComponent<Attack>().damage = inventoryEnemyList.enemyList[index].damage;
        zombie.GetComponent<Attack>().timeDelay = inventoryEnemyList.enemyList[index].Delay;
        zombie.GetComponent<Heath>().maxValue = inventoryEnemyList.enemyList[index].health;
        zombie.GetComponent<Heath>().value = inventoryEnemyList.enemyList[index].health;
        zombie.GetComponent<Move>().speed = inventoryEnemyList.enemyList[index].speed;
        zombie.GetComponent<Zombie>().score = inventoryEnemyList.enemyList[index].score;
        zombie.GetComponent<Zombie>().money = inventoryEnemyList.enemyList[index].money;
        zombie.GetComponent<Zombie>().type = inventoryEnemyList.enemyList[index].type;
    }

    void AddToQueue()
    {
        foreach (var zombie in zombiePool)
        {
            if (!zombie.activeInHierarchy)
            {

                zombieQueue.Enqueue(zombie);
            }
        }
    }

    [PunRPC]
	public void DisableAllZombie()
	{
        zombieQueue.Clear();
        for (int i = 0; i < zombieCount; i++)
        {
            if (zombiePool[i].activeInHierarchy)
            {
                zombiePool[i].SetActive(false);
            }
        }
    }


	[PunRPC]
	public void GetZombie()
	{
        AddToQueue();
        GameObject zombie = zombieQueue.Dequeue();
        zombie.SetActive(true);
        
        zombie.GetComponent<Zombie>().UpdateZombieDataRPC();        

        Heath heath = zombie.GetComponent<Heath>();
        Faction faction = zombie.GetComponent<Faction>();
        heath.value = heath.maxValue;
        heath.isDead = false;
        zombie.transform.position = spawnTransform.position;
        faction.currentState = State.Walk;
        
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
