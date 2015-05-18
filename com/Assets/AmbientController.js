#pragma strict

var VolumeToSet = 1.0; //-- This should be the volume of the ambient of the current town. Adjust it in the editor.
var TransitionSpeed = 1.0; //-- The higher the number the faster the volumes will fade.

private var TownAmbientsGO : GameObject[];
private var TownAmbients : AudioSource[];
private var counter = 0.0;

function Start ()
 {
 TownAmbientsGO = GameObject.FindGameObjectsWithTag("TownAmbient");
 TownAmbients = new AudioSource[TownAmbientsGO.Length];
 for (var i : int = 0; i < TownAmbientsGO.Length; i++)
  {
  		TownAmbients[i] = TownAmbientsGO[i].GetComponent.<AudioSource>();
  		if (!TownAmbients[i].transform.GetComponent.<TownMusic>().isStartingTown)
  		{
	   		TownAmbients[i].volume = 0.0;
   			TownAmbients[i].enabled = false;
   		}
	}
}

function FadeAmbients (x : AudioSource)
 {
 for (i in TownAmbients)
   {
   if (i.enabled)
    {
    var LastTown = i;
    }
   }
 //-- Fade out the audio of the last town while fading in the audio of the current town
 counter = 0.0;
 var LastTownVolume = LastTown.volume;
 var ThisTownAmbient = x; //-- Keep all AudioSources of all the other towns disabled to keep things efficient. Only enable them when their volume is not 0.0, so when the [player enters the town.
 ThisTownAmbient.enabled = true;
 while (counter <= 1.0)
  {
  LastTown.volume = Mathf.SmoothStep(LastTownVolume, 0.0, counter);
  ThisTownAmbient.volume = Mathf.SmoothStep(0.0, VolumeToSet, counter); //-- SmoothStep will ensure that the volume change is smooth instead of linear.
  counter += Time.deltaTime * TransitionSpeed;
  yield; //-- necessary to make sure that the game does not freeze while in the while loop
  }
 LastTown.enabled = false; //-- Disable the last town AudioSource to "unload" it from the memory.
 }