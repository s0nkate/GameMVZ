using UnityEngine;
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
}