/*
var crunchSound : AudioClip;
var crunchTimer : float = 0;
var crunchCooler: float = 0.6;//0.8;
var currentPitch: float = 0.0;
var randomPitch : float[];

var swoopSound : AudioClip;
var swoopTimer : float = 0;
var swoopCooler: float = 0.6;//0.8;

var gooshSound : AudioClip[];
var gooshTimer : float = 0;
var gooshCooler: float = 1.0;
var current : int;
static var theCurrent;

function Update () {
//Debug.Log(swoopCooler);// .LogWarning(swoopCooler);
//CRUNCH
//=================================================================
	if(Input.GetKey(KeyCode.W) && WalkingCrunch.CrunchBool == true){//to see if there is collision
		CrunchSound();
	}	
	if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && WalkingCrunch.CrunchBool == true){
		crunchCooler = 0.4;
		CrunchSound();
	}
	else
		crunchCooler = 0.6;
		//Check crunchTimer and cooler if they are greater or less than
		if(crunchTimer > 0){
			crunchTimer -= Time.deltaTime;
		}
		if(crunchTimer < 0){
			crunchTimer =0;
		}
	if(currentPitch>=randomPitch.Length)
		currentPitch=0;
	if(currentPitch<=randomPitch.Length)
		currentPitch=0;
	
//SWOOP
//=================================================================
	if(Input.GetKey(KeyCode.W) && WalkingSwoop.SwoopBool == true){//to see if there is collision
		SwoopSound();
	}
	if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && WalkingSwoop.SwoopBool == true){
		swoopCooler = 0.4;
		SwoopSound();
	}
	else
		swoopCooler = 0.6;	
		//Check crunchTimer and cooler if they are greater or less than
		if(swoopTimer > 0){
			swoopTimer -= Time.deltaTime;
		}
		if(swoopTimer < 0){
			swoopTimer =0;
		}
//Goosh
//=================================================================
	if(Input.GetKey(KeyCode.W) && WalkingGoosh.GooshBool == true){//to see if there is collision
		GooshSound();
	}
	if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && WalkingGoosh.GooshBool == true){
		gooshCooler = 0.8;
		GooshSound();
	}
	else
		gooshCooler = 1.0;	
		//Check crunchTimer and cooler if they are greater or less than
		if(gooshTimer > 0){
			gooshTimer -= Time.deltaTime;
		}
		if(gooshTimer < 0){
			gooshTimer =0;
		}
}

function CrunchSound(){
	if(crunchTimer == 0){
		currentPitch = randomPitch[Random.Range(0,randomPitch.Length)];
		audio.pitch = Mathf.Pow(2f,currentPitch/12.0f);
		//Debug.Log("CRUNCH PITCH"+currentPitch);
		audio.PlayOneShot(crunchSound);
		crunchTimer = crunchCooler;
	}
}
function SwoopSound(){
	if(swoopTimer == 0){
		audio.PlayOneShot(swoopSound);
		swoopTimer = swoopCooler;
	}
}
function GooshSound(){
	if(gooshTimer == 0){
		//audio.PlayOneShot(gooshSound[ Random.Range(0, gooshSound.Length) ]);
		current=Random.Range(0, gooshSound.Length);
		audio.PlayOneShot( gooshSound[current] );
		theCurrent = current;	//use this to manipulate other features.
				gooshTimer = gooshCooler;
	}
}
*/