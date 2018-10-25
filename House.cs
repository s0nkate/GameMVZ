using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zoom;

public class House : MonoBehaviour {

	public int heath;
	public Slider heathSlider;
	public int maxHeath = 100;
	void Start () {
		heathSlider.maxValue = maxHeath;
		heath = maxHeath;
	}
	
	void Update () {
		heathSlider.value = heath;

		if(heath <= 0)
		{
			Dead();	
		}
	}



	public void TakeDamage(int damage)
	{
		heath -= damage;
	}


	//Game Over
	void Dead()
	{
		// Time.timeScale = 0;
	}
}
