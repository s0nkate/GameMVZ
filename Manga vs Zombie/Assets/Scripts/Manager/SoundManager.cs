using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour 
{

	public Toggle  music;
	public Toggle  soundEffect;
	public Slider volumeSilder;
	public bool musicSoundActive;
	public bool effectSoundActive;
	public float volume;
	public static SoundManager Instance = null;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		volumeSilder.maxValue = 1f;
		volumeSilder.value = volume;
	}

	public void Update()
	{
		musicSoundActive = music.isOn;
		effectSoundActive = soundEffect.isOn;
		volume = volumeSilder.value;
	}

}
