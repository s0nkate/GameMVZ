using System.Collections;
using System.Collections.Generic;
using ECSComponent;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECSSystem {
	public class AttackSystem : ComponentSystem {
		struct Data {
			public Faction faction;
			public Attack attack;
			public Animator animator;
		}

		protected override void OnUpdate () {
			foreach (var e in GetEntities<Data> ()) {
				if (e.faction.currentState == State.Attack) {
					ChangeToAttack (e);
				}
			}
		}

		void ChangeToAttack (Data data) {
			if (data.attack.isAttack) {
				data.animator.SetInteger ("stage", (int) State.Attack);
				if (data.faction.value == FactionType.Player) {
					data.attack.isAttack = false;
					data.faction.currentState = State.Idle;
				}

			}
		}

		void TakeDamage (Data data) {
			foreach (var item in data.attack.target) {
				if (data.faction.currentState == State.Dead) {
					data.attack.target.Remove (item);
					continue;
				}
				if (item != null) {
					item.TakeDamage (data.attack.damage);
				}
			}
		}

	}
}