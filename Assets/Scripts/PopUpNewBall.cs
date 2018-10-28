using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpNewBall : MonoBehaviour {

	public string NumeroDeBola;
	public Transform ballPosition;

	GameObject bolaInstanciada;


	public void OnEnable(){
	

	
	}

	public void InstanciarBola () {

		//Instanciar bola 
		bolaInstanciada = Instantiate (Resources.Load ("Pelotas/" + NumeroDeBola) as GameObject, ballPosition);
		bolaInstanciada.transform.localScale = new Vector3 (850, 850, 0);

		//Modificar los sorting layers
		SpriteRenderer[] sprites = bolaInstanciada.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sprite in sprites) {
		
			sprite.sortingOrder += 20;
		
		}

	}

	public void DestruirBolaInstanciada(){
	//Llamar desde el Animador al terminar
		Destroy(bolaInstanciada);

	}

}
