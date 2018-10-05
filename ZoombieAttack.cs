using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;
public class ZoombieAttack : MonoBehaviour {

	private Zoombie zoombie;
	private House house;
	void Start()
	{
		zoombie = transform.parent.GetComponent<Zoombie>();
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.transform.CompareTag("house"))
		{
			house = collision.transform.gameObject.GetComponent<House>();
		
			zoombie.currentStage = Zoombie.STAGE_ATTACK;
			if(zoombie.currentStage == Zoombie.STAGE_ATTACK)
			{
				InvokeRepeating("AttackSuccess", 0, zoombie.timeDelay);
			}
			Debug.Log("attack");
		}

		Debug.Log("OnCollisionEnter");
	}

	void AttackSuccess()
	{
		if(house.heath > 0)
		{
			house.TakeDamage(zoombie.damage);
		}
	}
}
