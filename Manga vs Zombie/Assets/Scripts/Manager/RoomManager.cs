using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Photon.PunBehaviour 
{
	bool reliable = true;
	public static int senderId;
	RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void AcceptJoinRoom()
	{
		PhotonNetwork.RaiseEvent(NetworkManager.AcceptJoinRoom, senderId, reliable, raiseEventOptions);
		gameObject.SetActive(false);
	}


	public void CancelJoinRoom()
	{
		PhotonNetwork.RaiseEvent(NetworkManager.CancelJoinRoom, senderId, reliable, raiseEventOptions);
		gameObject.SetActive(false);
	}
}
