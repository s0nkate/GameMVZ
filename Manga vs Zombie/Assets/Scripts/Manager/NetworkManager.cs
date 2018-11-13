﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.PunBehaviour
{
	
	public string version = "1.0";
	public byte maxNumberPlayerInARoom = 2;
	public RoomInfo[] roomsList;
	public Text loadingText;
	public bool isOwn;
	public const byte RequestJoinRoom = 0;	
	public const byte AcceptJoinRoom = 1;
	public const byte CancelJoinRoom = 2;	
	public const byte AvoidJoinRoom = 3;
	public bool isInLobby;
	public bool isInRoom;	
	public bool isCancel;
	bool reliable = true;
	public int masterClientID;
	public Toggle onlineMode;
	RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
	public static NetworkManager Instance = null;
	bool isConnect;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;
		
		DontDestroyOnLoad (gameObject);
	}

	public void Cancel()
	{
		PhotonNetwork.RaiseEvent(NetworkManager.AvoidJoinRoom, PhotonNetwork.player.ID, reliable, raiseEventOptions);
		GameManager.Instance.Cancel();
	}

	public void ConnectAndJoin () 
	{	
		if(onlineMode.isOn == false)
		{
			PhotonNetwork.offlineMode = true;
			PhotonNetwork.CreateRoom("offlineRoom");
			Debug.Log("offline mode");
		}
		else
		{
			PhotonNetwork.ConnectUsingSettings(version);
		}
		GameManager.Instance.playScene.SetActive(true);
        GameManager.Instance.SoundBtn();
		loadingText.text = "Connecting to server....";
		// PhotonNetwork.JoinLobby ();

	}
	
	void Update () 
	{
		
	}

	void CreateRoom()
	{
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxNumberPlayerInARoom }, null);
	}

	public override void OnDisconnectedFromPhoton()
	{
		GameManager.Instance.loading.SetActive(false);
		isConnect = false;
	}


	public override void OnConnectedToPhoton()
	{
		Debug.Log("OnConnectedToPhoton");
		loadingText.text = "Connected to server";
		
		isConnect = true;

	}

	public override void OnJoinedLobby ()
	{
		Debug.Log("OnJoinedLobby");
		isInLobby = true;
		
		if(isCancel)
		{
			isOwn = true;
			CreateRoom();
			isCancel = false;
			return;
		}
		else
		{
			loadingText.text = "Searching a room....";
			PhotonNetwork.JoinRandomRoom();
		}
		
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		isInRoom = true;
		
		if(onlineMode.isOn == false)
		{
			loadingText.text = "Joined Room";
			GameManager.Instance.PlayGame();
			return;
		}

		
		if(!isOwn && !isCancel)
		{
			PhotonNetwork.RaiseEvent(RequestJoinRoom, PhotonNetwork.player.ID, reliable, raiseEventOptions);
			loadingText.text = "Waiting accept....";
		}
		else
		{
			
			loadingText.text = "Joined Room";
			GameManager.Instance.PlayGame();
			UpdateMasterID(PhotonNetwork.player.ID);
		}
	}

	public void UpdateMasterID(int id)
	{
		photonView.RPC("SetMasterID", PhotonTargets.AllBuffered, id);
	}

	[PunRPC]
	public void SetMasterID(int id)
	{
		masterClientID = id;
	}

	public void OnEnable()
	{
   		PhotonNetwork.OnEventCall += OnEvent;
	}

	public void OnDisable()
	{
   		PhotonNetwork.OnEventCall -= OnEvent;
	}

	public void OnEvent(byte eventCode, object content, int senderId)
	{
		RoomManager.senderId = senderId;
		int playerRequestId = (int) content;
		switch (eventCode)
		{
			case RequestJoinRoom:
				if(PhotonNetwork.player.IsMasterClient)
				{
					GameManager.Instance.DisplayRequestPopup();					
				}
				break;	
			case AcceptJoinRoom:
				if(playerRequestId == PhotonNetwork.player.ID)
				{
					loadingText.text = "Joined Room";
					GameManager.Instance.PlayGame();										
				}
				break;				
			case CancelJoinRoom:
				if(playerRequestId == PhotonNetwork.player.ID)
				{
					Debug.Log("cancel");
					PhotonNetwork.LeaveRoom();
					isCancel = true;
					Debug.Log("tat popup");
				}
				break;
			case AvoidJoinRoom:
				
				if(playerRequestId == PhotonNetwork.player.ID)
				{
					PhotonNetwork.Disconnect();
				}
				if(PhotonNetwork.player.IsMasterClient)
				{
					GameManager.Instance.DisableRequestPopup();					
				}
				break;
			default:
				break;
		}
	}

	public override void OnLeftRoom ()
	{
		Debug.Log("OnLeftRoom");
		isOwn = false;
		if(isCancel)
		{
			PhotonNetwork.JoinLobby();
			
		}
		// PhotonNetwork.LeaveLobby();
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{
		Debug.Log("OnPhotonRandomJoinFailed");
		loadingText.text = "No Room found!";
		isOwn = true;
		CreateRoom();
	}

	public override void OnLeftLobby ()
	{
		Debug.Log("OnLeftLobby");
	}

	public override void OnReceivedRoomListUpdate()
	{
  		roomsList = PhotonNetwork.GetRoomList();
		Debug.Log("Room count: " + roomsList.Length);
	}
	public override void OnCreatedRoom ()
	{
		Debug.Log("OnCreatedRoom");
		loadingText.text = "Creating a Room...";		
	}
	public override void OnFailedToConnectToPhoton (DisconnectCause cause)
	{
		Debug.Log("OnFailedToConnectToPhoton");
		loadingText.text = "Failed to connect server,Trying connect againt...";
		PhotonNetwork.ConnectUsingSettings(version);
	}

	public override void OnConnectedToMaster()
 	{
    	Debug.Log( "OnConnectedToMaster()" ); 
		PhotonNetwork.JoinLobby();

	}
}
