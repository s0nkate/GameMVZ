using System.Collections;
using ECSComponent;

using UnityEngine;
[System.Serializable]
public class InventoryEnemy  {
    public string name;
    public Texture2D image = null;
    public int health;
    public float speed;
    public int damage;
    public float Delay;
    public int money;
    public int score;
    public ZombieType type; 
    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip attack;
    public AnimationClip dead;
}
