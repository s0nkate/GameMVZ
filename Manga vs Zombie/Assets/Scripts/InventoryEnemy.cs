using System.Collections;

using UnityEngine;
[System.Serializable]
public class InventoryEnemy  {
    public string name;
    public Texture2D image = null;
    public float health;
    public float speed;
    public float damage;
    public float Delay;
    public int money;
    public int score;
    public enum Type {Walker=0
            ,Runner=1
            ,Hulker=2,
             Exploder=3 };
    public Type type; 
    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip attack;
    public AnimationClip dead;
}
