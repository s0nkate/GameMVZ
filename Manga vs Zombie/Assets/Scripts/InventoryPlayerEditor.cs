using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class InventoryItemEditor : EditorWindow
{

    public InventoryPlayerList inventoryPlayerList;
    private int viewIndex = 1;
    private string[] toolBar = new string[] { "Player", "Enemy", "Map", "Shop", "sence" };
    int tab;
    float myFloat = 5;
    bool mybool = false;
    [MenuItem("Window/Custom Inspector %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(InventoryItemEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            inventoryPlayerList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(InventoryPlayerList)) as InventoryPlayerList;
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

                if (inventoryPlayerList == null)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    if (GUILayout.Button("Create New Player List", GUILayout.ExpandWidth(false)))
                    {
                        CreateNewItemList();
                    }
                    if (GUILayout.Button("Open Existing Player List", GUILayout.ExpandWidth(false)))
                    {
                        OpenItemList();
                    }
                    GUILayout.EndHorizontal();
                }

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
                        inventoryPlayerList.playerList[viewIndex - 1]._image = EditorGUILayout.ObjectField("Item Icon", inventoryPlayerList.playerList[viewIndex - 1]._image, typeof(Texture2D), false) as Texture2D;


                        inventoryPlayerList.playerList[viewIndex - 1]._Dmg = EditorGUILayout.FloatField("Dmg ", inventoryPlayerList.playerList[viewIndex - 1]._Dmg, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Delay = EditorGUILayout.FloatField("Delay ", inventoryPlayerList.playerList[viewIndex - 1]._Delay, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill1 = EditorGUILayout.FloatField("DmgSkill1 ", inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill1, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Cooldown1 = EditorGUILayout.FloatField("Cooldown1 ", inventoryPlayerList.playerList[viewIndex - 1]._Cooldown1, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill2 = EditorGUILayout.FloatField("DmgSkill2 ", inventoryPlayerList.playerList[viewIndex - 1]._DmgSkill2, GUILayout.ExpandWidth(false));
                        inventoryPlayerList.playerList[viewIndex - 1]._Cooldown2 = EditorGUILayout.FloatField("Cooldown2 ", inventoryPlayerList.playerList[viewIndex - 1]._Cooldown2, GUILayout.ExpandWidth(false));
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
    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        inventoryPlayerList = CreateInventoryItemList.Create();
        if (inventoryPlayerList)
        {
            inventoryPlayerList.playerList = new List<InventoryPlayer>();
            string relPath = AssetDatabase.GetAssetPath(inventoryPlayerList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
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

    void AddItem()
    {
        InventoryPlayer newItem = new InventoryPlayer();
        newItem._Name = "New Item";
        //newItem._Id = inventoryPlayerList.playerList.Count;
        inventoryPlayerList.playerList.Add(newItem);
        viewIndex = inventoryPlayerList.playerList.Count;
    }

    void DeleteItem(int index)
    {
        inventoryPlayerList.playerList.RemoveAt(index);
    }
}