#pragma strict

var TownVolume = 1.0;
var isStartingTown = false;

private var FirstTime = true;
private var AmbientControllerScript : AmbientController;
private var Ambient : AudioSource;

function Start ()
{
	AmbientControllerScript = GameObject.FindGameObjectWithTag("Scripts").GetComponent.<AmbientController>();
 	Ambient = gameObject.GetComponent.<AudioSource>();
}

function OnTriggerEnter (other : Collider)
{
	if (other.tag == "Player" && isStartingTown && !FirstTime && !Ambient.isPlaying)
  	{
  		AmbientControllerScript.VolumeToSet = TownVolume;
  		AmbientControllerScript.FadeAmbients(gameObject.GetComponent.<AudioSource>());
  	}
 	else if (other.tag == "Player" && !isStartingTown && !Ambient.isPlaying)
  	{
  		AmbientControllerScript.VolumeToSet = TownVolume;
  		AmbientControllerScript.FadeAmbients(gameObject.GetComponent.<AudioSource>());
  	}
 	FirstTime = false;
}