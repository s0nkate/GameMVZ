using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum BehaviourType : int
{
		Idle = 0,
		Left = 1, 
		Right = 2,
		Skill1 = 3,
		Skill2 = 4,
		Item1 = 5,
		Item2 = 6
}
public class PlayerBehaviour : Photon.PunBehaviour 
{
	

	public BehaviourType behaviour;

	public void SetBehaviour(int state)
	{
		behaviour = (BehaviourType)state;
	}

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
