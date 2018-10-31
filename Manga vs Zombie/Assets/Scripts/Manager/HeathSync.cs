using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

public class HeathSync : Photon.PunBehaviour
{
	Heath heath;
    int heathNetwork;
    public PhotonView photonView;
    static float t = 0.0f;
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonSerializeView");
        
        if (stream.isWriting)
        {
            int value = heath.value;
            stream.Serialize(ref value);

        }
        else
        {         
            heathNetwork = 0;
            stream.Serialize(ref heathNetwork);
        }
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
            // heath.value = Mathf.Lerp(heath.value, heathNetwork, t);
            t += 0.5f * Time.deltaTime;
        }
    }
	
}
