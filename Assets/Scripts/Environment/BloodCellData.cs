using UnityEngine;
using System;

[Serializable]
public class BloodCellData
{
	public int numberOfCells = 60;
	public float cellXScale = 1.0f;
	public GameObject[] cellPrefabs;
	public Material[] cellMaterials;
	public float movementSpeed = 0.25f;
}

