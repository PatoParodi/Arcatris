using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteBrea : MonoBehaviour {

	public float movimientoPiso;
	public float velocidadBrea;
	public float velocidadPaddle;

	private GameObject pad;
	private float nuevaPosPad, nuevaPosBrea;
	private Vector3 nuevaPosicion;

	void Start(){
		//Busco el paddle para referenciarlo despues
		pad = GameObject.FindGameObjectWithTag ("paddle");

		//Establecer la nueva posicion de cada objeto
		nuevaPosBrea = transform.position.y;

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
//			StartCoroutine ("SubirPiso");

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

		nuevaPosicion += Vector3.up * movimientoPiso;
			
	//Subir brea cuando caen ladrillos

	}
		
	public IEnumerator SubirPiso(){
//		public void SubirPiso(){
			

//		//Establecer la nueva posicion de cada objeto
//		nuevaPosBrea = transform.position.y + movimientoPiso;
//		if(pad != null)
//			nuevaPosPad = pad.transform.position.y + movimientoPiso;

		//Subir piso de forma gradual para que no se vea un salto
		int partes = 5;
		float Porcion = movimientoPiso / partes;

		yield return new WaitForSeconds (0.5f);

		for(int i = 0; i<partes;i++){

			yield return new WaitForFixedUpdate();

			transform.position = new Vector3 (transform.position.x, 
				transform.position.y + Porcion, transform.position.z);

		}

		yield return new WaitForSeconds (0.1f);

		pad = GameObject.FindGameObjectWithTag ("paddle");
//		pad.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 10));
		if(pad != null)
			pad.transform.position = new Vector3 (pad.transform.position.x, pad.transform.position.y + movimientoPiso, pad.transform.position.z);

	}
}
