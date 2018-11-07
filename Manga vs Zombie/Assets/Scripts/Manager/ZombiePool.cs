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

	// public PhotonView photonView;
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

        
        //LoadLevel();
       
        // zombiePool = new List<GameObject>();

        // StartCoroutine("CreateZombiePool");		
    }

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom by Zombiepool");
        
        onNextLevel.Invoke();
        zombieSpawn.isActived = true;
        // if(PhotonNetwork.isMasterClient)
        // {
        // 	for (int i = 0; i < zombieCount; i++) 
        // 	{
        // 		photonView.RPC("CreateZombie", PhotonTargets.AllBuffered);
        // 	}
        // 	zombieSpawn.isActived = false;
        // }
    }



	void LoadLevel()
	{
		float time = inventorySceneList.scenelist[GameManager.Instance.i].DelayEnemy;
		zombieSpawn.SetTimeDelay(time);
		photonView.RPC("DisableAllZombie",PhotonTargets.AllBuffered);
		Debug.Log("loadlevel");
	}

	public void ActiveZombie()
	{
		photonView.RPC("GetZombie", PhotonTargets.AllBuffered);
	}

    private void Update()
    {
        //AddToQueue();
    }

    [PunRPC]
    void AddDataToZombie(GameObject zombie, int index)
    {
        zombie.GetComponent<Attack>().damage = inventoryEnemyList.enemyList[index].damage;
        zombie.GetComponent<Attack>().timeDelay = inventoryEnemyList.enemyList[index].Delay;
        zombie.GetComponent<Heath>().maxValue = inventoryEnemyList.enemyList[index].health;
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
        //for(int i = 0; i < zombieCount; i++)
        //{
        //if(!zombiePool[i].activeInHierarchy)
        //{
        //	Heath heath = zombiePool[i].GetComponent<Heath>();
        //	Faction faction = zombiePool[i].GetComponent<Faction>();
        //             heath.value = heath.maxValue;
        //             heath.isDead = false;

        //	zombiePool[i].transform.position = spawnTransform.position;
        //	zombiePool[i].SetActive(true);
        //             faction.currentState = State.Walk;
        //             return;	
        //}
        //}
        AddToQueue();
        GameObject zombie = zombieQueue.Dequeue();
        Heath heath = zombie.GetComponent<Heath>();
        Faction faction = zombie.GetComponent<Faction>();
        heath.value = heath.maxValue;
        heath.isDead = false;

        zombie.transform.position = spawnTransform.position;
        zombie.SetActive(true);
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
