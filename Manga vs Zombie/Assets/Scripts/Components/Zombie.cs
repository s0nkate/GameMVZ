using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


namespace ECSComponent
{
	public enum ZombieType
	{
			Walker,
			Runner,
			Hulker,
			Exploder
	}
	
	[RequireComponent(typeof(Faction))]
	public class Zombie : Photon.PunBehaviour
    {
		public Transform prefab;
		public int money;
		public int score;
		public ZombieType type;
		public GameObject explosion; 
		public InventoryEnemyList inventoryEnemyList;
		protected AnimatorOverrideController animatorOverrideController;
		protected AnimationClipOverrides clipOverrides;
		public int tempDamage;
		Animator anim;
		
		// public void UpdateZombieDataRPC(int index)
		// {
		// 	int index = Random.Range(0, inventoryEnemyList.enemyList.Count -1);
		// 	photonView.RPC("UpdateZombieData", PhotonTargets.AllBuffered, index);
		// }

		
		public void UpdateZombieData(int index)
		{
			if(index >= inventoryEnemyList.enemyList.Count)
			{
				return;
			}

			money = inventoryEnemyList.enemyList[index].money;
			score = inventoryEnemyList.enemyList[index].score;
			GetComponent<Heath>().maxValue = inventoryEnemyList.enemyList[index].health;
			GetComponent<Heath>().value = inventoryEnemyList.enemyList[index].health;
			GetComponent<Move>().speed = inventoryEnemyList.enemyList[index].speed;
			GetComponent<Attack>().damage = inventoryEnemyList.enemyList[index].damage;
			tempDamage = inventoryEnemyList.enemyList[index].damage;
			GetComponent<Attack>().timeDelay = inventoryEnemyList.enemyList[index].Delay;

			anim = GetComponent<Animator>();

        	animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        	anim.runtimeAnimatorController = animatorOverrideController;
        	clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        	animatorOverrideController.GetOverrides(clipOverrides);
        	clipOverrides["idle"] = inventoryEnemyList.enemyList[index].idle;
        	clipOverrides["walk"] = inventoryEnemyList.enemyList[index].walk;
        	clipOverrides["attack"] = inventoryEnemyList.enemyList[index].attack;
        	clipOverrides["dead"] = inventoryEnemyList.enemyList[index].dead;
        	animatorOverrideController.ApplyOverrides(clipOverrides);

        	GetComponent<Animator>().runtimeAnimatorController = animatorOverrideController;
		}
    }
}
