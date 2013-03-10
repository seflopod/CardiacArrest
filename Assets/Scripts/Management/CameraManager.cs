using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager>
{
	public float playerRunningViewportPct = 0.1f;
	public float playerJumpingViewportPct = 0.75f;
	public float playerFallingViewportPct = 0.25f;
	public float verticalLag = 0.2f;
	public float maxFOV = 60.0f;
	public float minFOV = 25.0f;
	
	//private copies of public vars, used to shorten line length
	private float _maxPct;
	private float _runPct;
	private float _jmpPct;
	private float _falPct;
	private float _vLag;
	
	//field of view variables
	private float _targetFOV;
	private float _dFOV;
	
	//vertical motion variables
	private float _targetY;
	private float _vVelocity;
	//general position varaibles
	private Vector3 _curPos;
	private Vector3 _newPos;
	
	//delta time
	private float _dt;
	
	protected override void Awake()
	{
		base.Awake();
		
		//copy public vars to private versions
		_runPct = playerRunningViewportPct;
		_jmpPct = playerJumpingViewportPct;
		_falPct = playerFallingViewportPct;
		_vLag = verticalLag;
        
        _vVelocity = 0.0f;
		
		//determine the change in FOV based on speed
		//this is just a slope calculation using dFOV/dSpeed
		_dFOV = (minFOV - maxFOV) / (GameManager.Instance.movement.maxSpeed -
									GameManager.Instance.movement.minSpeed);
	}
	
	private void LateUpdate()
	{
		if(GameManager.State == GameState.PlayingGame)
		{
			_dt = Time.deltaTime;
			_curPos = transform.position;
			_newPos = _curPos;
			
			//Narrow the field of view as the player moves faster
			_targetFOV = _dFOV * (GameManager.Speed -
                                    GameManager.Instance.movement.maxSpeed) +
                                                                        minFOV;
            if(_targetFOV < camera.fieldofView)
			    camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, _targetFOV,
                                                Time.time);
			
			//Make sure the player is correctly positioned in the viewport
			_targetY = DetermineTargetY();
			if(_targetY != _newPos.y)
				_newPos.y = Mathf.SmoothDamp(_curPos.y, _newPos.y, _vVelocity,
                                            _vLag);
			
			transform.position = _newPos;
		}
	}
	
	private float DetermineTargetY()
	{
		Vector3 playerPos = Player.Instance.transform.position;
        float ret;
        
		switch(Player.State)
		{
		case PlayerState.Running:
            ret = camera.ViewportPointToWorld((new Vector3(0.5f, _runPct, playerPos.z))).y;
			break;
		case PlayerState.Jumping:
            Ray rayToPlayerY = camera.ViewportPointToRay((new Vector3(0.5f, playerPos.y, playerPos.z)));
			Ray rayToMaxHeight = camera.ViewportPointToRay((new Vector3(0.5f, _jmpPct, playerPos.z)));
            float angleToMaxRay = Vector3.Angle(rayToPlayerY.direction, rayToMaxHeight.direction);
            
            //check to see if the angle is near some threshold, which can be
            //made into a variable at some point in the future.
            if(angleToMaxRay <= 1.0f)
                ret = playerPos.y;
			break;
		case PlayerState.Falling:
			Ray rayToPlayerY = camera.ViewportPointToRay((new Vector3(0.5f, playerPos.y, playerPos.z)));
    		Ray rayToMinHeight = camera.ViewportPointToRay((new Vector3(0.5f, _falPct, playerPos.z)));
            float angleToMinRay = Vector3.Angle(rayToMinHeight.direction, rayToPlayerY.direction);
            
            //check to see if the angle is near some threshold, which can be
            //made into a variable at some point in the future.
            if(angleToMinRay <= 1.0f)
                ret = playerPos.y;
			break;
		default:
                ret = _newPos.y;
			break;
		}
		print (ret);
		return ret;
	}
}