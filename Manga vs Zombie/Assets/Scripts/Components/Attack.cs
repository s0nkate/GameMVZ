using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

namespace ECSComponent
{
	public class Attack : MonoBehaviour 
	{
		public int damage;
		public float timeDelay;
		public bool isAttack;
		public Heath target;

		void OnCollisionEnter2D(Collision2D collision)
		{
			if(collision.transform.CompareTag("house"))
			{
				target = collision.transform.gameObject.GetComponent<Heath>();
				Zoombie zoombie = gameObject.GetComponent<Zoombie>();
				if(zoombie != null)
				{
					zoombie.currentState = State.Attack;
				}
				isAttack = true;
			}
		}

	}
}

