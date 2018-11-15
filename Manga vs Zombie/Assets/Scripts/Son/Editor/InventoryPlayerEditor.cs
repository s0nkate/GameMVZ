using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using ECSComponent;

public class InventoryItemEditor : EditorWindow
{
    public InventoryPlayerList inventoryPlayerList;
    public InventoryEnemyList inventoryEnemyList;
    public InventoryItemList inventoryItemList;
    public InventorySceneList inventorySceneList;
    private int viewIndex = 1;
    private int viewIndex1 = 1;
    private int viewIndex2 = 1;
    private int viewIndex3 = 1;
    private string[] toolBar = new string[] { "Player", "Enemy", "Scene", "Shop" };
    private string[] toolBar1 = new string[] { "Player", "Item" };
    int tab;
    int tab1;
    public ZombieType type;
    public Vector2 scrollPositon;
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
            inventorySceneList = AssetDatabase.LoadAssetAtPath(objectPath2, typeof(InventorySceneList)) as InventorySceneList;
        }
        if (EditorPrefs.HasKey("ObjectPath3"))
        {
            string objectPath3 = EditorPrefs.GetString("ObjectPath3");
            inventoryItemList = AssetDatabase.LoadAssetAtPath(objectPath3, typeof(InventoryItemList)) as InventoryItemList;
        }
    }

    void OnGUI()
    {
        tab = GUILayout.Toolbar(tab, toolBar);
        switch (tab)
        {
            case 0:
                scrollPositon = GUILayout.BeginScrollView(scrollPositon);
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
                        EditorGUILayout.LabelField("of   " + inventoryPlayerList.playerList.Count.ToString() + "  Player", "", GUILayout.ExpandWidth(false));
                        GUILayout.EndHorizontal();
                        inventoryPlayerList.playerList[viewIndex - 1]._Name = EditorGUILayout.TextField("Player Name", inventoryPlayerList.playerList[viewIndex - 1]._Name as string);
                        inventoryPlayerList.playerList[viewIndex - 1]._image = EditorGUILayout.ObjectField("Player Avatar", inventoryPlayerList.playerList[viewIndex - 1]._image, typeof(Texture2D), false) as Texture2D;
                        inventoryPlayerList.playerList[viewIndex - 1]._Dmg = EditorGUILayout.FloatField("Dmg ", inventoryPlayerList.playerList[viewIndex - 1]._Dmg, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Delay = EditorGUILayout.FloatField("Delay ", inventoryPlayerList.playerList[viewIndex - 1]._Delay, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._SoundPunch = EditorGUILayout.ObjectField("SoundPunch ", inventoryPlayerList.playerList[viewIndex - 1]._SoundPunch, typeof(AudioClip), false) as AudioClip;
                        inventoryPlayerList.playerList[viewIndex - 1]._SoundKick = EditorGUILayout.ObjectField("SoundKick ", inventoryPlayerList.playerList[viewIndex - 1]._SoundKick, typeof(AudioClip), false) as AudioClip;
                        inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill1 = EditorGUILayout.FloatField("DmgSkill1 ", inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill1, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Cooldown1 = EditorGUILayout.FloatField("Cooldown1 ", inventoryPlayerList.playerList[viewIndex - 1]._Cooldown1, GUILayout.ExpandWidth(false));
                        GUILayout.BeginHorizontal();
                        inventoryPlayerList.playerList[viewIndex - 1]._SoundSkill1 = EditorGUILayout.ObjectField("SoundSkill1 ", inventoryPlayerList.playerList[viewIndex - 1]._SoundSkill1, typeof(AudioClip), false) as AudioClip;
                        inventoryPlayerList.playerList[viewIndex - 1]._Image1 = EditorGUILayout.ObjectField("", inventoryPlayerList.playerList[viewIndex - 1]._Image1, typeof(Sprite), false) as Sprite;
                        GUILayout.EndHorizontal();
                        inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill2 = EditorGUILayout.FloatField("DmgSkill2 ", inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill2, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Cooldown2 = EditorGUILayout.FloatField("Cooldown2 ", inventoryPlayerList.playerList[viewIndex - 1]._Cooldown2, GUILayout.ExpandWidth(false));
                        GUILayout.BeginHorizontal();
                        inventoryPlayerList.playerList[viewIndex - 1]._SoundSkill2 = EditorGUILayout.ObjectField("SoundSkill2 ", inventoryPlayerList.playerList[viewIndex - 1]._SoundSkill2, typeof(AudioClip), false) as AudioClip;
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
                GUILayout.EndScrollView();
                break;

            case 1:
                scrollPositon = GUILayout.BeginScrollView(scrollPositon);

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
                        DeleteItem(viewIndex1 - 1);
                    }

                    GUILayout.EndHorizontal();
                    if (inventoryEnemyList.enemyList == null)
                        Debug.Log("wtf");
                    if (inventoryEnemyList.enemyList.Count > 0)
                    {
                        GUILayout.BeginHorizontal();
                        viewIndex1 = Mathf.Clamp(EditorGUILayout.IntField("Current Enemy", viewIndex1, GUILayout.ExpandWidth(false)), 1, inventoryEnemyList.enemyList.Count);
                        EditorGUILayout.LabelField("of   " + inventoryEnemyList.enemyList.Count.ToString() + "  Enemy", "", GUILayout.ExpandWidth(false));
                        GUILayout.EndHorizontal();
                        inventoryEnemyList.enemyList[viewIndex1 - 1].name = EditorGUILayout.TextField("Enemy Name", inventoryEnemyList.enemyList[viewIndex1 - 1].name as string);
                        inventoryEnemyList.enemyList[viewIndex1 - 1].image = EditorGUILayout.ObjectField("Enemy avatar", inventoryEnemyList.enemyList[viewIndex1 - 1].image, typeof(Texture2D), false) as Texture2D;
                        inventoryEnemyList.enemyList[viewIndex1 - 1].health = EditorGUILayout.IntField("Health ", inventoryEnemyList.enemyList[viewIndex1 - 1].health, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].speed = EditorGUILayout.FloatField("Speed ", inventoryEnemyList.enemyList[viewIndex1 - 1].speed, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].damage = EditorGUILayout.IntField("Damage ", inventoryEnemyList.enemyList[viewIndex1 - 1].damage, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].Delay = EditorGUILayout.FloatField("Delay ", inventoryEnemyList.enemyList[viewIndex1 - 1].Delay, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].money = EditorGUILayout.IntField("Money ", inventoryEnemyList.enemyList[viewIndex1 - 1].money, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].score = EditorGUILayout.IntField("Scose ", inventoryEnemyList.enemyList[viewIndex1 - 1].score, GUILayout.ExpandWidth(false));
                        inventoryEnemyList.enemyList[viewIndex1 - 1].type = (ZombieType)EditorGUILayout.EnumPopup("Type", inventoryEnemyList.enemyList[viewIndex1 - 1].type, GUILayout.ExpandWidth(false));
                        GUILayout.Label("Animation", EditorStyles.boldLabel);
                        inventoryEnemyList.enemyList[viewIndex1 - 1].idle = EditorGUILayout.ObjectField("Idle", inventoryEnemyList.enemyList[viewIndex1 - 1].idle, typeof(AnimationClip), false) as AnimationClip;
                        inventoryEnemyList.enemyList[viewIndex1 - 1].walk = EditorGUILayout.ObjectField("Walk", inventoryEnemyList.enemyList[viewIndex1 - 1].walk, typeof(AnimationClip), false) as AnimationClip;
                        inventoryEnemyList.enemyList[viewIndex1- 1].attack = EditorGUILayout.ObjectField("Attack", inventoryEnemyList.enemyList[viewIndex1 - 1].attack, typeof(AnimationClip), false) as AnimationClip;
                        inventoryEnemyList.enemyList[viewIndex1 - 1].dead = EditorGUILayout.ObjectField("Dead", inventoryEnemyList.enemyList[viewIndex1 - 1].dead, typeof(AnimationClip), false) as AnimationClip;
                    }
                }
                else
                {
                    GUILayout.Label("This Inventory Enemy is Empty.");
                }
        


        if (GUILayout.Button("Use Enemy"))
        {

            //inventoryPlayerList.selectedPlayerindex = viewIndex1 - 1;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(inventoryEnemyList);
        }
                GUILayout.EndScrollView();
        break;
    
            case 2:
                scrollPositon = GUILayout.BeginScrollView(scrollPositon);

                GUILayout.BeginHorizontal();
                GUILayout.Label(" Scene Editor", EditorStyles.boldLabel);
                if (inventorySceneList != null)
                {
                    if (GUILayout.Button("Show Scene List"))
                    {
                        EditorUtility.FocusProjectWindow();
                        Selection.activeObject = inventorySceneList;
                    }
                }
                if (GUILayout.Button("Open Scene List"))
                {
                    OpenItemList();
                }
                GUILayout.EndHorizontal();
                if (inventorySceneList != null)
                {
                    GUILayout.BeginHorizontal();

                    GUILayout.Space(10);

                    if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
                    {
                        if (viewIndex3 > 1)
                            viewIndex3--;
                    }
                    GUILayout.Space(5);
                    if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
                    {
                        if (viewIndex3 < inventorySceneList.scenelist.Count)
                        {
                            viewIndex3++;
                        }
                    }

                    GUILayout.Space(60);

                    if (GUILayout.Button("Add Scene", GUILayout.ExpandWidth(false)))
                    {
                        AddItem();
                    }
                    if (GUILayout.Button("Delete Scene", GUILayout.ExpandWidth(false)))
                    {
                        DeleteItem(viewIndex3 - 1);
                    }
                    GUILayout.EndHorizontal();
                }
                if (inventorySceneList.scenelist.Count > 0)
                {
                    GUILayout.BeginHorizontal();
                    viewIndex3 = Mathf.Clamp(EditorGUILayout.IntField("Current Scene", viewIndex3, GUILayout.ExpandWidth(false)), 1, inventorySceneList.scenelist.Count);
                    EditorGUILayout.LabelField("of   " + inventorySceneList.scenelist.Count.ToString() + "  Scene", "", GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                    GUILayout.Label("Scene Settings", EditorStyles.boldLabel);
                    GUILayout.Label("Level " + viewIndex3);
                    inventorySceneList.scenelist[viewIndex3 - 1].TimePlay = EditorGUILayout.IntField("Time Play", inventorySceneList.scenelist[viewIndex3 - 1].TimePlay, GUILayout.ExpandWidth(false));
                    inventorySceneList.scenelist[viewIndex3 - 1].DelayEnemy = EditorGUILayout.FloatField("Delay Enemy", inventorySceneList.scenelist[viewIndex3 - 1].DelayEnemy, GUILayout.ExpandWidth(false));
                    inventorySceneList.scenelist[viewIndex3 - 1].Healtower = EditorGUILayout.IntField("Healtower", inventorySceneList.scenelist[viewIndex3 - 1].Healtower, GUILayout.ExpandWidth(false));
                    inventorySceneList.scenelist[viewIndex3 - 1].Backgournd = EditorGUILayout.ObjectField("Backgournd", inventorySceneList.scenelist[viewIndex3 - 1].Backgournd, typeof(Sprite), false) as Sprite;
                    inventorySceneList.scenelist[viewIndex3 - 1].Foregournd = EditorGUILayout.ObjectField("Foregournd", inventorySceneList.scenelist[viewIndex3 - 1].Foregournd, typeof(Sprite), false) as Sprite;
                    inventorySceneList.scenelist[viewIndex3 - 1].Tower = EditorGUILayout.ObjectField("Tower", inventorySceneList.scenelist[viewIndex3 - 1].Tower, typeof(Sprite), false) as Sprite;
                    inventorySceneList.scenelist[viewIndex3 - 1].Towerenemy = EditorGUILayout.ObjectField("Tower Enemy", inventorySceneList.scenelist[viewIndex3 - 1].Towerenemy, typeof(Sprite), false) as Sprite;
                    if (GUI.changed)
                    {
                        EditorUtility.SetDirty(inventorySceneList);
                    }
                    inventorySceneList.selectedsceneindex = viewIndex3;
                }
                GUILayout.EndScrollView();
                break;
            case 3:
                GUILayout.Label("Shop Settings", EditorStyles.boldLabel);
                tab1 = GUILayout.Toolbar(tab1, toolBar1);
                switch (tab1)
                {
                    case 0:
                        scrollPositon = GUILayout.BeginScrollView(scrollPositon);

                        GUILayout.Label(" Shop Player Editor", EditorStyles.boldLabel);
                        if (inventoryPlayerList != null)
                        {
                            if (GUILayout.Button("Show Player List"))
                            {
                                EditorUtility.FocusProjectWindow();
                                Selection.activeObject = inventoryPlayerList;
                            }

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


                            if (GUILayout.Button("Delete Player", GUILayout.ExpandWidth(false)))
                            {
                                DeleteItem(viewIndex - 1);
                            }
                        }
                        GUILayout.EndHorizontal();
                        if (inventoryPlayerList.playerList.Count > 0)
                        {
                            GUILayout.BeginHorizontal();
                            viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Player", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryPlayerList.playerList.Count);
                            EditorGUILayout.LabelField("  of   " + inventoryPlayerList.playerList.Count.ToString() + "  Player", "", GUILayout.ExpandWidth(false));
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label(inventoryPlayerList.playerList[viewIndex - 1]._image, GUILayout.Height(70), GUILayout.Width(70));
                            GUILayout.EndHorizontal();
                            GUILayout.Label("Player Name :" + inventoryPlayerList.playerList[viewIndex - 1]._Name);
                            
                            GUILayout.Label("Dmg : " + inventoryPlayerList.playerList[viewIndex - 1]._Dmg);

                            GUILayout.Label("Delay : " + inventoryPlayerList.playerList[viewIndex - 1]._Delay);

                            GUILayout.Label("DmgSkill1 : " + inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill1);
                            GUILayout.Label("Cooldown1 : " + inventoryPlayerList.playerList[viewIndex - 1]._Cooldown1);
                            GUILayout.Label("DmgSkill2 : " + inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill2);
                            GUILayout.Label("Cooldown2 : " + inventoryPlayerList.playerList[viewIndex - 1]._Cooldown2);
                           
                            inventoryPlayerList.playerList[viewIndex - 1].Price = EditorGUILayout.IntField("Price: ", inventoryPlayerList.playerList[viewIndex - 1].Price, GUILayout.ExpandWidth(false));
                            
                            if (GUI.changed)
                            {
                                EditorUtility.SetDirty(inventoryPlayerList);
                            }
                        }
                        GUILayout.EndScrollView();
                        break;
                    case 1:
                        scrollPositon = GUILayout.BeginScrollView(scrollPositon);

                        GUILayout.BeginHorizontal();
                        GUILayout.Label(" Shop Item Editor", EditorStyles.boldLabel);
                        if (inventoryItemList != null)
                        {
                            if (GUILayout.Button("Show Item List"))
                            {
                                EditorUtility.FocusProjectWindow();
                                Selection.activeObject = inventoryItemList;
                            }
                        }
                        if (GUILayout.Button("Open Item List"))
                        {
                            OpenItemList();
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.Space(20);

                        if (inventoryItemList != null)
                        {
                            GUILayout.BeginHorizontal();

                            GUILayout.Space(10);

                            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
                            {
                                if (viewIndex2 > 1)
                                    viewIndex2--;
                            }
                            GUILayout.Space(5);
                            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
                            {
                                if (viewIndex2 < inventoryItemList.itemlist.Count)
                                {
                                    viewIndex2++;
                                }
                            }

                            GUILayout.Space(60);

                            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
                            {
                                AddItem();
                            }
                            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
                            {
                                DeleteItem(viewIndex2 - 1);
                            }

                            GUILayout.EndHorizontal();
                            if (inventoryItemList.itemlist == null)
                                Debug.Log("wtf");
                            if (inventoryItemList.itemlist.Count > 0)
                            {
                                GUILayout.BeginHorizontal();
                                viewIndex2 = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex2, GUILayout.ExpandWidth(false)), 1, inventoryItemList.itemlist.Count);
                               
                                EditorGUILayout.LabelField("of   " + inventoryItemList.itemlist.Count.ToString() + "  Item", "", GUILayout.ExpandWidth(false));
                                GUILayout.EndHorizontal();


                                inventoryItemList.itemlist[viewIndex2 - 1].nameItem = EditorGUILayout.TextField("Item Name", inventoryItemList.itemlist[viewIndex2 - 1].nameItem as string);
                                inventoryItemList.itemlist[viewIndex2 - 1].image = EditorGUILayout.ObjectField("Item Image", inventoryItemList.itemlist[viewIndex2 - 1].image, typeof(Texture2D), false) as Texture2D;
                                inventoryItemList.itemlist[viewIndex2 - 1].effect = EditorGUILayout.TextField("Item Effect", inventoryItemList.itemlist[viewIndex2 - 1].effect as string);
                                inventoryItemList.itemlist[viewIndex2 - 1].price = EditorGUILayout.IntField(" Price ", inventoryItemList.itemlist[viewIndex2 - 1].price);

                            }
                          
                        }
                        if (GUI.changed)
                        {
                            EditorUtility.SetDirty(inventoryItemList);
                        }
                        GUILayout.EndScrollView();
                        break;
                }
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
        if (tab == 3)
        {
            viewIndex2 = 1;
            inventoryItemList = CreateInventoryItemList.Create3();
            if (inventoryItemList)
            {
                inventoryItemList.itemlist = new List<InventoryItem>();
                string relPath2 = AssetDatabase.GetAssetPath(inventoryItemList);
                EditorPrefs.SetString("ObjectPath3", relPath2);
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
            string absPath2 = EditorUtility.OpenFilePanel("Select Inventory Scene", "", "");
            if (absPath2.StartsWith(Application.dataPath))
            {
                string relPath2 = absPath2.Substring(Application.dataPath.Length - "Assets".Length);
                inventorySceneList = AssetDatabase.LoadAssetAtPath(relPath2, typeof(InventorySceneList)) as InventorySceneList;
                if (inventorySceneList == null)
                    inventorySceneList = new InventorySceneList();
                if (inventorySceneList)
                {
                    EditorPrefs.SetString("ObjectPath2", relPath2);
                }
            }
        }
        if (tab == 3)
        {
            string absPath2 = EditorUtility.OpenFilePanel("Select Inventory Item List", "", "");
            if (absPath2.StartsWith(Application.dataPath))
            {
                string relPath3 = absPath2.Substring(Application.dataPath.Length - "Assets".Length);
                inventoryItemList = AssetDatabase.LoadAssetAtPath(relPath3, typeof(InventoryItemList)) as InventoryItemList;
                if (inventoryItemList.itemlist == null)
                    inventoryItemList.itemlist = new List<InventoryItem>();
                if (inventoryItemList)
                {
                    EditorPrefs.SetString("ObjectPath3", relPath3);
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
        if (tab == 2)
        {
            InventoryScene newItem3 = new InventoryScene();
            
           
            inventorySceneList.scenelist.Add(newItem3);
            viewIndex3 = inventorySceneList.scenelist.Count;
           
        }
        if (tab == 3)
        {
            InventoryItem newItem2 = new InventoryItem();
            newItem2.nameItem = "New Item";

            inventoryItemList.itemlist.Add(newItem2);
            viewIndex2 = inventoryItemList.itemlist.Count;
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
        if (tab == 3)
        {
            inventoryItemList.itemlist.RemoveAt(index);
        }
        if (tab == 2)
        {
            inventorySceneList.scenelist.RemoveAt(index);
        }
    }
  
}