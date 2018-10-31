using System.Collections;
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

				bool isActive = gameObject.activeSelf;
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				PlayerBehaviour playerBehaviour = GetComponent<Player1Controller>().playerBehaviour;
				int state = (int)playerBehaviour.behaviour;


				stream.Serialize(ref isActive);
				stream.Serialize(ref position);
				stream.Serialize(ref rotation);
				stream.Serialize(ref state);
				Debug.Log("ME: " + state);

				Debug.Log("send");
      	  	}
      	 	else
      		{

				bool isActive = gameObject.activeSelf;
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				PlayerBehaviour playerBehaviour = GetComponent<Player1Controller>().playerBehaviour;
				int state = 0;

				stream.Serialize(ref isActive);
				stream.Serialize(ref position);
				stream.Serialize(ref rotation);
				stream.Serialize(ref state);
				Debug.Log("YOU: " + state);
		
				gameObject.SetActive(isActive);
				transform.position = position;
				transform.rotation = rotation;
				playerBehaviour.SetBehaviour(state);
       		}
		}


	}

	
}
