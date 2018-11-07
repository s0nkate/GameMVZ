using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	
	public class TimeSync : Photon.MonoBehaviour, IPunObservable
	{	
		public int time;
		
		void Start()
		{
		}
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
		{			
			if (stream.isWriting)
        	{

				int time = GameManager.Instance.time;

				stream.Serialize(ref time);

      	  	}
      	 	else
      		{
				stream.Serialize(ref time);

                if(!photonView.isMine)
				{
					GameManager.Instance.time = time;
				}
       		}
		}
	}
}
