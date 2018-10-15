using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Player1Controller : MonoBehaviour
{
    public bool faceright = true;
    public static float attackdelay;
    public bool attacking = false;
    public bool attacking1 = false;
    public Animator anim;
    public Collider2D trigger;
    float h = 0;

    public bool skill1 = false;
    public float skill1delay = 1;
    public bool skill2 = false;
    public float skill2delay = 1;
    public Collider2D trigger1;
    public Collider2D trigger2;
    public Image imageColldown1;
    public bool isCooldown1 = false;
    public Image imageColldown2;
    public bool isCooldown2 = false;
    public float dl;


    public string Editurl;
    public string Editname;
    public float Editdmg;
    public float Editdelay;
    public float Editdmgskill1;
    public float Editcool1;
    public float Editdmgskill2;
    public float Editcool2;


    string jsonstring;

    public void LoadData(string filePath, int ID)
    {
        //Load Data
        string jsonString = File.ReadAllText(Application.dataPath + filePath);


        Character[] player = JsonHelper.FromJson<Character>(jsonString);

        //Loop through the Json Data Array
        for (int i = 0; i < player.Length; i++)
        {
            //Check if Id matches
            if (player[i]._Id == ID)
            {

                //Increment Change value?
                Editurl = player[i]._Urlimage;
                Editname = player[i]._Name;
                Editdmg = player[i]._Dmg;
                Editdelay = player[i]._Delay;
                Editdmgskill1 = player[i]._DmgSkill1;
                Editcool1 = player[i]._Cooldown1;
                Editdmgskill2 = player[i]._DmgSkill2;
                Editcool2 = player[i]._Cooldown2;

                break;
            }

        }
    }
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;
        LoadData("/PlayerSave.json", 1);
    }
    void Start()
    {

       // Debug.Log(Editdmg);
    }

    void Update()
    {


        if (h > 0 && !faceright)
        {
            Flip();
        }
        if (h < 0 && faceright)
        {
            Flip();
        }
        if (attacking1)
        {

            if (dl > 0)
            {
                dl -= Time.deltaTime;

            }
            else
            {
                attacking1 = false;

                trigger.enabled = false;

            }
        }
        if (attacking)
        {

            if (dl > 0)
            {
                dl -= Time.deltaTime;
            }
            else
            {

                attacking = false;
                trigger.enabled = false;

            }
        }
        if (skill1)
        {
            if (skill1delay > 0)
            {
                skill1delay -= Time.deltaTime;
            }
            else
            {
                skill1 = false;
                trigger1.enabled = false;
            }
        }
        if (skill2)
        {
            if (skill2delay > 0)
            {
                skill2delay -= Time.deltaTime;
            }
            else
            {
                skill2 = false;
                trigger2.enabled = false;
            }
        }
        if (isCooldown1)
        {
            imageColldown1.fillAmount += 1 / Editcool1 * Time.deltaTime;


            if (imageColldown1.fillAmount >= 1)
            {
                imageColldown1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
        if (isCooldown2)
        {

            imageColldown2.fillAmount += 1 / Editcool2 * Time.deltaTime;

            if (imageColldown2.fillAmount >= 1)
            {
                imageColldown2.fillAmount = 0;
                isCooldown2 = false;
            }
        }





        anim.SetBool("Skill1", skill1);
        anim.SetBool("Skill2", skill2);
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Attacking1", attacking1);
    }

    public void Flip()
    {
        faceright = !faceright;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;

    }
    public void Attack()
    {
        int random = UnityEngine.Random.Range(0, 2);
        trigger.enabled = true;

        dl = Editdelay;

        if (random == 0)
        {
            attacking = true;
        }
        else
        {
            attacking1 = true;
        }
    }
    public void AttackLeft()
    {
        if (attacking1 == false && attacking == false && skill1 == false && skill2 == false)
        {
            float h = -1;
            if (h > 0 && !faceright)
            {
                Flip();
            }
            if (h < 0 && faceright)
            {
                Flip();
            }
            Attack();


        }
    }
    public void AttackRight()
    {
        if (attacking1 == false && attacking == false && skill1 == false && skill2 == false)
        {
            float h = 1;
            if (h > 0 && !faceright)
            {
                Flip();
            }
            if (h < 0 && faceright)
            {
                Flip();
            }
            Attack();

        }
    }
    public void Skill1()

    {

        if (isCooldown1 == false && skill2 == false && attacking == false && attacking1 == false && skill1 == false)
        {
            isCooldown1 = true;
            skill1 = true;
            skill1delay = 1;
            trigger1.enabled = true;




        }
    }
    public void Skill2()
    {
        if (isCooldown2 == false && skill1 == false && attacking == false && attacking1 == false && skill2 == false)
        {
            isCooldown2 = true;
            skill2 = true;
            skill2delay = 1;
            trigger2.enabled = true;




        }
    }



    [Serializable]
    public class Character
    {
        public int _Id;
        public string _Urlimage;
        public string _Name;
        public float _Dmg;
        public float _Delay;
        public float _DmgSkill1;
        public float _Cooldown1;
        public float _DmgSkill2;
        public float _Cooldown2;
        public Character()
        {

        }
        public Character(int id, string urlimage, string playerName, float dmg, float delay, float dmgSkill1, float cooldown1, float dmgSkill2, float cooldown2)
        {
            _Id = id;
            _Urlimage = urlimage;
            _Name = playerName;
            _Dmg = dmg;
            _Delay = delay;
            _DmgSkill1 = dmgSkill1;
            _Cooldown1 = cooldown1;
            _DmgSkill2 = dmgSkill2;
            _Cooldown2 = cooldown2;
        }


    }
}



