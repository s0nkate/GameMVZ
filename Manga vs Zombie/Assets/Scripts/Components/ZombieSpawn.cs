using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : Photon.PunBehaviour 
{

	public  float timeDelay;
	public List<GameObject> list;
	public bool isActived = false;

	public void Start()
	{
	}

	public void SetTimeDelay(float time)
	{
		timeDelay = time;
	}

	void Update()
	{
		
		if(!GameManager.Instance.isPlaying)
		{
			isActived = false;
		}
		if(!isActived && PhotonNetwork.player.IsMasterClient && GameManager.Instance.isPlaying)
		{
			Debug.Log("SpawnZombie");
			photonView.RPC("DisableAllZombie", PhotonTargets.All);
			StartSpawn();
			isActived = true;
		}
	}

	public void StartSpawn()
	{
		StartCoroutine(Addzombie());
	}

	IEnumerator Addzombie()
	{
		while(GameManager.Instance.isPlaying)
		{
			photonView.RPC("GetZombie", PhotonTargets.All);
			yield return new WaitForSeconds(timeDelay);
		}
	}
}
