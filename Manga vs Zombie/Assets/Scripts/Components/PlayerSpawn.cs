using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : Photon.PunBehaviour {

	public List<GameObject> list;
	public bool isActived;
	public bool isInstance;
	// public PhotonView photonView;
	void Awake()
	{
		isInstance = false;
		// photonView = GetComponent<PhotonView>();
		
		// PhotonNetwork.isMessageQueueRunning = true;
		// Object[] data = new Object[1];
		// data[0] = this.transform;
		// GameObject player = PhotonNetwork.Instantiate("Prefabs/Player/Player", transform.position, transform.localRotation, 0);
	}

	public override void OnJoinedRoom()
	{
		// if(NetworkManager.Instance.isInRoom && !isInstance)
		// {
		// 	photonView.RPC("SpawnPlayer", PhotonTargets.AllBuffered);
		// 	isInstance = true;
		// }
		GameObject player = PhotonNetwork.Instantiate("Prefabs/Player/Player", transform.position, transform.localRotation, 0) as GameObject;
		
	}

	[PunRPC]
	void SpawnPlayer()
	{
		GameObject player = Instantiate(Resources.Load("Prefabs/Player/Player"), transform.position, transform.localRotation, transform) as GameObject;
	}

	

	
}
