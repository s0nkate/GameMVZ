using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDamage : MonoBehaviour
{

    public static string UpdateDMG1;
    public  Text text1;
    public Text text2;
    public static string UpdateDMG2;
    static bool update=false;
    public InventoryPlayerList playerlist;
    public int i;

     void Start()
    {
        Debug.Log("" + playerlist.selectedPlayerindex);
        //LoadData();
        //text1.text = "Damage:    " + playerlist.playerList[i]._DmgSkill1.ToString();
        //text2.text = "Damage:   " + playerlist.playerList[i]._DmgSkill2.ToString();
    }

    public void LoadData()
    {

        //i = playerlist.selectedPlayerindex;
    }
}
