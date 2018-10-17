using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimationEnd : Photon.PunBehaviour {

	public void Destroy()
	{
		// Destroy(transform.gameObject);
		// gameObject.SetActive(false);
		photonView.RPC("DeactiveZombie", PhotonTargets.AllBuffered);
	}

	public void DestroyMysefl()
	{
		// Destroy(transform.gameObject);
		photonView.RPC("DeactiveZombie", PhotonTargets.AllBuffered);
		
	}

	[PunRPC]
	void DeactiveZombie()
	{
		gameObject.SetActive(false);
	}
}
