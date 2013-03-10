using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour 
{
	
	private float zzz=0.4f;	
	
	void Update()
	{

		this.gameObject.light.color = Color.Lerp(Color.blue, Color.magenta, Mathf.PingPong(Time.time, zzz));
	}
}
