using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Photon.PunBehaviour 
{
	object[] content = new object[] { new Vector3(10.0f, 2.0f, 5.0f), 1, 2, 5, 10 };
	bool reliable = true;
	RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void AcceptJoinRoom()
	{
		PhotonNetwork.RaiseEvent(NetworkManager.AcceptJoinRoom, content, reliable, raiseEventOptions);
		gameObject.SetActive(false);
	}


	public void CancelJoinRoom()
	{
		PhotonNetwork.RaiseEvent(NetworkManager.CancelJoinRoom, content, reliable, raiseEventOptions);
		gameObject.SetActive(false);
	}
}
