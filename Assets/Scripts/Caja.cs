using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour {

	public int puntos;

	public float velocidad;
	public GameObject moneda;
	public Sprite _cajaRoja;
	public bool powerUpBajarBrea; //Determina que es un ladrillo con PowerUp
	public bool powerUpMB; //Power Up Multiple Balls
	public bool powerUpRedBall; //Power Up Red Ball
	public bool primeraCaja = false;
	public bool NoCalcularPowerUp;

	private GameController controller;
	private Vector2 posicionInicial;

	void Start(){

		posicionInicial = transform.position;

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		if (gameObject.tag == "Caja") {
			velocidad = 0;
		}
		else {
			velocidad = LevelManager.levelManager.velocidadCajas;

		}

		if (!NoCalcularPowerUp) {
			//No determinar si son powerUps para la oleada de Tutorial
			if (gameObject.tag == "Caja") {
				if (LevelManager.levelManager.contadorCajasDerretidas > 5)
				//Verificar si sera una caja Power Up Bajar Brea
				if (probabilidad (LevelManager.levelManager.PowerUpBajarBrea)) {
					GetComponent<Animator> ().SetBool ("BreaDown", true);
					powerUpBajarBrea = true;
				}

				if (!powerUpBajarBrea && !powerUpRedBall && probabilidad (LevelManager.levelManager.PowerUpMultipleBallProb)) {
					powerUpMB = true;

				}

				if (!powerUpBajarBrea && !powerUpMB && probabilidad (LevelManager.levelManager.PowerUpRedBall)) {
					powerUpRedBall = true;

				}
		
			}
		
		}

		if (powerUpMB) {
			GetComponent<Animator> ().SetBool ("PowerUpMB", true);
		}

		if (powerUpRedBall) {
			GetComponent<Animator> ().SetBool ("PowerUpRedBall", true);
		}

	}

	// Update is called once per frame
	void Update () {

		if (gameObject.tag != "Caja")
			velocidad = LevelManager.levelManager.velocidadCajas;

		if (controller.ballInPlay){
			//Solo moverse cuando la pelota esta en juego
			transform.position = new Vector3 (transform.position.x, transform.position.y - velocidad * Time.deltaTime, transform.position.z);
			primeraCaja = false;
		}
		
		if (primeraCaja) {

			transform.position = Vector2.Lerp(transform.position, new Vector2 (posicionInicial.x, posicionInicial.y - 2.2f),0.1f);
	
		}
	}



	void OnTriggerEnter2D(Collider2D col){

		//Animar ladrillo a derretirse al pasar por el convertidor
		if (gameObject.tag == "Caja"){
		if (col.gameObject.tag == "Convertidor") {

			//Contador para activar la aparicion de PowerUps para bajar la brea
			LevelManager.levelManager.contadorCajasDerretidas++;

			//Le doy velocidad inversa al ladrillo para que no se mueva (Nacho crack)
			velocidad = -1 * LevelManager.levelManager.velocidadCajas;
			 

			// Convertir la caja a liquido
			if (powerUpBajarBrea) {
				GetComponent<Animator> ().SetBool ("ConvertirVioleta", true);
			} else {
				GetComponent<Animator> ().SetBool ("Convertir", true);
			}
			// Deshabilitar los colliders NO triger al transformarse
			BoxCollider2D[] colidersCaja = GetComponents<BoxCollider2D> ();

			foreach (BoxCollider2D colider in colidersCaja)
				if (!colider.isTrigger)
					colider.enabled = false;

			// Luego de la animacion se dispara un evento (Animator) para subir la brea SubirBrea()

		}
		
		//Si lo choca una Bola Roja debe atravesarlo
		if (col.gameObject.tag == "pelota")
			if(col.gameObject.GetComponent<ballScript> ().RedBallFlag) {
				//Si lo choca una red ball apagar el collider para que no rebote
				GetComponent<BoxCollider2D> ().enabled = false;

				explotarCaja (col.gameObject);

		}
	}
			

		if (gameObject.tag == "prefab" &&
			col.gameObject.tag == "lineaSpawn"){
			//Una vez que el prefab termina de aparecer en la arena,  llamar al proximo
			controller.spawnCajas ();

			LevelManager.levelManager.AumentarVelocidadCajas ();

		}
			
	}

	public void BajarBrea(){

		GameObject brea = GameObject.FindGameObjectWithTag ("Brea");

		brea.GetComponent<LimiteBrea> ().bajarBrea(4);

	}

	public void SubirBrea(){
		//Este evento es llamado desde el Animator de la Caja (Brea_Fall)
		GameObject brea = GameObject.FindGameObjectWithTag ("Brea");

		brea.GetComponent<LimiteBrea> ().subirBrea ();

	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "pelota") {

			explotarCaja (col.gameObject);

		}
	}


	void explotarCaja (GameObject objeto){

		// Deshabilitar los colliders NO triger al explotar
		GetComponent<BoxCollider2D>().enabled = false;

		if (powerUpBajarBrea) {
			//Bajar Brea
			BajarBrea ();

		} else if (powerUpMB) {
			//Multiple Balls
			//Instanciar bola extra
			GameObject bolaNueva;
			for (int i = 0; i < LevelManager.levelManager.PowerUpMultipleBallCant; i++) {
				bolaNueva = Instantiate (Resources.Load ("Prefabs/ball"), transform.position, Quaternion.identity) as GameObject;
				bolaNueva.GetComponentInChildren<Animator> ().SetTrigger ("MultipleBall");	//Saltar animacion de Spawn
				bolaNueva.GetComponent<Rigidbody2D> ().AddForce (controller.obtenerVectorVelocidad (controller.fuerzaPelota, 50f, 130f));
				bolaNueva.GetComponent<CircleCollider2D> ().enabled = true;
			}

		} else if (powerUpRedBall) {
			//Red Ball
			objeto.GetComponent<ballScript>().PowerUpRedBall(LevelManager.levelManager.PowerUpRedBallDuracion);
		}

		else{
			//Explotar moneda con 25% de probabilidad
			if (probabilidad (controller.porcentajeSpawnDiamante)) {
				Instantiate (moneda, new Vector3 (transform.position.x, transform.position.y, -1), Quaternion.identity);
				//El objeto moneda viajara hasta el contador y luego se destruira
			}
			//PUNTOS
			//Multiplicador de puntos por combo
			LevelManager.levelManager.multiplicadorPuntosCaja++;
			LevelManager.levelManager._sortingLayer++;	//Aumento la capa para que se muestre delante
			if(LevelManager.levelManager.multiplicadorPuntosCaja > 1)
				popUpMultiplicador ();

			//Instanciar particulas y acumular puntos
			GameObject particulas = Instantiate (Resources.Load ("Prefabs/puntosParticulas"), transform.position, Quaternion.identity) as GameObject;
			particulas.GetComponent<puntosSpawn> ()._puntos = LevelManager.levelManager.multiplicadorPuntosCaja * LevelManager.levelManager.puntosBaseCaja;

		}

		//Reproducir sonido
		SoundManager.soundManager.playSound (GetComponent<AudioSource> ());

		//Animar y destruir caja
		controller.explotarCaja (gameObject, true);


	}

	public bool probabilidad(float porcentaje){
	
		int valorRandom = Random.Range (1, 100);

		if (valorRandom < porcentaje)
			return true;
		else {
			return false;
		}

	}

	public void popUpMultiplicador(){
		//Mostrar PopUp del multiplicador por Combo de Cajas

		Instantiate (Resources.Load ("Prefabs/PopUpMultiplicador"));


	}
		
}
