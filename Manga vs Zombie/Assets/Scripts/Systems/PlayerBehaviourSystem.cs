using System.Collections;
using System.Collections.Generic;
using ECSComponent;
using Unity.Entities;
using UnityEngine;

namespace ECSSystem 
{
	public class PlayerBehaviourSystem : ComponentSystem 
	{

		struct Data 
		{
			public Player1Controller controller;
			public PhotonView photonView;
		}
		protected override void OnUpdate () 
		{
			foreach (var entity in GetEntities<Data> ()) 
			{
				if (entity.controller.playerBehaviour != null)
				{
					switch (entity.controller.playerBehaviour.behaviour) 
					{
						case BehaviourType.Left:
							Debug.Log("system" + PhotonNetwork.player.ID);
							entity.controller.AttackLeft ();
							break;
						case BehaviourType.Right:
							entity.controller.AttackRight ();
							break;
						case BehaviourType.Skill1:
							entity.controller.Skill1 ();
							break;
						case BehaviourType.Skill2:
							entity.controller.Skill2 ();
							break;
						case BehaviourType.Item1:
							// entity.controller.AttackLeft();
							break;
						case BehaviourType.Item2:
							// entity.controller.AttackLeft();
							break;
						default:
							break;
					}
				}
			}
		}
	}
}