using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class HeathSync : Photon.MonoBehaviour, IPunObservable
{
	Heath heath;
    int heathNetwork;
    static float t = 0.0f;
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(heath.value);
        }
        else
        {           
            heath.value = (int)stream.ReceiveNext();
        }
    }


	void Awake () 
	{
		heath = gameObject.GetComponent<Heath>();
        heathNetwork = heath.value;
	}

    // void Update()
    // {
    //     if(!photonView.isMine)
    //     {
            // heath.value = heathNetwork;
            // // heath.value = Mathf.Lerp(heath.value, heathNetwork, t);
            // t += 0.5f * Time.deltaTime;
    //     }
    // }
	
}
