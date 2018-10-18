using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryItemEditor : EditorWindow
{

    public InventoryPlayerList inventoryPlayerList;
    public InventoryEnemyList inventoryEnemyList;
    public InventoryMap inventoryMap;
    private int viewIndex = 1;
    private int viewIndex1 = 1;
    private string[] toolBar = new string[] { "Player", "Enemy", "Map", "Shop", "sence" };
    int tab;
    float myFloat = 5;
    bool mybool = false;
    public InventoryEnemy.Type type;
    [MenuItem("Window/Custom Inspector %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(InventoryItemEditor));
    }
    public 
    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            inventoryPlayerList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(InventoryPlayerList)) as InventoryPlayerList;
            
        }
        if (EditorPrefs.HasKey("ObjectPath1"))
        {
            string objectPath1 = EditorPrefs.GetString("ObjectPath1");
            inventoryEnemyList = AssetDatabase.LoadAssetAtPath(objectPath1, typeof(InventoryEnemyList)) as InventoryEnemyList;
        }
        if (EditorPrefs.HasKey("ObjectPath2"))
        {
            string objectPath2 = EditorPrefs.GetString("ObjectPath2");
            inventoryMap = AssetDatabase.LoadAssetAtPath(objectPath2, typeof(InventoryMap)) as InventoryMap;
        }
    }

    void OnGUI()
    {


        tab = GUILayout.Toolbar(tab, toolBar);
        switch (tab)
        {
            case 0:
                GUILayout.BeginHorizontal();
                GUILayout.Label(" Player Editor", EditorStyles.boldLabel);
                if (inventoryPlayerList != null)
                {
                    if (GUILayout.Button("Show Player List"))
                    {
                        EditorUtility.FocusProjectWindow();
                        Selection.activeObject = inventoryPlayerList;
                    }
                }
                if (GUILayout.Button("Open Player List"))
                {
                    OpenItemList();
                }
                //if (GUILayout.Button("New Player List"))
                //{
                //    EditorUtility.FocusProjectWindow();
                //    Selection.activeObject = inventoryPlayerList;
                //}
                GUILayout.EndHorizontal();

                //if (inventoryPlayerList == null)
                //{
                //    GUILayout.BeginHorizontal();
                //    GUILayout.Space(10);
                //    if (GUILayout.Button("Create New Player List", GUILayout.ExpandWidth(false)))
                //    {
                //        CreateNewItemList();
                //    }
                //    if (GUILayout.Button("Open Existing Player List", GUILayout.ExpandWidth(false)))
                //    {
                //        OpenItemList();
                //    }
                //    GUILayout.EndHorizontal();
                //}

                GUILayout.Space(20);

                if (inventoryPlayerList != null)
                {
                    GUILayout.BeginHorizontal();

                    GUILayout.Space(10);

                    if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
                    {
                        if (viewIndex > 1)
                            viewIndex--;
                    }
                    GUILayout.Space(5);
                    if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
                    {
                        if (viewIndex < inventoryPlayerList.playerList.Count)
                        {
                            viewIndex++;
                        }
                    }

                    GUILayout.Space(60);

                    if (GUILayout.Button("Add Player", GUILayout.ExpandWidth(false)))
                    {
                        AddItem();
                    }
                    if (GUILayout.Button("Delete Player", GUILayout.ExpandWidth(false)))
                    {
                        DeleteItem(viewIndex - 1);
                    }

                    GUILayout.EndHorizontal();
                    if (inventoryPlayerList.playerList == null)
                        Debug.Log("wtf");
                    if (inventoryPlayerList.playerList.Count > 0)
                    {
                        GUILayout.BeginHorizontal();
                        viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Player", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryPlayerList.playerList.Count);
                        //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                        EditorGUILayout.LabelField("of   " + inventoryPlayerList.playerList.Count.ToString() + "  Player", "", GUILayout.ExpandWidth(false));
                        GUILayout.EndHorizontal();

                        //inventoryPlayerList.playerList[viewIndex - 1]._Id =
                        //EditorGUILayout.IntField("Player Id", inventoryPlayerList.playerList[viewIndex - 1]._Id, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Name = EditorGUILayout.TextField("Player Name", inventoryPlayerList.playerList[viewIndex - 1]._Name as string);
                        inventoryPlayerList.playerList[viewIndex - 1]._image = EditorGUILayout.ObjectField("Player Avatar", inventoryPlayerList.playerList[viewIndex - 1]._image, typeof(Texture2D), false) as Texture2D;


                        inventoryPlayerList.playerList[viewIndex - 1]._Dmg = EditorGUILayout.FloatField("Dmg ", inventoryPlayerList.playerList[viewIndex - 1]._Dmg, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Delay = EditorGUILayout.FloatField("Delay ", inventoryPlayerList.playerList[viewIndex - 1]._Delay, GUILayout.ExpandWidth(false));
                        
                        inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill1 = EditorGUILayout.FloatField("DmgSkill1 ", inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill1, GUILayout.ExpandWidth(false));
                        GUILayout.BeginHorizontal();
                        inventoryPlayerList.playerList[viewIndex - 1]._Cooldown1 = EditorGUILayout.FloatField("Cooldown1 ", inventoryPlayerList.playerList[viewIndex - 1]._Cooldown1, GUILayout.ExpandWidth(false));

                        inventoryPlayerList.playerList[viewIndex - 1]._Image1 = EditorGUILayout.ObjectField("", inventoryPlayerList.playerList[viewIndex - 1]._Image1, typeof(Sprite), false) as Sprite;
                        GUILayout.EndHorizontal();
                        inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill2 = EditorGUILayout.FloatField("DmgSkill2 ", inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill2, GUILayout.ExpandWidth(false));
                        GUILayout.BeginHorizontal();
                        inventoryPlayerList.playerList[viewIndex - 1]._Cooldown2 = EditorGUILayout.FloatField("Cooldown2 ", inventoryPlayerList.playerList[viewIndex - 1]._Cooldown2, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Image2 = EditorGUILayout.ObjectField("", inventoryPlayerList.playerList[viewIndex - 1]._Image2, typeof(Sprite), false) as Sprite;
                        GUILayout.EndHorizontal();
                        GUILayout.Label("Animation", EditorStyles.boldLabel);
                        inventoryPlayerList.playerList[viewIndex - 1].playIdle = EditorGUILayout.ObjectField("Idle", inventoryPlayerList.playerList[viewIndex - 1].playIdle, typeof(AnimationClip), false) as AnimationClip;
                        inventoryPlayerList.playerList[viewIndex - 1].playattack1 = EditorGUILayout.ObjectField("Attack1", inventoryPlayerList.playerList[viewIndex - 1].playattack1, typeof(AnimationClip), false) as AnimationClip;
                        inventoryPlayerList.playerList[viewIndex - 1].playattack2 = EditorGUILayout.ObjectField("Attack2", inventoryPlayerList.playerList[viewIndex - 1].playattack2, typeof(AnimationClip), false) as AnimationClip;
                        inventoryPlayerList.playerList[viewIndex - 1].playskill1 = EditorGUILayout.ObjectField("Skill1", inventoryPlayerList.playerList[viewIndex - 1].playskill1, typeof(AnimationClip), false) as AnimationClip;
                        inventoryPlayerList.playerList[viewIndex - 1].playskill2 = EditorGUILayout.ObjectField("Skill2", inventoryPlayerList.playerList[viewIndex - 1].playskill2, typeof(AnimationClip), false) as AnimationClip;




                    }
                    else
                    {
                        GUILayout.Label("This Inventory Player is Empty.");
                    }
                }


                if (GUILayout.Button("Use Player"))
                {

                    inventoryPlayerList.selectedPlayerindex = viewIndex - 1;
                }

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(inventoryPlayerList);
                }

                break;

            case 1:

                GUILayout.BeginHorizontal();
                GUILayout.Label(" Enemy Editor", EditorStyles.boldLabel);
                if (inventoryEnemyList != null)
                {
                    if (GUILayout.Button("Show Enemy List"))
                    {
                        EditorUtility.FocusProjectWindow();
                        Selection.activeObject = inventoryEnemyList;
                    }
                }
                if (GUILayout.Button("Open Enemy List"))
                {
                    OpenItemList();
                }

                GUILayout.EndHorizontal();

                //if (inventoryEnemyList == null)
                //{
                //    GUILayout.BeginHorizontal();
                //    GUILayout.Space(10);
                //    //if (GUILayout.Button("Create New Enemy List", GUILayout.ExpandWidth(false)))
                //    //{
                //    //    CreateNewItemList();
                //    //}
                //    //if (GUILayout.Button("Open Existing Enemy List", GUILayout.ExpandWidth(false)))
                //    //{
                //    //    OpenItemList();
                //    //}
                //    GUILayout.EndHorizontal();
                //}

                GUILayout.Space(20);

                if (inventoryEnemyList != null)
                {
                    GUILayout.BeginHorizontal();

                    GUILayout.Space(10);

                    if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
                    {
                        if (viewIndex1 > 1)
                            viewIndex1--;
                    }
                    GUILayout.Space(5);
                    if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
                    {
                        if (viewIndex1 < inventoryEnemyList.enemyList.Count)
                        {
                            viewIndex1++;
                        }
                    }

                    GUILayout.Space(60);

                    if (GUILayout.Button("Add Enemy", GUILayout.ExpandWidth(false)))
                    {
                        AddItem();
                    }
                    if (GUILayout.Button("Delete Enemy", GUILayout.ExpandWidth(false)))
                    {
                        DeleteItem(viewIndex - 1);
                    }

                    GUILayout.EndHorizontal();
                    if (inventoryEnemyList.enemyList == null)
                        Debug.Log("wtf");
                    if (inventoryEnemyList.enemyList.Count > 0)
                    {
                        GUILayout.BeginHorizontal();
                        viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Enemy", viewIndex1, GUILayout.ExpandWidth(false)), 1, inventoryEnemyList.enemyList.Count);
                        //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                        EditorGUILayout.LabelField("of   " + inventoryEnemyList.enemyList.Count.ToString() + "  Enemy", "", GUILayout.ExpandWidth(false));
                        GUILayout.EndHorizontal();
                        inventoryEnemyList.enemyList[viewIndex1 - 1].name = EditorGUILayout.TextField("Enemy Name", inventoryEnemyList.enemyList[viewIndex1 - 1].name as string);
                        inventoryEnemyList.enemyList[viewIndex1 - 1].image = EditorGUILayout.ObjectField("Enemy avatar", inventoryEnemyList.enemyList[viewIndex1 - 1].image, typeof(Texture2D), false) as Texture2D;
                        inventoryEnemyList.enemyList[viewIndex1 - 1].health = EditorGUILayout.FloatField("Health ", inventoryEnemyList.enemyList[viewIndex1 - 1].health, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].speed = EditorGUILayout.FloatField("Speed ", inventoryEnemyList.enemyList[viewIndex1 - 1].speed, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].damage = EditorGUILayout.FloatField("Damage ", inventoryEnemyList.enemyList[viewIndex1 - 1].damage, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].Delay = EditorGUILayout.FloatField("Delay ", inventoryEnemyList.enemyList[viewIndex1 - 1].Delay, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].money = EditorGUILayout.IntField("Money ", inventoryEnemyList.enemyList[viewIndex1 - 1].money, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].score = EditorGUILayout.IntField("Scose ", inventoryEnemyList.enemyList[viewIndex1 - 1].score, GUILayout.ExpandWidth(false));
                        type = (InventoryEnemy.Type)EditorGUILayout.EnumPopup("Type", type, GUILayout.ExpandWidth(false));
                        GUILayout.Label("Animation", EditorStyles.boldLabel);
                        inventoryEnemyList.enemyList[viewIndex - 1].idle = EditorGUILayout.ObjectField("Idle", inventoryEnemyList.enemyList[viewIndex - 1].idle, typeof(AnimationClip), false) as AnimationClip;
                        inventoryEnemyList.enemyList[viewIndex - 1].walk = EditorGUILayout.ObjectField("Walk", inventoryEnemyList.enemyList[viewIndex - 1].walk, typeof(AnimationClip), false) as AnimationClip;
                        inventoryEnemyList.enemyList[viewIndex - 1].attack = EditorGUILayout.ObjectField("Attack", inventoryEnemyList.enemyList[viewIndex - 1].attack, typeof(AnimationClip), false) as AnimationClip;
                        inventoryEnemyList.enemyList[viewIndex - 1].dead = EditorGUILayout.ObjectField("Dead", inventoryEnemyList.enemyList[viewIndex - 1].dead, typeof(AnimationClip), false) as AnimationClip;



                    }
                }
                else
                {
                    GUILayout.Label("This Inventory Enemy is Empty.");
                }
        


        if (GUILayout.Button("Use Enemy"))
        {

            inventoryPlayerList.selectedPlayerindex = viewIndex1 - 1;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(inventoryEnemyList);
        }

        break;
    
            case 2:
                GUILayout.BeginHorizontal();
                GUILayout.Label(" Enemy Editor", EditorStyles.boldLabel);
                if (inventoryMap != null)
                {
                    if (GUILayout.Button("Show Map List"))
                    {
                        EditorUtility.FocusProjectWindow();
                        Selection.activeObject = inventoryMap;
                    }
                }
                if (GUILayout.Button("Open Map List"))
                {
                    OpenItemList();
                }
                
                 GUILayout.EndHorizontal();
                GUILayout.Label("Map Settings", EditorStyles.boldLabel);
                inventoryMap.Backgournd=EditorGUILayout.ObjectField("Backgournd",inventoryMap.Backgournd, typeof(Texture2D), false) as Texture2D;
                inventoryMap.Foregournd = EditorGUILayout.ObjectField("Foregournd", inventoryMap.Foregournd, typeof(Texture2D), false) as Texture2D;
                inventoryMap.Tower = EditorGUILayout.ObjectField("Tower", inventoryMap.Tower, typeof(Texture2D), false) as Texture2D;
                inventoryMap.Towerenemy = EditorGUILayout.ObjectField("Towerenemy", inventoryMap.Towerenemy, typeof(Texture2D), false) as Texture2D;
                if (GUI.changed)
                {
                    EditorUtility.SetDirty(inventoryMap);
                }
                
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
    void CreateNewItemList()
    { if(tab==0) { 
       
        viewIndex = 1;
        inventoryPlayerList = CreateInventoryItemList.Create();
        if (inventoryPlayerList)
        {
            inventoryPlayerList.playerList = new List<InventoryPlayer>();
            string relPath = AssetDatabase.GetAssetPath(inventoryPlayerList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
        }
        if (tab == 1)
        {
            viewIndex1 = 1;
            inventoryEnemyList = CreateInventoryItemList.Create1();
            if (inventoryEnemyList)
            {
                inventoryEnemyList.enemyList = new List<InventoryEnemy>();
                string relPath1 = AssetDatabase.GetAssetPath(inventoryEnemyList);
                EditorPrefs.SetString("ObjectPath1", relPath1);
            }


        }
    }

    void OpenItemList()
    {
        if (tab == 0) { 
        string absPath = EditorUtility.OpenFilePanel("Select Inventory Item List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            inventoryPlayerList = AssetDatabase.LoadAssetAtPath(relPath, typeof(InventoryPlayerList)) as InventoryPlayerList;
            if (inventoryPlayerList.playerList == null)
                inventoryPlayerList.playerList = new List<InventoryPlayer>();
            if (inventoryPlayerList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
        }
        if (tab == 1)
        {
            string absPath1 = EditorUtility.OpenFilePanel("Select Inventory Item List", "", "");
            if (absPath1.StartsWith(Application.dataPath))
            {
                string relPath1 = absPath1.Substring(Application.dataPath.Length - "Assets".Length);
                inventoryEnemyList = AssetDatabase.LoadAssetAtPath(relPath1, typeof(InventoryEnemyList)) as InventoryEnemyList;
                if (inventoryEnemyList.enemyList == null)
                    inventoryEnemyList.enemyList = new List<InventoryEnemy>();
                if (inventoryEnemyList)
                {
                    EditorPrefs.SetString("ObjectPath1", relPath1);
                }
            }
        }
        if (tab == 2)
        {
            string absPath2 = EditorUtility.OpenFilePanel("Select Inventory Map", "", "");
            if (absPath2.StartsWith(Application.dataPath))
            {
                string relPath2 = absPath2.Substring(Application.dataPath.Length - "Assets".Length);
                inventoryMap = AssetDatabase.LoadAssetAtPath(relPath2, typeof(InventoryMap)) as InventoryMap;
                if (inventoryMap == null)
                    inventoryMap = new InventoryMap();
                if (inventoryMap)
                {
                    EditorPrefs.SetString("ObjectPath2", relPath2);
                }
            }
        }
    }

    void AddItem()
    {
        if (tab == 0)
        {
            InventoryPlayer newItem = new InventoryPlayer();
            newItem._Name = "New Item";
            //newItem._Id = inventoryPlayerList.playerList.Count;
            inventoryPlayerList.playerList.Add(newItem);
            viewIndex = inventoryPlayerList.playerList.Count;
            
        }
        if (tab == 1)
        {
            InventoryEnemy newItem1 = new InventoryEnemy();
            newItem1.name = "New Enemy";
            
            inventoryEnemyList.enemyList.Add(newItem1);
            viewIndex1 = inventoryEnemyList.enemyList.Count;
        }
    }
    void DeleteItem(int index)
    {
        if (tab == 0)
        {
            inventoryPlayerList.playerList.RemoveAt(index);
        }
        if (tab == 1)
        { 
        inventoryEnemyList.enemyList.RemoveAt(index);
        }
    }
  
}