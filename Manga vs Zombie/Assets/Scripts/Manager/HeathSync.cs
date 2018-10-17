using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class HeathSync : Photon.MonoBehaviour, IPunObservable
{
	Heath heath;
	int heathNetwork;
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(heath.value);
        }
        else
        {           
            this.heath.value = (int)stream.ReceiveNext();
        }
    }


	void Start () 
	{
		heath = GetComponent<Heath>();
	}
	
}
