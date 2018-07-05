using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript2 : MonoBehaviour {

	//Se llama desde la animacion de Spawn
	public void afterSpawn(){

		GetComponentInParent<ballScript> ().pelotaSpawneada = true;

	}

}
