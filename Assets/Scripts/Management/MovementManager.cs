using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MovementManager : Singleton<MovementManager>
{
	protected override void Awake()
	{
		base.Awake();
	}
	
	//The way the title screen handles movement needs to change
	//I'm thinking that there should be two controllers, one per heart.  These will each be told
	//what to do via this or a similar function, much like PlayerMovement below.
	//Keep in mind the changes Adam has made, those needed to be folded in.
    public void TitleScreenMovement(bool doSprint, bool doJump)
    {
        if(doSprint)
            TitleScreenManager.Instance.Sprinting();
        else
            TitleScreenManager.Instance.Running();
        
        if(doJump)
            TitleScreenManager.Instance.Jumping();
        else
            TitleScreenManager.Instance.Idle();
    }
	
	// <summary>
	/// Tells the Player how to move
	/// </summary>
	/// <param name='doSprint'>
	/// True if the Player should sprint; false otherwise.
	/// </param>
	/// <param name='doJump'>
	/// True if the Player should jump; false otherwise.
	/// </param>
	/// <description>
	/// This takes the bools passed from Input methods in InputController and
	/// uses them to adjust background stats and to tell the Player object if it
	/// should move and what animations it should play.
	/// </description>
    public void PlayerMovement(bool doSprint, bool doJump)
    {
		PlayerMovementStats movement = GameManager.Instance.movement;
		PlayerHeartStats heartStats = GameManager.Instance.heartStats;
		
        if(doJump)
        {
            if(Player.State != PlayerState.Jumping)
            {
                Player.State = PlayerState.Jumping;
                Player.Instance.transform.gameObject.rigidbody.AddForce(0.0f,
                                                            movement.jumpSpeed,
                                                            0.0f,
                                                            ForceMode.Impulse);
                //GameManager.HeartRate*=1.2f; //arbitrary and subject to change
            }
			
			//If we're at or near a platform, we're not jumping; we're running.
            if(Physics.Raycast(Player.Instance.transform.position,-Vector3.up,
								0.1f))
                Player.State = PlayerState.Running;
        }
		if(doSprint)
		{
			//If we're sprinting, we're running but also increasing speed
			//and heart rate
			GameManager.Speed+=movement.sprintIncrease;
            
            if(GameManager.Speed >= movement.maxSpeed)
                GameManager.Speed = movement.maxSpeed;
            
			GameManager.HeartRate+=(6 + (GameManager.Speed-0.5f)/2);
			
			//Since the only place we are currently affecting heart rate is
			//when we're sprinting, check for death here.
			if(GameManager.HeartRate > heartStats.maxHeartRate)
			{
				//die
			}
		}
		else
		{
			//The only time heartRate will go down is when we're NOT sprinting
			GameManager.HeartRate-=heartStats.heartRateDecrease;
			if(GameManager.HeartRate < heartStats.minHeartRate)
				GameManager.HeartRate = heartStats.minHeartRate;
			
			//Superflous?
			//Player.State = PlayerState.Running;
		}
		
		//No matter what our speed is always decreasing, think of drag
		//But we never go backwards
		if(GameManager.Speed >= movement.speedDampen)
			GameManager.Speed-=movement.speedDampen;
		
		BloodCell.BloodCellSpeed = GameManager.Speed / 10;
	}
}