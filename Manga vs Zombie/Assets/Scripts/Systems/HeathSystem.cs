using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using ECSComponent;
using UnityEngine.UI;

namespace ECSSystem
{
	public class HeathSystem : ComponentSystem 
    
	{
        
		struct Data
		{
			public Heath heath;
		}

		struct HouseData
		{
			public House house;
			public Heath heath;
			public PhotonView photonView;
		}

		struct ZombieData
		{
			public Zombie zoombie;
			public Animator animator;
			public Heath heath;
			public Faction faction;

		}
		protected override void OnUpdate()
		{
			CheckHeath();
			CheckDead();
			CheckHouse();
		}



		void CheckHouse()
		{
			foreach (var e in GetEntities<HouseData>())
			{
				if(e.heath.value <= 0 )
				{
					
					GameManager.Instance.GameOver();
				}
			}
		}

		void CheckHeath()
		{
			foreach (var e in GetEntities<Data>())
			{
				e.heath.heathSlider.maxValue = e.heath.maxValue;
				e.heath.heathSlider.value = e.heath.value;
				if(e.heath.OnInjured == null)
				{
					e.heath.OnInjured += this.OnInjured;
				}

                if (e.heath.CheckID == null)
                {
                    e.heath.CheckID += this.CheckID;
                }
            }
		}
		void CheckDead()
		{
          
            int i = 0;
            foreach (var e in GetEntities<ZombieData>())
			{
                if (e.heath.value <= 0)
                {
                    e.faction.currentState = State.Dead;
                    e.animator.SetInteger("stage", (int)State.Dead);
                }
                if ( e.heath.value <= 0 && !e.heath.isDead)
				{
                    //e.faction.currentState = State.Dead;
                    //e.animator.SetInteger("stage", (int)State.Dead);
                    e.heath.isDead = true;
                    i++;
					
                    e.heath.CheckID(e.zoombie.score,e.zoombie.money ,e.heath.idAttack);
                    
                    
				}
               

            }
            i = 0;
		}

		private void OnInjured(GameObject heath, int damage)
		{
			// heath.value -= damage;
			// PhotonView photonView = heath.GetComponent<PhotonView>();
			Heath hp = heath.GetComponent<Heath>();
			hp.value -= damage;
			// photonView.RPC("IncreaseHeath", PhotonTargets.Others, hp, damage);

		}

		[PunRPC]
		void IncreaseHeath(Heath heath, int damage)
		{
			heath.value -= damage;

		}
        private void CheckID(int score,int money,int id)
        {
            if (id == PhotonNetwork.player.ID) { 
                GameManager.Instance.Score += score;
            	GameManager.Instance.Gold += money;
                Debug.Log(id + " " + PhotonNetwork.player.ID);
            }
            else
            {
                Debug.Log(id +" " + PhotonNetwork.player.ID);
            }
        }
	}
}

