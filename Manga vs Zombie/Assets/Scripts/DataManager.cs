using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string CharacterUrl = "Data/Character";
    private string _name = "/Name";
    //private string _dmg = "/Damage";
    private int index;

    private void Start()
    {
        List<Player1Controller> players;
        LoadCharacter(out players);
    }

    public void SaveCharacter(Player1Controller character)
    {
        var newCharacter = CharacterUrl + "/" + index;
        PlayerPrefs.SetString(newCharacter + _name, character.gameObject.name);
        //PlayerPrefs.SetFloat(newCharacter + _dmg, character.dmg);

        index++;
        PlayerPrefs.SetInt(CharacterUrl, index);
    }

    public void LoadCharacter(out List<Player1Controller> character)
    {
        character = new List<Player1Controller>();
        for (int i = 0; i < PlayerPrefs.GetInt(CharacterUrl); i++)
        {
            Player1Controller newChar = new Player1Controller();
            newChar.gameObject.name = PlayerPrefs.GetString(CharacterUrl + "/" + i + _name);
            //newChar.dmg = PlayerPrefs.GetFloat(CharacterUrl + "/" + i + _dmg);
            character.Add(newChar);
        }
    }
}
