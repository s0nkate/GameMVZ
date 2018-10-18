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
    [MenuItem("Assets/Create/Inventory Map")]
    public static InventoryMap Create2()
    {
        InventoryMap asset2 = ScriptableObject.CreateInstance<InventoryMap>();

        AssetDatabase.CreateAsset(asset2, "Assets/InventoryMap.asset");
        AssetDatabase.SaveAssets();
        return asset2;
    }
}