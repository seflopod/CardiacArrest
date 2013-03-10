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
			if(i==0)
				ubound = bcData.numberOfCells;
			else
				ubound = bcData.numberOfCells/(2*i); //4 is so fucking arbitrary.
			
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
				
				//Position the cell on a give it speeds.
				newCell = newCellObj.AddComponent<BloodCell>();
				newCell.Init(Vector3.right*j*(bcData.numberOfCells/ubound),
								0.0f,
								bcData.numberOfCells*bcData.cellXScale,
								0.0f, i*5.0f); //arbitrary and subject to change
				newCell.HorizontalSpeed = 0.0f;
				newCell.VerticalSpeed = 0.0f;
			}
		}
	}
	
	//these are for general movement, but with the instance vars there can be variance
	//if so desired.
	public static float BloodCellHorizSpeed { get; set; }
	public static float BloodCellVertSpeed { get; set; }
	#endregion
	
	#region private_vars	
	private float _minX;
	private float _maxX;
	private float _minY;
	private float _maxY;
	private float _avgY;
	private bool _moveVertical;
	private bool _lowTarget;
	private bool _highTarget;
	private float _yTarget;
	private float _hSpeed;
	private float _vSpeed;
	private float _deltaTime;
	private Vector3 _newPos;
	#endregion
	
	#region unity_funcs
	private void Update()
	{
		_hSpeed = BloodCellHorizSpeed;
		_vSpeed = BloodCellVertSpeed;
		
		_deltaTime = Time.deltaTime;
		
		//move the cell and adjust if it reaches its bounds
		_newPos = transform.position;
		_newPos.x+=_hSpeed * _deltaTime;
		if(_hSpeed < 0)
		{
			if(_newPos.x <= _minX)
				_newPos.x = _maxX - _minX + _newPos.x;
		}
		else if(_hSpeed > 0)
		{
			//I haven't checked to see if this works, so the signs might be off
			if(_newPos.x >= _maxX)
				_newPos.x = _minX - _maxX + _newPos.x;
		}
		
		if(_moveVertical)
		{
			_newPos.y += ((_yTarget<_avgY)?-1:1) * _vSpeed * _deltaTime;
			if((_yTarget < _avgY && _newPos.y <= _yTarget) || 
				(_yTarget > _avgY && _newPos.y >= _yTarget))
				SetNewYTarget();
		}
		transform.position = _newPos;
	}
	#endregion
	
	public void Init(Vector3 startPos, float minX, float maxX, float minY,
						float maxY)
	{
		_minX = minX;
		_maxX = maxX;
		_minY = minY;
		_maxY = maxY;
		_avgY = (maxY + minY) / 2;
		_moveVertical = (minY != maxY);
		_highTarget = false;
		_lowTarget = false;
		_yTarget = _avgY;
		if(_moveVertical)
		{
			SetNewYTarget();
			startPos.y = _avgY;
		}
		transform.position = startPos;
	}
	
	private void SetNewYTarget()
	{
		do
		{
			if(_lowTarget)
			{
				_yTarget = Random.Range(_avgY, _maxY);
			}
			else if(_highTarget)
			{
				_yTarget = Random.Range(_minY, _avgY);
			}
			else
			{
				_yTarget = Random.Range(_minY, _maxY);
			}
		} while(_yTarget == _avgY);
		
		if(_yTarget < _avgY)
		{
			_lowTarget = true;
			_highTarget = false;
		}
		else
		{
			_lowTarget = false;
			_highTarget = true;
		}
	}
	
	#region properties
	public float VerticalSpeed
	{
		get { return _vSpeed; }
		set { _vSpeed = value; }
	}
	
	public float HorizontalSpeed
	{
		get { return _hSpeed; }
		set { _hSpeed = value; }
	}
	#endregion
}