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
        public int id;
		public PlayerBehaviour playerBehaviour;
		public Player1Controller playerController;

		void Start()
		{
			playerController = GetComponent<Player1Controller>();
			id = (int)photonView.instantiationData[1];
		}
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
		{			
			if (stream.isWriting)
        	{
				bool isActive = gameObject.activeSelf;
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				int state = 0;
				if(playerBehaviour != null)
				{
					state = (int)playerBehaviour.behaviour;
				}
				stream.Serialize(ref isActive);
				stream.Serialize(ref position);
				stream.Serialize(ref rotation);
				stream.Serialize(ref state);

      	  	}
      	 	else
      		{
				bool isActive = gameObject.activeSelf;
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				int state = 0;

				stream.Serialize(ref isActive);
				stream.Serialize(ref position);
				stream.Serialize(ref rotation);
				stream.Serialize(ref state);

                if(!photonView.isMine)
				{
					gameObject.SetActive(isActive);

					if(playerBehaviour != null)
					{
						playerBehaviour.SetBehaviour(state);
					}
					switch (state) 
					{
						case (int)BehaviourType.Left:
							playerController.AttackLeft ();
							break;
						case (int)BehaviourType.Right:
							playerController.AttackRight ();
							break;
						case (int)BehaviourType.Skill1:
							playerController.Skill1 ();
							break;
						case (int)BehaviourType.Skill2:
							playerController.Skill2 ();
							break;
						case (int)BehaviourType.Item1:
							playerController.UseItem1();
							break;
						case (int)BehaviourType.Item2:
                            playerController.UseItem2();
                            break;
						default:
							break;
					}
					transform.position = position;
					transform.rotation = rotation;
				}
       		}
		}
	}
}
