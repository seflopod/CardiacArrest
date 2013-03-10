using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {
	
	public AudioClip beatClip1;
	public AudioClip beatClip2;
	
	private  float cooler = 0.6F;
	private float cooler2= 2.0F;

	public float timer = 0.0F;
	public float timer2= 0.0F;
	public int selector=0;
	
	void heartBeatF(float _cooler)
	{
			if(timer>0)	
				timer -= Time.deltaTime;
			if(timer<0)	
				timer = 0;
			if(timer2>0)
				timer2 -= Time.deltaTime;
			if(timer2<0)
				timer2 = 0;

			if(timer==0){
				audio.PlayOneShot(beatClip1);
				timer2= 0.18F;
				timer = _cooler;
			}
			if(timer2==0){
				audio.PlayOneShot(beatClip2);
				timer2 = cooler2;
			}
	}
	void Update () {	
		if(selector>5)
			selector=0;
		selects(Player.soundSpeed);
		
		this.gameObject.light.color = Color.Lerp(Color.blue, Color.magenta, Mathf.PingPong(Time.time, cooler));
	}

	void selects(int _select)
	{
		switch(_select){
		case 0:	cooler=0.9f;
				heartBeatF(cooler);
				break;
		case 1: cooler=0.7f;
				heartBeatF(cooler);
				break;
		case 2:	cooler=0.5f;
				heartBeatF(cooler);
				break;
		case 3: cooler=0.4f;
				heartBeatF(cooler);
				break;
		case 4: cooler=0.3f;
				heartBeatF(cooler);
				break;
		case 5: cooler=0.1f;
				heartBeatF(cooler);
				break;
		default:break;
		}
	}

}
