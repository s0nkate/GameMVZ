using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class PlayerSpawn : Photon.PunBehaviour {

	public List<GameObject> list;
	public bool isActived;
	public bool isInstance;
	object[] data = new object[2];
	GameObject player;

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
		
		data[0] = GameManager.Instance.GetSelectedPlayer().index;
		data[1] = PhotonNetwork.player.ID;
		Vector3 pos = transform.position;
		if(!PhotonNetwork.player.IsMasterClient)
		{
			pos = transform.position + new Vector3(0.25f, 0, 0);
		}
		player = PhotonNetwork.Instantiate("Prefabs/Player/Player", pos, transform.localRotation, 0, data);
	}

	public override void OnPhotonPlayerDisconnected	(PhotonPlayer leftPlayer)
	{
		if(leftPlayer.ID == NetworkManager.Instance.masterClientID)
		{
			NetworkManager.Instance.UpdateMasterID(PhotonNetwork.player.ID);
			ChangePosition();
			// photonView.RPC("ChangePosition", PhotonTargets.All);
		}
	}	

	// [PunRPC]
	void ChangePosition()
	{
		player.transform.position = transform.position;
	}
}
