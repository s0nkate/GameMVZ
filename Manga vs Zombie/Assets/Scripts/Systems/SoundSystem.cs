using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;

namespace ECSSystem
{
	public class SoundSystem : ComponentSystem 
	{
		struct EffectData
		{
			public Faction faction;
			public EffectSound effectSound;
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
					entity.audioSource.volume = SoundManager.Instance.volume;
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
				foreach (var entity in GetEntities<EffectData>())
				{
					if(entity.audioSource.isPlaying)
					{
						continue;
					}

					if(entity.faction.value == FactionType.Zombie && entity.faction.currentState == State.Attack)
					{
						PlayAudio(entity.audioSource, entity.effectSound.attackSound);
					}
				}
			}
			else
			{
				foreach (var entity in GetEntities<EffectData>())
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

