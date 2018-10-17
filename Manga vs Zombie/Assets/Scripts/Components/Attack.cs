using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECSComponent;

namespace ECSComponent
{
	[RequireComponent(typeof(Faction))]
	public class Attack : MonoBehaviour 
	{
		public int damage;
		public float timeDelay;
		public bool isAttack;
		public List<Heath> target;
		private float time = 0;

		void OnCollisionStay2D(Collision2D collision)
		{
			if(collision.transform.CompareTag("house"))
			{
				Heath heath = collision.transform.gameObject.GetComponent<Heath>();
				Faction faction = gameObject.GetComponent<Faction>();
				faction.currentState = State.Attack;
				isAttack = true;
				if(time >= timeDelay)
				{
					heath.TakeDamage(damage);
					time = 0;
				}
				else
				{
					time += Time.deltaTime;
				}				
			}

			// if(collision.transform.CompareTag("Enemy"))
			// {
			// 	Debug.Log("Player attack");
			// 	Heath heath = collision.transform.gameObject.GetComponent<Heath>();
			// 	Faction faction = gameObject.GetComponent<Faction>();
			// 	if(faction.value == FactionType.Player)
			// 	{
			// 		target.Add(heath);
			// 		faction.currentState = State.Attack;
			// 	}
			// 	isAttack = true;
			// }


		}

	}
}

