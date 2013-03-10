using UnityEngine;
using System;
using System.Collections;

public enum PlayerState
{
	Running,
	Jumping,
	Dead
};

public class Player : Singleton<Player>
{
	public AudioClip explode;
	public static int soundSpeed = 0;
    
    private static PlayerState _state;
    
    private Vector3 _startPos;
    private SkinnedMeshRenderer _meshRenderer;
    private float _timer;
    
    protected override void Awake()
    {
        base.Awake();
        _startPos = transform.position;
        _meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        animation.wrapMode = WrapMode.Loop;
        animation["beat"].blendMode = AnimationBlendMode.Additive;
        animation["jump"].speed = 2;
    }
    
    private void Update()
    {
        //This series of ifs should be broken out to the GameManager class
        //I'm just too lazy to mess with sound atm.
        if(GameManager.Speed>1.0f && GameManager.Speed<1.3f){soundSpeed=0;}
		if(GameManager.Speed>1.3f && GameManager.Speed<1.6f){soundSpeed=1;}
		if(GameManager.Speed>1.6f && GameManager.Speed<1.9f){soundSpeed=2;}
		if(GameManager.Speed>1.9f && GameManager.Speed<2.2f){soundSpeed=3;}
		if(GameManager.Speed>2.5f && GameManager.Speed<2.8f){soundSpeed=4;}
		if(GameManager.Speed>3.1f && GameManager.Speed<3.4f){soundSpeed=5;}
        
        switch(_state)
        {
            case PlayerState.Running:
                Running();
                break;
            case PlayerState.Jumping:
                animation.CrossFade("jump");
				animation["beat"].speed = GameManager.Speed/2.0f; //shouldn't this be tuned to HeartRate?
                break;
            case PlayerState.Dead:
                break;
            default:
                break;
        }
    }
    
    private void Running()
    {
        animation["run"].speed = GameManager.Speed/1.6f;
        animation["beat"].speed = GameManager.Speed/2.0f; //shouldn't this be tuned to HeartRate?
        animation.CrossFade("run");
        animation.Blend("beat", 2.0f);
    }
    
    public static PlayerState State
    {
        get { return _state; }
        set { _state = value; }
    }
	
}