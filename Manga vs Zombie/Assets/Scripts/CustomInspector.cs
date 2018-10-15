using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.AnimatedValues;
using System;
using System.Collections.Generic;
using UnityEditorInternal;
using System.IO;
using LitJson;
using System.Linq;

public class CustomInspector : EditorWindow
{

    float myFloat = 5;
    bool mybool = false;
    public Vector2 scrollPositon;
    public Rect windowRect = new Rect(100, 100, 200, 200);

    public Texture2D image;
    public Texture2D playerTexture;
    public AnimationClip playskill1;
    public AnimationClip playskill2;
    public AnimationClip playattack1;
    public AnimationClip playattack2;
    public AnimationClip playIdle;
    
    public string Urlimage = "Assets/Sprite/sprite sasuke/sskimage.jpg";
    int tab;
    //public string id;
    public string playername;
    public string stringToEdit = "", stringToEdit1 = "", stringToEdit2 = "", stringToEdit3 = ""
        , stringToEdit4 = "", stringToEdit5 = "", stringToEdit6 = "", stringtoEditurl;
    private string[] toolBar = new string[] { "Player", "Enemy", "Map", "Shop", "sence" };
    private string jsonString;
    JsonData playerData;
    JsonData playerJson;
    public string playerName;
    public float dmg;
    public float delay;
    public float dmgSkill1;
    public float cooldown1;
    public float dmgSkill2;
    public float cooldown2;
    public string urlimage;
    //private List<Character> database = new List<Character>();
    //Character player = new Character(2, "Assets/Sprite/sprite sasuke/sskimage.jpg", "Sasuke", 20, 0.3f, 50, 10, 100, 20);
    string Editurl;
    string Editname;
    float Editdmg;
    float Editdelay;
    float Editdmgskill1;
    float Editcool1;
    float Editdmgskill2;
    float Editcool2;
    int lenght;

