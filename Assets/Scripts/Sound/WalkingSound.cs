using UnityEngine;
using System.Collections;
//WalkingSound script by:Matt Sponholz
//meant to be used with sound and interaction in Excellent ways 
public class WalkingSound : MonoBehaviour {
	public AudioClip runClip;
	
	public float timer = 0.0F;
	private float _cooler=0.5F;
	
	public float currentPitch = 0.0F;
	public float[] randomPitch;
	public int walkSelect = 0;
	
	void playWalk(AudioClip audioClip,float cooler)
	{ 
		if(timer>0)
			timer -= Time.deltaTime;
		if(timer<0)
			timer = 0;
		if(currentPitch>=randomPitch.Length)
			currentPitch=0;
		if(timer==0){
			currentPitch=randomPitch[Random.Range(0,randomPitch.Length)];
			audio.pitch = Mathf.Pow(2f,currentPitch/12.0f);//a good algorithm for setting near-exact pitch
			audio.PlayOneShot(audioClip);
			timer = cooler;
		}
	}
	void OnTriggerStay(Collider other)
	{
		//CharacterController dummy = (CharacterController) other;
		
		
			if(other.gameObject.tag == "GroundTag"){
				walkSpeedSelect(Player.soundSpeed);
					playWalk(runClip,_cooler);
			}
	}
	void OnTriggerExit(Collider other)
	{// RESETS DEFAULTS
		timer = 0.0F;
		_cooler=.4F;
		currentPitch = 0.0F;
	}
	void Update()
	{
		if(Player.soundSpeed>5)
			Player.soundSpeed=0;
		
	}
	void walkSpeedSelect(int _select)
	{
		switch(_select){
		case 0:	_cooler=0.54F;
				break;
		case 1: _cooler=0.40F;
				break;
		case 2:	_cooler=0.34F;
				break;
		case 3: _cooler=0.28F;
				break;
		case 4: _cooler=0.22F;
				break;
		case 5:	_cooler=0.2F;
				break;
		default:break;
		}
	}
	
}
