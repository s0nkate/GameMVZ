using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class HeathSync : Photon.PunBehaviour
{
	Heath heath;
    int heathNetwork = 0;
    int idNetwork;
    public PhotonView photonView;
    static float t = 0.0f;
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        stream.Serialize(ref heathNetwork);
        stream.Serialize(ref idNetwork);
        
        // if (stream.isWriting)
        // {
        //     int value = heath.value;
        //     int id = heath.idAttack;
        //     stream.Serialize(ref value);
        //     stream.Serialize(ref id);

        // }
        // else
        // {         
        //     heathNetwork = 0;
        //     idNetwork = 0;
        //     stream.Serialize(ref heathNetwork);
        //     stream.Serialize(ref idNetwork);
        //     heath.value = heathNetwork;
        //     heath.idAttack = idNetwork;

        // }
    }


	void Awake () 
	{
		heath = gameObject.GetComponent<Heath>();
        photonView = gameObject.GetComponent<PhotonView>();
        heathNetwork = heath.maxValue;
	}

    void Update()
    {
        if(!photonView.isMine)
        {
            heath.value = heathNetwork;
            heath.idAttack = idNetwork;
            // heath.value = Mathf.Lerp(heath.value, heathNetwork, t);
            // t += 0.5f * Time.deltaTime;
        }
        else
        {
            heathNetwork = heath.value;
            idNetwork = heath.idAttack;
        }
    }
	
}
