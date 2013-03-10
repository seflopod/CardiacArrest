var sound1: AudioClip;
var sound2: AudioClip;
var rate  : float=0.2;
var sounds: AudioClip[];

private var audio1: AudioSource;
private var audio2: AudioSource;

function Start(){
  audio1 = gameObject.AddComponent(AudioSource);
  	//audio.timeSamples
  	audio1.audio.pitch = 0.5; 
  	audio1.clip = sound1;
  
  audio2 = gameObject.AddComponent(AudioSource);
  audio2.clip = sound2;
  
  CrossFade(audio2, audio1, rate);//0.6
}
function Update(){
	/*Debug.Log("ONE Pitch"+audio1.pitch);
	Debug.Log("TWO Pitch"+audio2.pitch);
	Debug.Log("ONE Volume"+audio1.volume);
	Debug.Log("TWO Volume"+audio2.volume);*/
}
function CrossFade(a1: AudioSource, a2: AudioSource, duration: float){
  var v0: float = a1.volume; // keep the original volume
  a2.Play(); // make sure a2 is playing
  var t: float = 0;
  while (t < 1){
    t = Mathf.Clamp01(t + Time.deltaTime / duration);
    a1.volume = (1 - t) * v0;
    a2.volume = t * v0;
    yield;
  }
}
	function OnCollisionEnter(){
  // crossfade sound2 to sound1 in 0.6 seconds
  CrossFade(audio2, audio1, 0.6);
  }