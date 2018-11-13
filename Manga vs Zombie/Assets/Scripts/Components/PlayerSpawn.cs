using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class PlayerSpawn : Photon.PunBehaviour {

	public List<GameObject> list;
	public bool isActived;
	object[] data = new object[2];
	GameObject player;

	public override void OnJoinedRoom()
	{
		SpawnPlayer();
	}

	public void SpawnPlayer()
	{
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
		}
	}	

	void ChangePosition()
	{
		player.transform.position = transform.position;
	}
}
