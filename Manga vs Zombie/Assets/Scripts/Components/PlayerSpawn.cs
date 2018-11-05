using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class PlayerSpawn : Photon.PunBehaviour {

	public List<GameObject> list;
	public bool isActived;
	public bool isInstance;
	object[] index = new object[1];
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
		
		index[0] = GameManager.Instance.GetSelectedPlayer().index;
		GameObject player = PhotonNetwork.Instantiate("Prefabs/Player/Player", transform.position, transform.localRotation, 0, index) as GameObject;
        player.GetComponent<Player>().id = PhotonNetwork.player.ID;
        if(PhotonNetwork.playerList.Length > 1)
		{
			player.transform.position += new Vector3(0.3f, 0, 0);
		}
	}

	[PunRPC]
	void SpawnPlayer()
	{
		GameObject player = Instantiate(Resources.Load("Prefabs/Player/Player"), transform.position, transform.localRotation, transform) as GameObject;
		player.GetComponent<Player>().id = PhotonNetwork.player.ID;
	}

	

	
}
