using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager>
{
	private Vector3 _curPos;
	private Vector3 _newPos;
	
	protected override void Awake()
	{
		base.Awake();
	}
	
	private void Update()
	{
		//Move the camera closer to the player as the speed increases
		_curPos = transform.position;
        _newPos = new Vector3(_curPos.x, _curPos.y, 3.75f*GameManager.Speed-18.75f);
		transform.position = _newPos;
	}
}