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
            heathNetwork = (int)stream.ReceiveNext();
            heath.value = heathNetwork;
        }
    }


	void Awake () 
	{
		heath = gameObject.GetComponent<Heath>();
	}

    void Update()
    {
        // if(!photonView.isMine)
        // {
        //     heath.value = heathNetwork;
        // }
    }
	
}
