using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour 
{	
	private GameObject player;
	
	// Update is called once per frame
	void LateUpdate () 
	{		
		if(!player)
			player = GameObject.FindWithTag("Player");
		
			this.transform.LookAt(player.transform);
		
	}
}
