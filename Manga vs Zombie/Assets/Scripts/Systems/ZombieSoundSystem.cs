using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;

namespace ECSSystem
{
	public class ZombieSoundSystem : ComponentSystem 
	{
		struct Data
		{
			public Faction faction;
			public ZombieSound zombieSound;
			public AudioSource audioSource;
		}

		protected override void OnUpdate()
		{
			if(SoundManager.Instance.effectSoundActive)
			{
				foreach (var entity in GetEntities<Data>())
				{
					if(entity.audioSource.isPlaying)
					{
						continue;
					}

					switch (entity.faction.currentState)
					{
						case State.Walk :
							entity.audioSource.volume = SoundManager.Instance.volume;
							entity.audioSource.clip = entity.zombieSound.walkSound;
							entity.audioSource.Play();
							break;
						case State.Attack :
							entity.audioSource.volume = SoundManager.Instance.volume;
							entity.audioSource.clip = entity.zombieSound.attackSound;
							entity.audioSource.Play();
							break;
						case State.Dead :
							entity.audioSource.volume = SoundManager.Instance.volume;
							entity.audioSource.clip = entity.zombieSound.deadSound;
							entity.audioSource.Play();
							break;
						default:
							break;
					}
				}
			}
			
		}
	
	}
}

