using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundManager;

public class paddle : MonoBehaviour {

	public float velocidad;

	public float VelocidadControl;

	private GameController controller;
	private float move;
	private Transform posBrea;
	public Transform pelotaSpawnPaddle; 
	private Vector3 nuevaPosicion;

	void Awake(){
	
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		Vector3 nuevaPosicion = gameObject.transform.position;

		move = 0.5f;
		
	}
		
	void Update(){

		nuevaPosicion = new Vector3 (gameObject.transform.position.x, GameObject.Find ("paddleSpawn").transform.position.y, 0);

		//Subir pad siempre al nivel de la brea
//		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, nuevaPosicion, velocidad * Time.deltaTime);
		gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, nuevaPosicion, velocidad * Time.deltaTime);

	}


	void OnCollisionEnter2D(Collision2D col){
			

			if (col.gameObject.tag == "pelota" && controller.ballInPlay) {

			//LevelManager -> Sumar rebote
			LevelManager.levelManager.addRebote ();

			//LevelManager -> Reinicializar Multiplicador puntos
			LevelManager.levelManager.ReinicializarMultiplicadorPuntos ();

			//Reproducir audio
			soundManager.playSound (GetComponent<AudioSource> ());

			if (col.contacts.Length > 0) {
				ContactPoint2D contact = col.contacts [0];

				col.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

				// Calculo la diferencia entre el centro del pad y el punto x de colision
				float calc = contact.point.x - transform.position.x;

				// Calculo el porcentaje para usar como angulo de salida
				float porc = calc * 100 / GetComponent<BoxCollider2D> ().bounds.size.x;

				//Verifico de que lado reboto para la animacion correspondiente
				move = (porc / 100) + 0.5f;
				GetComponent<Animator> ().SetFloat ("Move", move);
				GetComponent<Animator> ().SetBool ("Impacto", true);
				StartCoroutine (reiniciarFlotacion (1f));

				// Parto de los 90 grados como mi 0
				porc = 90 - porc;

				contact.rigidbody.AddForce (controller.obtenerVectorVelocidad (controller.fuerzaPelota, porc, porc));


			} else {
				//Animar aunque no encuentre punto de contacto
				GetComponent<Animator> ().SetFloat ("Move", 0.5f);
				GetComponent<Animator> ().SetBool ("Impacto", true);
				StartCoroutine (reiniciarFlotacion (1f));
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag == "lineaSpawn") {
			//GameOVer

			controller.ballInPlay = false;

			controller.UI_inGame.SetActive (false);

			controller.PantallaInicial.GetComponent<MenuController> ().MostrarPlay (true);
		}

	}

	public IEnumerator reiniciarFlotacion(float duracion){

		//Esperar que termine la animacion actual
		yield return new WaitForSeconds (duracion);

		//Volver el animador Blend a 0
		GetComponent<Animator> ().SetBool ("Impacto", false);


	}
		

}
