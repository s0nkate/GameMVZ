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
          		// stream.SendNext(gameObject.activeSelf);

				// stream.SendNext (transform.position);
				// stream.SendNext (transform.rotation);

				// stream.SendNext(animator.GetBool("Skill1"));
				// stream.SendNext(animator.GetBool("Skill2"));
				// stream.SendNext(animator.GetBool("Attacking"));
				// stream.SendNext(animator.GetBool("Attacking1"));
				bool isActive = gameObject.activeSelf;
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				PlayerBehaviour playerBehaviour = GetComponent<Player1Controller>().playerBehaviour;
				int state = (int)playerBehaviour.behaviour;
				// bool skill1 = animator.GetBool("Skill1");
				// bool skill2 = animator.GetBool("Skill2");
				// bool actack1 = animator.GetBool("Attacking");
				// bool actack2 = animator.GetBool("Attacking1");

				stream.Serialize(ref isActive);
				stream.Serialize(ref position);
				stream.Serialize(ref rotation);
				stream.Serialize(ref state);
				// stream.Serialize(ref skill1);
				// stream.Serialize(ref skill2);
				// stream.Serialize(ref actack1);
				// stream.Serialize(ref actack2);
				Debug.Log("send");
      	  	}
      	 	else
      		{
           		// gameObject.SetActive((bool) stream.ReceiveNext());

				// transform.position = (Vector3)stream.ReceiveNext();
 				// transform.rotation = (Quaternion)stream.ReceiveNext();

				// animator.SetBool("Skill1", (bool) stream.ReceiveNext());
        		// animator.SetBool("Skill2", (bool) stream.ReceiveNext());
        		// animator.SetBool("Attacking", (bool) stream.ReceiveNext());
        		// animator.SetBool("Attacking1", (bool) stream.ReceiveNext());	
				bool isActive = gameObject.activeSelf;
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				PlayerBehaviour playerBehaviour = GetComponent<Player1Controller>().playerBehaviour;
				int state = 0;
				// bool skill1 = animator.GetBool("Skill1");
				// bool skill2 = animator.GetBool("Skill2");
				// bool actack1 = animator.GetBool("Attacking");
				// bool actack2 = animator.GetBool("Attacking1");

				//photonView.RequestOwnership();

				stream.Serialize(ref isActive);
				stream.Serialize(ref position);
				stream.Serialize(ref rotation);
				stream.Serialize(ref state);
				// stream.Serialize(ref skill1);
				// stream.Serialize(ref skill2);
				// stream.Serialize(ref actack1);
				// stream.Serialize(ref actack2);
				// if(!photonView.isMine)
				// {
					gameObject.SetActive(isActive);
					transform.position = position;
					transform.rotation = rotation;
					playerBehaviour.SetBehaviour(state);
					// animator.SetBool("Skill1", (bool) skill1);
        			// animator.SetBool("Skill2", (bool) skill2);
        			// animator.SetBool("Attacking", (bool) actack1);
        			// animator.SetBool("Attacking1", (bool) actack2);
					// Debug.Log("receive " + actack1 + actack2 + skill1 + skill2);
				// }
       		}
		}


	}

	
}
