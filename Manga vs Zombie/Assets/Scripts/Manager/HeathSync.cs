using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class HeathSync : Photon.PunBehaviour
{
	Heath heath;
    int heathNetwork = 0;
    int idNetwork;
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        if (stream.isWriting)
        {
            int value = heath.value;
            int id = heath.idAttack;
            stream.Serialize(ref value);
            stream.Serialize(ref id);

        }
        else
        {         
            heathNetwork = 0;
            idNetwork = 0;
            stream.Serialize(ref heathNetwork);
            stream.Serialize(ref idNetwork);
            if(!photonView.isMine)
            {
                heath.value = heathNetwork;
                heath.idAttack = idNetwork;
            }
        }
    }


	void Awake () 
	{
		heath = gameObject.GetComponent<Heath>();
        heathNetwork = heath.maxValue;
	}	
}
