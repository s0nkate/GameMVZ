using System.Collections;
using System.Collections.Generic;
using ECSComponent;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace ECSSystem {
	public class EffectSystem : ComponentSystem {

		struct HouseData {
			public House house;
			public Effect effect;
			public PhotonView photonView;
		}

		struct ZombieData {
			public Zombie zombie;
			public Effect effect;
			public PhotonView photonView;
		}

		protected override void OnUpdate () {
			switch (GameManager.Instance.effectType) {
				case EffectType.HeathUp:
					HPUp ();
					break;
				case EffectType.HouseDeffent:
					Debug.Log ("HouseDeffent");
					HouseDeffent ();
					break;
				case EffectType.DamageDown:
					Debug.Log ("DamageDown");
					DamageDown ();
					break;
				default:
					break;
			}

		}

		void HPUp () {
			foreach (var entity in GetEntities<HouseData> ()) {
				entity.photonView.RPC ("HPUp", PhotonTargets.AllBuffered);
			}
		}

		void HouseDeffent () {
			foreach (var entity in GetEntities<ZombieData> ()) {
				entity.photonView.RPC ("HouseDeffent", PhotonTargets.AllBuffered);
			}

			foreach (var entity in GetEntities<HouseData> ())

			{
				entity.photonView.RPC ("HouseDeffent", PhotonTargets.AllBuffered);
			}
		}

		void DamageDown () {
			foreach (var entity in GetEntities<ZombieData> ()) {
				entity.photonView.RPC ("DamageDown", PhotonTargets.AllBuffered);
			}
		}

	}
}