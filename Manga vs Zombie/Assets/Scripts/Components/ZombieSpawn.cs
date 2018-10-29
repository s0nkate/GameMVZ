using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour 
{

	public  float timeDelay;
	public List<GameObject> list;
	public bool isActived;

	public void Start()
	{
		
	}

	public void SetTimeDelay(float time)
	{
		timeDelay = time;
	}
}
