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
    [SerializeField] Character _character;

    private void Start()
    {
        Debug.Log(_character.Name);
    }

    private void Update()
    {
        if (update)
        {
            text1.text = "Damage:    " + UpdateDMG1.ToString();
            text2.text = "Damage:   " + UpdateDMG2.ToString();
            update = false;
        }
    }
    
    public static void Updatedmg1(string value)
    {
        UpdateDMG1 = value;
        update = true;
        
    }
    public static void Updatedmg2(string value)
    {
        UpdateDMG2 = value;
        update = true;
    }
}
