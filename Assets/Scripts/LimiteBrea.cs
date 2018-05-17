using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteBrea : MonoBehaviour {

	public float movimientoPiso;
	public float velocidadBrea;
	public Transform PosicionInicial;
	public ParticleSystem particulasBrea;

	private Vector3 nuevaPosicion;
	public bool BreaEnPosicionInicial;

	void Start(){

		nuevaPosicion = gameObject.transform.position;
	
	}

	void Update(){
		
//		//Subir Brea
		gameObject.transform.position = Vector3.Lerp (transform.position, nuevaPosicion , velocidadBrea * Time.deltaTime);

		if (Vector3.Distance (gameObject.transform.position, PosicionInicial.position) <= 0.05f)
			BreaEnPosicionInicial = true;
		else
			BreaEnPosicionInicial = false;

	}

	public bool BreaEstaEnPosicionInicial(){

		return BreaEnPosicionInicial;

	}

	void OnTriggerEnter2D(Collider2D other){
	
		int cantBolas;

		cantBolas = GameObject.FindGameObjectsWithTag ("pelota").Length;

		//Si pasa la pelota, Game Over
		if (other.gameObject.tag == "pelota") {

			SoundManager.soundManager.playSound(other.GetComponent<AudioSource> ());

			Destroy (other.gameObject); //, 1.2f);

			cantBolas = GameObject.FindGameObjectsWithTag ("pelota").Length;

//			delayGameOver (0.5f);
			if(cantBolas == 1) //Solo cuando queda 1 bola
				if(GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().ballInPlay)
					GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().gameOver ();
			
			

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

//	public IEnumerator delayGameOver(float delay){
//	
//		yield return new WaitForSeconds (delay);
//
//		if(GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().ballInPlay)
//			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().gameOver ();
//	
//	}

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

		particulasBrea.Play ();

		nuevaPosicion -= Vector3.up * movimientoPiso * multiplicador;

		//Validar que la nueva posicion no quede abajo del minimo
		if (PosicionInicial.position.y > nuevaPosicion.y)
			nuevaPosicion = new Vector3 (nuevaPosicion.x, PosicionInicial.position.y, nuevaPosicion.z);

	}
		
}
