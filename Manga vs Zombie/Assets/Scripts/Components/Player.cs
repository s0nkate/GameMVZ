﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	
	public class Player : Photon.MonoBehaviour, IPunObservable
	{	
		public int damage;
		public float timeDelay;
		public Animator animator;

		// public void OnPhotonInstantiate(PhotonMessageInfo info)
		// {
		// 	Debug.Log("player instance");
		// 	info.photonView.GetComponent<Transform>().parent = GameObject.FindWithTag("PlayerSpawn").transform;
		// }
		void Start()
		{
		}

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
		{
			if (stream.isWriting)
        	{
          		stream.SendNext(gameObject.activeSelf);

				stream.SendNext (transform.position);
				stream.SendNext (transform.rotation);

				stream.SendNext(animator.GetBool("Skill1"));
				stream.SendNext(animator.GetBool("Skill2"));
				stream.SendNext(animator.GetBool("Attacking"));
				stream.SendNext(animator.GetBool("Attacking1"));
				Debug.Log("send");
      	  	}
      	 	else
      		{
           		gameObject.SetActive((bool) stream.ReceiveNext());

				transform.position = (Vector3)stream.ReceiveNext();
 				transform.rotation = (Quaternion)stream.ReceiveNext();

				animator.SetBool("Skill1", (bool) stream.ReceiveNext());
        		animator.SetBool("Skill2", (bool) stream.ReceiveNext());
        		animator.SetBool("Attacking", (bool) stream.ReceiveNext());
        		animator.SetBool("Attacking1", (bool) stream.ReceiveNext());
				Debug.Log("receive " + gameObject  +  gameObject.activeSelf);
       		}
		}
	}
}