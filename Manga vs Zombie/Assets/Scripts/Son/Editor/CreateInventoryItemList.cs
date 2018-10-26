using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class CreateInventoryItemList
{
    [MenuItem("Assets/Create/Inventory Player List")]
    public static InventoryPlayerList Create()
    {
        InventoryPlayerList asset = ScriptableObject.CreateInstance<InventoryPlayerList>();

        AssetDatabase.CreateAsset(asset, "Assets/InventoryPlayerList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
    [MenuItem("Assets/Create/Inventory Enemy List")]
    public static InventoryEnemyList Create1()
    {
        InventoryEnemyList asset1 = ScriptableObject.CreateInstance<InventoryEnemyList>();

        AssetDatabase.CreateAsset(asset1, "Assets/InventoryEnemyList.asset");
        AssetDatabase.SaveAssets();
        return asset1;
    }
    [MenuItem("Assets/Create/Inventory Scene")]
    public static InventorySceneList Create2()
    {
        InventorySceneList asset2 = ScriptableObject.CreateInstance<InventorySceneList>();

        AssetDatabase.CreateAsset(asset2, "Assets/InventoryScene.asset");
        AssetDatabase.SaveAssets();
        return asset2;
    }
    [MenuItem("Assets/Create/Inventory Item")]
    public static InventoryItemList Create3()
    {
        InventoryItemList asset3 = ScriptableObject.CreateInstance<InventoryItemList>();

        AssetDatabase.CreateAsset(asset3, "Assets/InventoryItem.asset");
        AssetDatabase.SaveAssets();
        return asset3;
    }
}