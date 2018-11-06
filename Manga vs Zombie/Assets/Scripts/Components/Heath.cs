using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ECSComponent
{
	public class Heath : MonoBehaviour
    {
		public int value;
		public int maxValue;
		public Slider heathSlider;
		public delegate void InjuredHander(GameObject heath, int damage);
		public InjuredHander OnInjured;
        public delegate void CheckScore(int score,int money, int id);
        public CheckScore CheckID;
        public int idAttack;
        public bool isDead;
		
		public void TakeDamage(int damage)
		{
			if(OnInjured != null)
			{
				OnInjured(gameObject, damage);
			}
		}
        public void CheckId(int id)
        {
            if (CheckID != null)
            {
                CheckID(GetComponent<Zombie>().score, GetComponent<Zombie>().money, id);
            }
        }

        [PunRPC]
		void InactiveZombie(int viewID)
		{
			PhotonView photonView = PhotonView.Find(viewID);
			if(photonView != null)
			{
				photonView.gameObject.SetActive(false);
			}
		}

		[PunRPC]
		void MarkDead(int viewID)
		{
			PhotonView photonView = PhotonView.Find(viewID);
			if(photonView)
			{
				photonView.gameObject.GetComponent<Faction>().currentState = State.Dead;
				photonView.gameObject.GetComponent<Animator>().SetInteger("stage", (int)State.Dead);	
			}
		}
    
    }	
}
