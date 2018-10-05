using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimationEnd : MonoBehaviour {

	public void Destroy()
	{
		Destroy(transform.gameObject);
	}

	public void DestroyMysefl()
	{
		Destroy(transform.gameObject);
	}
}
