using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimationEnd : Photon.PunBehaviour 
{
	void Awake()
	{
		
	}
	public void Destroy()
	{
		// Destroy(transform.gameObject);
		gameObject.SetActive(false);
		// PhotonView photonView = GetComponent<PhotonView>();
		// photonView.RPC("DeactiveZombie", PhotonTargets.AllBuffered);
		// if(PhotonNetwork.player.IsMasterClient)
		// {
			
		// }
		
	}

	public void DestroyMysefl()
	{
		gameObject.SetActive(false);
		// PhotonView photonView = GetComponent<PhotonView>();
		// photonView.RPC("DeactiveZombie", PhotonTargets.AllBuffered);
		// if(PhotonNetwork.player.IsMasterClient)
		// {
			
		// }
		
	}

	[PunRPC]
	void DeactiveZombie()
	{
		gameObject.SetActive(false);
	}
}
