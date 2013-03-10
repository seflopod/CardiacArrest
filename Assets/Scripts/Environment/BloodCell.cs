using UnityEngine;
using System.Collections;

public class BloodCell : MonoBehaviour
{
	#region statics	
	public static void GenerateCells(BloodCellData bcData)
	{
		GameObject newCellObj;
		GameObject sceneParent;
		BloodCell newCell;
		Vector3[] usingPath;
		int ubound;
		
		//Create a Random object, this is just to cut down on line length.
		System.Random dN = new System.Random();
		
		//These are all for modifying the spawned cell
		int matIdx = 0;
		Color rndColor = Color.red;
		string baseName = "BloodCell";
		sceneParent = GameObject.Find ("_BloodCellParent");
		
		for(int i=0;i<bcData.cellPrefabs.Length;++i)
		{
			ubound = bcData.numberOfCells/(i+1);
			usingPath = VectorPaths.PathAtIndex(i);
			
			for(int j=0;j<ubound;++j)
			{
				//Determine the random material and color for the new cell
				matIdx = dN.Next(bcData.cellMaterials.Length);
				rndColor = new Color((dN.Next(77)+67)/255.0f, dN.Next(15)/255.0f, dN.Next(15)/255.0f);
				
				//Spawn a new cell and texture it
				newCellObj = (GameObject) GameObject.Instantiate(bcData.cellPrefabs[i], Vector3.zero, Quaternion.identity);
				newCellObj.transform.localScale = new Vector3(bcData.cellXScale, 1.0f, bcData.cellXScale);
				newCellObj.GetComponent<MeshRenderer>().material = bcData.cellMaterials[matIdx];
				newCellObj.renderer.material.color = rndColor;
				
				//Name the cell so I can separate them in the inspector
				newCellObj.name = baseName + i.ToString() + j.ToString().PadLeft(4,'0');
				newCellObj.transform.parent = sceneParent.transform;
				
				//Position the cell on a path and give it a speed.
				newCell = newCellObj.AddComponent<BloodCell>();
				newCell.CellPath = usingPath;
				newCell.PercentOnPath = j/((float)ubound);
				newCell.Speed = 0.0f;
			}
		}
	}
	
	public static float BloodCellSpeed { get; set; }
	#endregion
	
	#region private_vars
	private float _pctOnPath;
	private Vector3[] _path;
	private bool _looped;
	#endregion
	
	#region unity_funcs
	
	private void Awake()
	{
		_pctOnPath = 0.0f;
		_looped = false;
	}
	
	private void Update()
	{
		if(_path != null)
		{
			//not the best way, I just didn't want to change a few lines if this
			//doesn't work as expected
			this.Speed = BloodCell.BloodCellSpeed;
			
			_pctOnPath += this.Speed*Time.deltaTime;
			if(gameObject.name.Contains("0000"))
				print (this.Speed + " " + _pctOnPath);
			if(_pctOnPath > 1.0f && !_looped)
			{
				_looped = true;
				_pctOnPath-=1.0f;
				if(gameObject.name.Contains("0000"))
					print ("New Pct:" + _pctOnPath);
				iTween.MoveUpdate(gameObject,
									iTween.PointOnPath(_path,_pctOnPath), 0.0f);
			}
			else
			{
				_looped = false;
				iTween.MoveUpdate(gameObject,
									iTween.PointOnPath(_path,_pctOnPath),
									this.Speed);
			}
			//iTween.PutOnPath(transform, _path, _pctOnPath);
		}
	}
	#endregion
	
	#region properties
	public float PercentOnPath
	{
		get { return _pctOnPath; }
		set
		{
			_pctOnPath = value;
			iTween.PutOnPath(transform, _path, value);
			
		}
	}
	
	public Vector3[] CellPath
	{
		get { return _path; }
		set { _path = value; }
	}
	
	public float Speed { get; set; }
	#endregion
}