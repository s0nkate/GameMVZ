using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType
{
		Idle,
		Left,
		Right,
		Skill1,
		Skill2,
		Item1,
		Item2
}
public class PlayerBehaviour : Photon.PunBehaviour 
{
	

	public BehaviourType behaviour;

	public void AttackLeft()
	{
		behaviour = BehaviourType.Left;
	}

	public void AttackRight()
	{
		behaviour = BehaviourType.Right;
	}

	public void UseSkill1()
	{
		behaviour = BehaviourType.Skill1;
	}

	public void UseSkill2()
	{
		behaviour = BehaviourType.Skill2;
	}
	public void UseItem1()
	{
		behaviour = BehaviourType.Item1;
	}

	public void UseItem2()
	{
		behaviour = BehaviourType.Item2;
	}
	void Update () 
	{
		
	}
	public void SetIdle()
	{
		behaviour = BehaviourType.Idle;
	}
}
