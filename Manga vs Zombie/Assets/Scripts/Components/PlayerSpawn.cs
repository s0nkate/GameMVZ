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
		Vector3 pos = transform.position;
		GameObject player = PhotonNetwork.Instantiate("Prefabs/Player/Player", transform.position, transform.localRotation, 0) as GameObject;
		if(PhotonNetwork.player.IsMasterClient)
		{
			player.transform.position += new Vector3(0.1f,0,0);
		}
		else
		{
			player.transform.position += new Vector3(-0.1f,0,0);
		}
	}

	[PunRPC]
	void SpawnPlayer()
	{
		GameObject player = Instantiate(Resources.Load("Prefabs/Player/Player"), transform.position, transform.localRotation, transform) as GameObject;
	}

	

	
}
