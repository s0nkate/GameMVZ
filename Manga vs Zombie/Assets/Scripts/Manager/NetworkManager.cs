using System.Collections;
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
	public bool isInLobby;
	public bool isInRoom;	
	bool isCancel;
	bool reliable = true;
	RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
	public static NetworkManager Instance = null;
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
		PhotonNetwork.ConnectUsingSettings(version);
		DontDestroyOnLoad (gameObject);
	}



	public void ConnectAndJoin () 
	{
		loadingText.text = "Connecting to server....";
		PhotonNetwork.JoinLobby ();

	}
	
	void Update () 
	{
		
	}

	void CreateRoom()
	{
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxNumberPlayerInARoom }, null);
	}



	public override void OnConnectedToPhoton()
	{
		Debug.Log("OnConnectedToPhoton");
		loadingText.text = "Connected to server";

	}
	// public override void OnOwnershipRequest(object[] viewAndPlayer)
	// {
	// 	PhotonView view = viewAndPlayer[0] as PhotonView;
    //  	PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;
	// 	photonView.TransferOwnership(requestingPlayer.ID);
	// }

	public override void OnJoinedLobby ()
	{
		Debug.Log("OnJoinedLobby");
		isInLobby = true;
		GameManager.Instance.playScene.SetActive(true);
		if(isCancel)
		{
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
	// public override void OnPhotonPlayerConnected(PhotonPlayer player)
    // {
    //     Debug.Log("OnPhotonPlayerConnected: " + player);
    // }


	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		isInRoom = true;
		if(!isOwn)
		{
			PhotonNetwork.RaiseEvent(RequestJoinRoom, PhotonNetwork.player.ID, reliable, raiseEventOptions);
			loadingText.text = "Waiting accept....";
		}
		else
		{
			
			loadingText.text = "Joined Room";
			// Debug.Log("acctive player1");
			GameManager.Instance.PlayGame();
			// PlayerManager.Instance.SetPlayer(GameManager.Instance.instancePlayer[0]);
			
			
		}
			
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
					// Debug.Log("acctive player2");
					// PlayerManager.Instance.SetPlayer(GameManager.Instance.instancePlayer[1]);
					// Debug.Log("acctive player1");
					// GameManager.Instance.instancePlayer[0].SetActive(true);
										
				}
				break;				
			case CancelJoinRoom:
				if(playerRequestId == PhotonNetwork.player.ID)
				{
					// PhotonNetwork.JoinRandomRoom();
					isCancel = true;
					PhotonNetwork.LeaveRoom();
				}
				break;
			default:
				break;
		}
	}

	public override void OnLeftRoom ()
	{
		Debug.Log("OnLeftRoom");
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{
		Debug.Log("OnPhotonRandomJoinFailed");
		loadingText.text = "No Room found!";
		isOwn = true;
		CreateRoom();
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
	}

	public override void OnConnectedToMaster()
 	{
    	Debug.Log( "OnConnectedToMaster()" ); 
		if(isCancel)
		{
			CreateRoom ();
		}
 	}
	
}
