using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.PunBehaviour
{

	public string version = "1.0";
	public byte maxNumberPlayerInARoom = 2;
	public const string roomName = "RoomName";
	public RoomInfo[] roomsList;
	public Text loadingText;
	public bool isOwn;
	public const byte RequestJoinRoom = 0;	
	public const byte AcceptJoinRoom = 1;
	public const byte CancelJoinRoom = 2;		
	object[] content = new object[] { new Vector3(10.0f, 2.0f, 5.0f), 1, 2, 5, 10 }; // Array contains the target position and the IDs of the selected units
	bool reliable = true;
	RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

	private int roomCount = 0;
	void Awake()
	{
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.ConnectUsingSettings(version);
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
		roomCount++;
		PhotonNetwork.CreateRoom(roomName + roomCount, new RoomOptions() { MaxPlayers = maxNumberPlayerInARoom }, null);
	}

	public override void OnConnectedToPhoton()
	{
		Debug.Log("OnConnectedToPhoton");
		loadingText.text = "Connected to server";

	}

	

	public override void OnJoinedLobby ()
	{
		Debug.Log("OnJoinedLobby");
		loadingText.text = "Searching a room....";
		PhotonNetwork.JoinRandomRoom();
	}
	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		
		if(!isOwn)
		{
			PhotonNetwork.RaiseEvent(RequestJoinRoom, content, reliable, raiseEventOptions);
			loadingText.text = "Waiting accept....";
		}
		else
		{
			loadingText.text = "Joined Room";
			GameManager.Instance.PlayGame();
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
		switch (eventCode)
		{
			case RequestJoinRoom:
				GameManager.Instance.DisplayRequestPopup();
				break;
			case AcceptJoinRoom:
				loadingText.text = "Joined Room";
				GameManager.Instance.PlayGame();
				break;
			case CancelJoinRoom:
				PhotonNetwork.JoinRandomRoom();
				break;
			default:
				break;
		}
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
	
}
