using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSComponent {
    public enum EffectType : int {
        None = -1,
        HeathUp = 0,
        DamageDown = 1,
        HouseDeffent = 2
    }

    public class Effect : MonoBehaviour {
        public Animator anim;
        public bool shield;
        public const int hpUpValue = 100;
        public const int damageDownValue = 10;
        public const float timeEffect = 5;
        public float t = 0;
        bool isCalledClean;
        public GameObject arrow;
        Heath heath;
        Attack attack;
        Zombie zombie;

        void Awake () {
            heath = GetComponent<Heath> ();
            attack = GetComponent<Attack> ();
            zombie = GetComponent<Zombie> ();
            anim = GetComponent<Animator> ();
        }

        [PunRPC]
        void HPUp () {
            anim.SetTrigger ("trigger");

            if (heath == null)
                return;
            int newHP = heath.value + hpUpValue;
            heath.value = newHP > heath.maxValue ? heath.maxValue : newHP;
            GameManager.Instance.effectType = EffectType.None;

        }

        [PunRPC]
        void DamageDown () {
            int newDamage = zombie.tempDamage - damageDownValue;
            attack.damage = newDamage > 0 ? newDamage : 0;
            arrow.SetActive (true);
            if(!isCalledClean)
            {
                isCalledClean = true;
                Invoke ("CleanEffect", timeEffect);
            }
        }

        [PunRPC]
        void HouseDeffent () {
            if (zombie == null) {
                if (!shield) {
                    shield = true;
                    anim.SetBool ("bool", shield);
                }
            } else {
                attack.damage = 0;
            }
            if(!isCalledClean)
            {
                isCalledClean = true;
                Invoke ("CleanEffect", timeEffect);
            }
            
        }

        void CleanEffect () {
            shield = false;
            if (zombie == null) {
                anim.SetBool ("bool", false);
                GameManager.Instance.effectType = EffectType.None;
            } else {
                arrow.SetActive (false);
                GameManager.Instance.effectType = EffectType.None;
                attack.damage = zombie.tempDamage;
            }

        }

    }
}