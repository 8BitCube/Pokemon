using UnityEngine;
using System.Collections;

public class SpawnArea : MonoBehaviour {

	public int[] spawnables;
	public bool spawning;

	public float rangeMin,rangeMax,delayMin,delayMax;
	public int spawnMin,spawnMax;

	public int spawned;

	void Start(){
		StartCoroutine(Spawning());
	}

	IEnumerator Spawning(){

		BoxCollider bc = GetComponent<BoxCollider>();
		float newDelay = Random.Range(delayMin,delayMax);

		yield return new WaitForSeconds(newDelay);

		while(spawning)
		{
			Vector3 correctPosition = transform.position-(bc.size/2)+new Vector3(0,bc.size.y/2,0);

			int creating = spawnables[Random.Range(0,spawnables.Length)];
			int amount = Random.Range(spawnMin,spawnMax);
			float range = Random.Range(rangeMin,rangeMax);

			Vector3 pos = new Vector3(Random.Range(0,bc.size.x),0,Random.Range(0,bc.size.z));

			int tries = 16;

			RaycastHit rh;

			while(Physics.SphereCast((correctPosition+pos),range,new Vector3(),out rh)){
				if(tries<=0){
					pos=new Vector3(bc.size.x/2,0,bc.size.z/2);
					break;
				}
				pos = new Vector3(Random.Range(0,bc.size.x),-0.125f,Random.Range(0,bc.size.z));
				tries--;
			}

			if(Vector3.Distance(GameManager.Instance.Player.transform.position,correctPosition+pos)>=15)
			if(spawned<=15)
			for(int i = 0; i < amount; i ++){
				GameObject newPokemon = (GameObject)Instantiate(PokemonDatabase.basis,correctPosition+pos+new Vector3(Random.insideUnitCircle.x*range,0,Random.insideUnitCircle.y*range),Quaternion.identity);

				newPokemon.name=PokemonDatabase.baseStats[creating].name;

				newPokemon.GetComponent<Pokemon>().SetPokemon(creating);
				spawned+=1;
			}


			yield return new WaitForSeconds(Random.Range(delayMin,delayMax));
		}
	}
}
