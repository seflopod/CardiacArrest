using UnityEngine;
using System;

[Serializable]
public class PlayerHeartStats
{
	public float maxHeartRate = 205.0f;
	public float minHeartRate = 98.0f;
	public float heartRateIncreaseMultiplier = 0.8f;
	public float heartRateDecrease = -0.6f;
}
