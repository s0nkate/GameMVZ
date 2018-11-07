using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class DeadAnimationEnd : Photon.PunBehaviour 
{
	void Awake()
	{
		
	}
	public void Destroy()
	{
		// Destroy(transform.gameObject);
		gameObject.SetActive(false);
		Zombie zombie = GetComponent<Zombie>();
		Heath heath = GetComponent<Heath>();
		CheckID(zombie.score, zombie.money, heath.idAttack);
		// PhotonView photonView = GetComponent<PhotonView>();
		// photonView.RPC("DeactiveZombie", PhotonTargets.AllBuffered);
		// if(PhotonNetwork.player.IsMasterClient)
		// {
			
		// }
		
	}

	public void DestroyMysefl()
	{
		gameObject.SetActive(false);		
	}

	[PunRPC]
	void DeactiveZombie()
	{
		gameObject.SetActive(false);
		
	}

	private void CheckID(int score,int money,int id)
    {
		Debug.Log("Check Id:"+id + PhotonNetwork.player.ID);
        if (id == PhotonNetwork.player.ID) 
		{ 
            GameManager.Instance.Score += score;
            GameManager.Instance.Gold += money;
        }
        else
        {
        }
    }
}
