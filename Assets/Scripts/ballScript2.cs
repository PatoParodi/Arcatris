using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript2 : MonoBehaviour {

	string bolaElegida = "Pelotas/" + LevelManager.levelManager.numeroBolaElegida;


	public void mostrarPelota(){

//		if (bolaElegida == "")
//			bolaElegida = "00";
//		GetComponent<SpriteRenderer> ().sprite = Resources.Load (bolaElegida,typeof(Sprite)) as Sprite;

		GetComponentInParent<ballScript> ().pelotaSpawneada = true;
	
	}

	//Se llama desde la animacion de Spawn
	public void afterSpawn(){

		GetComponentInParent<ballScript> ().pelotaSpawneada = true;

	}

		
}
