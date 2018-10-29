using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimationEnd : Photon.PunBehaviour {
	
	void Awake()
	{
		// photonView = GetComponent<PhotonView>();
	}
	public void Destroy()
	{
		// Destroy(transform.gameObject);
		gameObject.SetActive(false);
		// photonView.RPC("DeactiveZombie", PhotonTargets.AllBuffered);
	}

	public void DestroyMysefl()
	{
		gameObject.SetActive(false);
		// Destroy(transform.gameObject);
		// photonView.RPC("DeactiveZombie", PhotonTargets.AllBuffered);
		
	}

	[PunRPC]
	void DeactiveZombie()
	{
		gameObject.SetActive(false);
	}
}
