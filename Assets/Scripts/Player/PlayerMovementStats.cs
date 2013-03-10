using UnityEngine;
using System;

[Serializable]
public class PlayerMovementStats
{
	public float minSpeed = 1.0f;
	public float maxSpeed = 3.4f;
	public float sprintIncrease = 0.075f;
	public float speedDampen = 0.005f;
	public float jumpSpeed = 9.0f;
	public float gravity = 20.0f;
}
