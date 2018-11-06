using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	
	public class TimeSync : Photon.MonoBehaviour, IPunObservable
	{	
		public int damage;
		
		void Start()
		{
		}
		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
		{			
			if (stream.isWriting)
        	{

				int state = 0;

				stream.Serialize(ref state);

      	  	}
      	 	else
      		{

				int state = 0;
				stream.Serialize(ref state);

                if(!photonView.isMine)
				{
				
				}
       		}
		}
	}
}
