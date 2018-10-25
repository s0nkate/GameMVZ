using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : Photon.MonoBehaviour {

	public List<GameObject> list;
	public bool isActived;
	// public PhotonView photonView;
	void Awake()
	{
		// photonView = GetComponent<PhotonView>();
		photonView.RPC("SpawnPlayer", PhotonTargets.AllBuffered);
		// PhotonNetwork.isMessageQueueRunning = true;
		// Object[] data = new Object[1];
		// data[0] = this.transform;
		// GameObject player = PhotonNetwork.Instantiate("Prefabs/Player/Player", transform.position, transform.localRotation, 0);
	}

	[PunRPC]
	void SpawnPlayer()
	{
		GameObject player = Instantiate(Resources.Load("Prefabs/Player/Player"), transform.position, transform.localRotation, transform) as GameObject;
	}

	

	
}
