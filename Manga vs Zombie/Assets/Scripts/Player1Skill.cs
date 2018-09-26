using UnityEngine;
using UnityEngine.UI;

public class Player1Skill : MonoBehaviour
{

    public bool skill1 = false;
    public float skill1delay = 1;
    public bool skill2 = false;
    public float skill2delay = 1;
    public Collider2D trigger1;
    public Collider2D trigger2;
    public Image imageColldown1;
    private float cooldown1;
    public bool isCooldown1 = false;
    public Image imageColldown2;
    private float cooldown2;
    public bool isCooldown2 = false;
    public Animator anim;
    public int dmg1;
    public int dmg2;
    Player1Controller pl;
    public void Skill1()
    {
      
        if (isCooldown1 == false && skill2 == false &&pl.attacking==false &&pl.attacking1==false )
        {
            isCooldown1 = true;
            skill1 = true;
            skill1delay = 1;
            trigger1.enabled = true;
            cooldown1 = 5;
            dmg1 = 50;
        }
    }
    public void Skill2()
    {
        if (isCooldown2 == false && skill1 == false && pl.attacking == false && pl.attacking1 == false)
        {
            isCooldown2 = true;
            skill2 = true;
            skill2delay = 1;
            trigger2.enabled = true;
            cooldown2 = 10;
            dmg2 = 100;

        }
    }
     void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        pl = gameObject.GetComponent<Player1Controller>();

    }
    void Update()
    {
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
            imageColldown1.fillAmount += 1 / cooldown1 * Time.deltaTime;
        }
        if (imageColldown1.fillAmount >= 1)
        {
            imageColldown1.fillAmount = 0;
            isCooldown1 = false;
        }
        if (isCooldown2)
        {
            imageColldown2.fillAmount += 1 / cooldown2 * Time.deltaTime;
        }
        if (imageColldown2.fillAmount >= 1)
        {
            imageColldown2.fillAmount = 0;
            isCooldown2 = false;
        }
        anim.SetBool("Skill2", skill2);
        anim.SetBool("Skill1", skill1);
    }
}
