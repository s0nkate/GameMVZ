using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;

namespace ECSSystem
{
	public class SoundSystem : ComponentSystem 
	{
		struct Data
		{
			public Faction faction;
			public ZombieSound zombieSound;
			public AudioSource audioSource;
		}

		struct MusicData
		{
			public BackgroundMusic background;
			public AudioSource audioSource;
		}

		protected override void OnUpdate()
		{
			CheckMusic();
			CheckEffect();
		}

		void CheckMusic()
		{
			if(SoundManager.Instance.musicSoundActive)
			{
				foreach (var entity in GetEntities<MusicData>())
				{
					if(entity.audioSource.isPlaying)
					{
						continue;
					}
					entity.audioSource.Play();
					
				}
			}
			else
			{
				foreach (var entity in GetEntities<MusicData>())
				{
					if(entity.audioSource.isPlaying)
					{
						entity.audioSource.Stop();
					}
					
				}
			}
		}

		void CheckEffect()
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
							
							break;
						case State.Attack :
							PlayAudio(entity.audioSource, entity.zombieSound.attackSound);
							break;
						case State.Dead :
							
							break;
						default:
							break;
					}
				}
			}
			else
			{
				foreach (var entity in GetEntities<Data>())
				{
					if(entity.audioSource.isPlaying)
					{
						entity.audioSource.Stop();
					}
				}
			}
		}

		void PlayAudio(AudioSource audioSource, AudioClip audio)
		{
			audioSource.volume = SoundManager.Instance.volume;
			audioSource.clip = audio;
			audioSource.Play();
		}
	
	}
}

