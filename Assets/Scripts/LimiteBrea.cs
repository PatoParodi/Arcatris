﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteBrea : MonoBehaviour {

	public float movimientoPiso;
	public float velocidadBrea;
	public float velocidadPaddle;

	private GameObject pad;
	private GameController controller;
	private float nuevaPosPad, nuevaPosBrea;

	void Start(){
		//Busco el paddle para referenciarlo despues
		pad = GameObject.FindGameObjectWithTag ("paddle");

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		//Establecer la nueva posicion de cada objeto
		nuevaPosBrea = transform.position.y;
	
	}

	void Update(){
//		
//		//Subir Brea
//		if(transform.position.y < nuevaPosBrea)
//			transform.position =  new Vector3 (transform.position.x, 
//				transform.position.y + velocidadBrea * Time.deltaTime , transform.position.z);
//
//		// Subir Paddle
//		if (pad == null) {
//			pad = GameObject.FindGameObjectWithTag ("paddle");
//			if(pad != null)
//				nuevaPosPad = pad.transform.position.y;
//		}
//		else
//			if (pad.transform.position.y < nuevaPosPad)
//				pad.transform.position = new Vector3 (pad.transform.position.x,
//					pad.transform.position.y + velocidadPaddle * Time.deltaTime, pad.transform.position.z);
//		
	}

	void OnTriggerEnter2D(Collider2D other){
	

		//Si pasa la pelota, Game Over
		if (other.gameObject.tag == "ball" && controller.ballInPlay) {

			StartCoroutine (finJuego(other.gameObject));

			// Volver a habilitar el Collider del paddle luego de perder (cuando la bola pasa muy por debajo)
//			pad = GameObject.FindGameObjectWithTag ("paddle");
//			if(pad != null)
//				pad.GetComponent<BoxCollider2D> ().enabled = true;	

		}

		//En caso que llegue al fondo, subir el piso
		if (other.gameObject.tag == "Caja") {
			Destroy (other.gameObject);
//			StartCoroutine ("SubirPiso");

		}

		//Destruir objeto prefab cuando pase
		if (other.gameObject.tag == "prefab") {
			Destroy (other.gameObject);
		}
			
	}

	public IEnumerator finJuego(GameObject ball){

		ball.GetComponent<CircleCollider2D> ().enabled = false;

		yield return new WaitForSecondsRealtime (0.3f);

		Destroy (ball);

		yield return new WaitForSecondsRealtime (0.5f);

		if(controller.ballInPlay)
			controller.gameOver ();

	}
		
	public IEnumerator SubirPiso(){

		//Subir piso de forma gradual para que no se vea un salto
		int partes = 5;
		float Porcion = movimientoPiso / partes;

		yield return new WaitForSeconds (0.5f);

		for(int i = 0; i<partes;i++){

			yield return new WaitForFixedUpdate();

			transform.position = new Vector3 (transform.position.x, 
				transform.position.y + Porcion, transform.position.z);

		}

//		yield return new WaitForSeconds (0.1f);

//		pad.GetComponent<Rigidbody2D> ().AddForceAtPosition (1, pad.transform.position);

//		pad = GameObject.FindGameObjectWithTag ("paddle");
////		pad.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 10));
//		if(pad != null)
//			pad.transform.position = new Vector3 (pad.transform.position.x, pad.transform.position.y + movimientoPiso, pad.transform.position.z);
//
	}
}
