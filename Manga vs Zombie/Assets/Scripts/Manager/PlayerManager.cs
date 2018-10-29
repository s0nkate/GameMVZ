using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Photon.PunBehaviour {

	public static PlayerManager Instance = null;
	public GameObject myPlayer;
	public GameObject playerBehaviour;
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
		DontDestroyOnLoad (gameObject);
	}
	void Start () {
		// GameObject playerBehaviour = GameObject.FindWithTag("PlayerBehaviour");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPlayer(GameObject player)
	{
		myPlayer = player;
		// myPlayer.GetComponent<Player1Controller>().SetPlayerBehaviour(playerBehaviour);
		myPlayer.SetActive(true);
		// photonView.RPC("ActivePlayer", PhotonTargets.AllBuffered);
	}


	[PunRPC]
	public void ActivePlayer()
	{
		myPlayer.SetActive(true);
		Debug.Log("active player " + myPlayer);
		
	}


}
