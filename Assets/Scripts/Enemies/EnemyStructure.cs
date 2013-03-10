using UnityEngine;
using System.Collections;
using System;

public class EnemyStructure : MonoBehaviour 
{	
	//Used for enemy speed adjustments\\
    private float speed = -.002f;		
	
	private void Awake()
	{
		animation.wrapMode = WrapMode.Loop;
	}
	
	public float EnemySpeed
	{
		get { return this.speed; }
		set { this.speed = value; }
	}
	
	private void Update()
	{
		this.transform.position-= new Vector3(speed, 0, 0);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			this.animation.Play("copbeat");			
			Application.Quit();
		}
	}
}
