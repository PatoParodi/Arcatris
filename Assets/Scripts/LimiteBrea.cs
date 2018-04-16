using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteBrea : MonoBehaviour {

	public float movimientoPiso;
	public float velocidadBrea;

	private GameObject pad;
	private Vector3 nuevaPosicion;

	void Start(){
		//Busco el paddle para referenciarlo despues
		pad = GameObject.FindGameObjectWithTag ("paddle");

		nuevaPosicion = gameObject.transform.position;
	
	}

	void Update(){
		
//		//Subir Brea
		gameObject.transform.position = Vector3.Lerp (transform.position, nuevaPosicion , velocidadBrea * Time.deltaTime);

	}

	void OnTriggerEnter2D(Collider2D other){
	

		//Si pasa la pelota, Game Over
		if (other.gameObject.tag == "ball") {
			Destroy (other.gameObject);
			if(GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().ballInPlay)
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().gameOver ();
			// Volver a habilitar el Collider del paddle luego de perder (cuando la bola pasa muy por debajo)
//			pad = GameObject.FindGameObjectWithTag ("paddle");
//			if(pad != null)
//				pad.GetComponent<BoxCollider2D> ().enabled = true;	

		}

		//En caso que llegue al fondo, subir el piso
		if (other.gameObject.tag == "Caja") {
			Destroy (other.gameObject);

		}

		//Destruir objeto prefab cuando pase
		if (other.gameObject.tag == "prefab") {
			Destroy (other.gameObject);
		}
			
	}

	public void moverBrea(Vector3 posicion){
	//Mueve la brea hacia la nueva posicion
		nuevaPosicion = posicion;
//		gameObject.transform.position = Vector3.Lerp (transform.position, nuevaPosicion, velocidadBrea * Time.deltaTime);

	}

	public void subirBrea(){
		//Subir brea cuando caen ladrillos

		nuevaPosicion += Vector3.up * movimientoPiso;
			
	}

	public void bajarBrea(float multiplicador){

		nuevaPosicion -= Vector3.up * movimientoPiso * multiplicador;

	}
		
}
