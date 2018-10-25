using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryPlayer   {
   // [HideInInspector]public int _Id;
    public Texture2D _image =null;
    public string _Name;
    public float _Dmg;
    public float _Delay;
    public float _DmgSkill1;
    public float _Cooldown1;
    public Sprite _Image1;
    public float _DmgSkill2;
    public float _Cooldown2;
    public Sprite _Image2;

    public AnimationClip playskill1;
    public AnimationClip playskill2;
    public AnimationClip playattack1;
    public AnimationClip playattack2;
    public AnimationClip playIdle;
    
}