    //void Test()
    //{
    //    List<Character> player = JsonHelper.FromJson<Character>(jsonString).ToList();
    //    Debug.Log(jsonString);
    //}
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
                lenght = player.Length;
                break;
            }

        }
        playername = Editname;
        stringtoEditurl = Editurl;
        stringToEdit = Editdelay.ToString();
        stringToEdit1 = Editdmg.ToString();
        stringToEdit2 = Editcool1.ToString();
        stringToEdit3 = Editdmgskill1.ToString();
        stringToEdit4 = Editcool2.ToString();
        stringToEdit5 = Editdmgskill2.ToString();
        playerTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(stringtoEditurl, typeof(Texture2D));
    }
    // public class SaveData
    //{
    public void SaveData(string filePath, int ID, string Editurl, string Editname, float Editdmg, float Editdelay, float Editdmgskill1, float Editcool1, float Editdmgskill2, float Editcool2)
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
                player[i]._Urlimage = Editurl;
                player[i]._Name = Editname;
                player[i]._Dmg = Editdmg;
                player[i]._Delay = Editdelay;
                player[i]._DmgSkill1 = Editdmgskill1;
                player[i]._Cooldown1 = Editcool1;
                player[i]._DmgSkill2 = Editdmgskill2;
                player[i]._Cooldown2 = Editcool2;

                break;
            }
        }

        //Convert to Json
        string newJsonString = JsonHelper.ToJson(player);

        //Save
        File.WriteAllText(Application.dataPath + filePath, newJsonString);
    }
    //}
    public void AddData(string filePath, string Editurl, string Editname, float Editdmg, float Editdelay, float Editdmgskill1, float Editcool1, float Editdmgskill2, float Editcool2)
    {
        //Load Data
        string jsonString = File.ReadAllText(Application.dataPath + filePath);


        Character[] player = JsonHelper.FromJson<Character>(jsonString);


        //Loop through the Json Data Array
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i]._Name == "")
            {

                player[i]._Id = i + 1;
                player[i]._Urlimage = Editurl;
                player[i]._Name = Editname;
                player[i]._Dmg = Editdmg;
                player[i]._Delay = Editdelay;
                player[i]._DmgSkill1 = Editdmgskill1;
                player[i]._Cooldown1 = Editcool1;
                player[i]._DmgSkill2 = Editdmgskill2;
                player[i]._Cooldown2 = Editcool2;

                break;
            }
        }

        //Convert to Json
        string newJsonString = JsonHelper.ToJson(player, true);

        //Save
        File.WriteAllText(Application.dataPath + filePath, newJsonString);
    }



    private void Awake()
    {

        LoadData("/PlayerSave.json", 1);
        //Datachange("/PlayerSave.json", 1, "1","1",2,1,1,1,1,1);
        //AddData("/PlayerSave.json", "1", "1", 4, 4, 4, 4, 4, 4);


        //playerData["Player"][0]["_name"] = playername;
        //Debug.Log(playerData);
        //Load();
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
    [MenuItem("Window/Custom Isnpector")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CustomInspector));

    }
    public static void Init()
    {
        GetWindow<CustomInspector>("FocusedWindow");
        EditorWindow.GetWindow(typeof(CustomInspector));


    }

    void OnGUI()
    {
        tab = GUILayout.Toolbar(tab, toolBar);
        switch (tab)
        {
            case 0:
                
                scrollPositon = GUILayout.BeginScrollView(scrollPositon);
                GUILayout.Label("Player Settings", EditorStyles.boldLabel);


                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Player Name :");
                //GUI.Label(new Rect(50, 50, 200, 30), "Name :" +m_PlayerName);
                playername = GUILayout.TextField(playername, 10, GUILayout.Width(200));

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();

                image = (Texture2D)EditorGUILayout.ObjectField("Avatar", playerTexture, typeof(Texture2D), false);

                //GUILayout.Label(playerTexture, GUILayout.Width(200));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);





                GUILayout.Label("Attack", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Attack delay :");
                //stringToEdit = Editdelay.ToString();
                stringToEdit = GUILayout.TextField(stringToEdit, 5, GUILayout.Width(200));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Dmg :");

                stringToEdit1 = GUILayout.TextField(stringToEdit1, 5, GUILayout.Width(200));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                GUILayout.Label("Skill 1 ", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Colldowns :");
                stringToEdit2 =
                    GUILayout.TextField(stringToEdit2, 5, GUILayout.Width(200));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Dmg :");

                stringToEdit3 =
                    GUILayout.TextField(stringToEdit3, 5, GUILayout.Width(200));

                string tdmg1 = stringToEdit3.ToString();
                UpdateDamage.Updatedmg1((string)tdmg1);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                GUILayout.Label("Skill 2 ", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Colldowns :");
                stringToEdit4 =
                        GUILayout.TextField(stringToEdit4, 5, GUILayout.Width(200));


                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Dmg :");

                stringToEdit5 =

                    GUILayout.TextField(stringToEdit5, 5, GUILayout.Width(200));

                string tdmg2 = stringToEdit5.ToString();
                UpdateDamage.Updatedmg2((string)tdmg2);

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


                

                //EditorGUILayout.BeginHorizontal();
                //player1 = EditorGUILayout.ObjectField(player1, typeof(GameObject), true) as GameObject;
                //EditorGUILayout.EndHorizontal();

                //player1.gameObject.GetComponent<Player1Controller>().SetValues(playerTexture,playername, dmg, atd, dmg1, cod1, dmg2, cod2);


                if (GUILayout.Button("Save"))
                {
                    Editurl = stringtoEditurl;
                    Editname = playername;
                    if (stringToEdit != null)
                    {
                        Editdelay = float.Parse(stringToEdit);
                    }

                    if (stringToEdit1 != null)
                    {
                        Editdmg = float.Parse(stringToEdit1);
                    }

                    if (stringToEdit2 != null)
                    {
                        Editcool1 = float.Parse(stringToEdit2);
                    }

                    if (stringToEdit3 != null)
                    {
                        Editdmgskill1 = float.Parse(stringToEdit3);
                    }

                    if (stringToEdit4 != null)
                    {
                        Editcool2 = float.Parse(stringToEdit4);
                    }

                    if (stringToEdit5 != null)
                    {
                        Editdmgskill2 = float.Parse(stringToEdit5);
                    }
                    if (stringToEdit == null)
                    {
                        stringToEdit = "0";
                        Editdelay = 0;
                    }
                    if (stringToEdit1 == null)
                    {
                        stringToEdit1 = "0";
                        Editdmg = 0;
                    }
                    if (stringToEdit2 == null)
                    {
                        stringToEdit2 = "0";
                        Editcool1 = 0;
                    }
                    if (stringToEdit3 == null)
                    {
                        stringToEdit3 = "0";
                        Editdmgskill1 = 0;
                    }
                    if (stringToEdit4 == null)
                    {
                        stringToEdit4 = "0";
                        Editcool2 = 0;
                    }
                    if (stringToEdit5 == null)
                    {
                        stringToEdit5 = "0";
                        Editdmgskill2 = 0;
                    }
                    SaveData("/PlayerSave.json", 1, Editurl, Editname, Editdmg, Editdelay, Editdmgskill1, Editcool1, Editdmgskill2, Editcool2);

                }

                GUILayout.Label("Animation ", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Idle :     ");
                playIdle = (AnimationClip)EditorGUILayout.ObjectField("", playIdle, typeof(AnimationClip), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Attack1 :");
                playattack1 = (AnimationClip)EditorGUILayout.ObjectField("", playattack1, typeof(AnimationClip), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Attack2 :");
                playattack2 = (AnimationClip)EditorGUILayout.ObjectField("", playattack2, typeof(AnimationClip), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Skill1 :   ");
                playskill1 = (AnimationClip)EditorGUILayout.ObjectField("", playskill1, typeof(AnimationClip), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Skill2 :   ");
                playskill2 = (AnimationClip)EditorGUILayout.ObjectField("", playskill2, typeof(AnimationClip), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button("Add Player"))
                {



                }
                GUILayout.EndScrollView();
                break;

            case 1:
                GUILayout.Label("Enemy Settings", EditorStyles.boldLabel);



                break;
            case 2:
                GUILayout.Label("Map Settings", EditorStyles.boldLabel);
                break;
            case 3:
                GUILayout.Label("Shop Settings", EditorStyles.boldLabel);

                break;
            case 4:
                GUILayout.Label("Sence Settings", EditorStyles.boldLabel);
                //GUILayout.Label(EditorWindow.focusedWindow.ToString());
                GUILayout.Label("Sound Settings", EditorStyles.boldLabel);
                mybool = EditorGUILayout.Toggle("Mute All Sounds", mybool);


                myFloat = EditorGUILayout.Slider("Sounds", myFloat, 0, 10);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                break;

        }
    }

}







