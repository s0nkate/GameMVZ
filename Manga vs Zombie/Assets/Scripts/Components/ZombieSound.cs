using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent
{
	[RequireComponent(typeof(AudioSource))]
	public class ZombieSound : MonoBehaviour 
	{

		public AudioClip walkSound;
		public AudioClip attackSound;
		public AudioClip deadSound;
	}
}
