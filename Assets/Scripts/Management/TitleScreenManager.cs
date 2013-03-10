using UnityEngine;
using System.Collections;

public class TitleScreenManager : Singleton<TitleScreenManager>
{
	private enum RunnerStates { Running, Sprinting, Dead };
	private enum JumperStates { Idle, Jumping };

	private GameObject _runner;
	private GameObject _jumper;
	private float _speed;
	private float _heartRate;
	private RunnerStates _runnerState;
	private JumperStates _jumperState;
	private bool _jumped;
		
	protected override void Awake()
	{
		base.Awake();
	}
	
	private void Start()
	{
		_speed = GameManager.Instance.movement.minSpeed;
		_heartRate = GameManager.Instance.heartStats.minHeartRate;
		_runner = null;
		_jumper = null;
		_runnerState = RunnerStates.Running;
		_jumperState = JumperStates.Idle;
		
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject go in gos)
		{
			if(go.name.Equals("JumpingHeart"))
				_jumper = go;
			else if(go.name.Equals("RunningHeart"))
				_runner = go;
			
			if(_jumper != null && _runner != null)
				break;
		}
		
		_runner.animation["beat"].blendMode = AnimationBlendMode.Additive;
		_jumper.animation["jump"].speed = 2;
		_jumper.animation["beat"].wrapMode = WrapMode.Loop;
		_jumper.animation.Play("beat");
	}
	
	private void Update()
	{
		switch(_runnerState)
		{
		case RunnerStates.Running:
			Running();
			break;
		case RunnerStates.Sprinting:
			Sprinting();
			break;
		case RunnerStates.Dead:
			_runnerState = RunnerStates.Running;
			_speed = GameManager.Instance.movement.minSpeed;
			_heartRate = GameManager.Instance.heartStats.minHeartRate;
			Running();
			break;
		default:
			_runnerState = RunnerStates.Running;
			break;
		}
		
		switch(_jumperState)
		{
		case JumperStates.Idle:
			
			break;
		case JumperStates.Jumping:
			Jumping();
			break;
		default:
			_jumperState = JumperStates.Idle;
			break;
		}
	}
	
	public void Running()
	{
		_runner.animation["run"].speed = _speed/1.6f;	//Matt:added 5:16pm
		_runner.animation["beat"].speed = _speed/2.0f;//Matt:added 5:16pm
		
		if(_heartRate <= GameManager.Instance.heartStats.minHeartRate)	
			_heartRate=GameManager.Instance.heartStats.minHeartRate;
		else
			_heartRate--;
		
		if(_speed < GameManager.Instance.movement.minSpeed)
			_speed = GameManager.Instance.movement.minSpeed;
		else
			_speed-=GameManager.Instance.movement.speedDampen;
		
		_runner.animation.CrossFade("run");
		_runner.animation.Blend("beat", 2.0f);
	}
	
	
	
	public void Sprinting()
	{
		if(_speed < GameManager.Instance.movement.maxSpeed)
			_speed+=GameManager.Instance.movement.sprintIncrease;
		else
			_speed=GameManager.Instance.movement.maxSpeed;
		
		if(_heartRate <= GameManager.Instance.heartStats.maxHeartRate &&
			_heartRate >= GameManager.Instance.heartStats.minHeartRate && _speed >= 1.5f)
		{
			_heartRate+=1f;
		}
		else if(_heartRate <= GameManager.Instance.heartStats.maxHeartRate &&
				_heartRate >= GameManager.Instance.heartStats.minHeartRate && _speed >= 2.5f)
		{
			_heartRate+=1.5f;
		}
		else
			_heartRate+=.2f;
		
		if(_heartRate >= GameManager.Instance.heartStats.maxHeartRate)
		{
			_runnerState = RunnerStates.Dead;
		}
		
		_runner.animation.CrossFade("run");
		_runner.animation.Blend("beat", 2.0f);
		
	}
	
	public void Jumping()
	{
		_jumper.animation.CrossFade("jump");
		_jumperState = JumperStates.Idle;
	}
	
	public void Idle()
	{
		_jumper.animation.Play("beat");
	}
	
	/*
	private void Dead()
	{			
 		if(timer < this.animation["die"].length)
		{
			this.animation.CrossFade("die");
			timer+=.01f;//just some random magic number
		}
		
		if (timer >= this.animation["die"].length)
		{
			Instantiate(explodePrefab, this.transform.position, Quaternion.identity);
			Destroy(GameObject.FindWithTag("Player"));
			timer = 0;			
			
			this.transform.position = startPos;
			playerState = PlayerState.Running;
			HeartBeat = MIN_HEARTBEAT;
			PlayerSpeed = MIN_SPEED;
		}		
	}*/
}
