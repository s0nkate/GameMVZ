using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace ECSComponent
{
public enum EffectType : int 
{
	None = -1,
	HeathUp = 0,
	DamageDown = 1,
	HouseDeffent = 2
}

public class Effect : MonoBehaviour 
{

	public const int hpUpValue = 100;
	public const int damageDownValue =10;
	public const float timeEffect = 5;
	public float t = 0;
	Heath heath;
	Attack attack;
	Zombie zombie;

	void Awake()
	{
		heath = GetComponent<Heath>();
		attack = GetComponent<Attack>();
		zombie = GetComponent<Zombie>();
	}

	[PunRPC]
	void HPUp()
	{
		if(heath == null)
			return;
		int newHP = heath.value + hpUpValue;
		heath.value = newHP > heath.maxValue ? heath.maxValue : newHP;	
		GameManager.Instance.effectType = EffectType.None;
	}

	[PunRPC]
	void DamageDown()
	{	
		if(attack == null)
			return;
		attack.damage = Math.Abs(zombie.tempDamage - damageDownValue);
		CleanEffect();
	}

	[PunRPC]
	void HouseDeffent()
	{
		if(attack == null)
			return;
		attack.damage = 0;		
		CleanEffect();
	}

	void CleanEffect()
	{
		if(t < timeEffect)
		{	
			t += Time.deltaTime;
		}
		else
		{	
			attack.damage = zombie.tempDamage;
		}
		t = 0;
		GameManager.Instance.effectType = EffectType.None;		
	}
}
}