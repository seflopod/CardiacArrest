//CreateCameraLocationAsset.cs
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class BloodCellLayer
{
	
}

public class BloodCellLayerHolder : ScriptableObject
{
	List<BloodCellLayer> layers;
	
	//I don't think it likes ctors for SOs.  Need to fix
	public BloodCellLayerHolder(List<BloodCellLayer> layers)
	{
		this.layers = layers;
	}
}

public class CreateBloodCellLayerData
{
    [MenuItem("Custom/Create new BloodCell layer information")]
    public static void CreateMyAsset()
    {
        List<BloodCellLayer> bloodCellLayer = new List<BloodCellLayer>();
        BloodCellLayerHolder asset = new BloodCellLayerHolder(bloodCellLayer);  //scriptable object 
        AssetDatabase.CreateAsset(asset, "Assets/BloodCellLayerData.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}