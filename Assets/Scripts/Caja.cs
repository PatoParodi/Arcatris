using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour {

	public int puntos;

	public float velocidad;
	public GameObject moneda;
	public Sprite _cajaRoja;
	public bool powerUpBajarBrea; //Determina que es un ladrillo con PowerUp
	public bool primeraCaja = false;

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

		if(LevelManager.levelManager.contadorCajasDerretidas > 5)
		//Verificar si sera una caja Power Up Bajar Brea
			if (gameObject.tag == "Caja" && probabilidad (5)) {
				GetComponent<Animator> ().SetBool ("BreaDown", true);
				powerUpBajarBrea = true;
			}
		

	}

	// Update is called once per frame
	void Update () {

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
		if (gameObject.tag == "Caja" && col.gameObject.tag == "Convertidor") {

			//Contador para activar la aparicion de PowerUps para bajar la brea
			LevelManager.levelManager.contadorCajasDerretidas++;

			//Le doy velocidad inversa al ladrillo para que no se mueva (Nacho crack)
			velocidad = -1 * LevelManager.levelManager.velocidadCajas;
			 

			// Convertir la caja a liquido
			if (powerUpBajarBrea) {
				GetComponent<Animator> ().SetBool ("ConvertirVioleta", true);
			}
			else{
				GetComponent<Animator> ().SetBool ("Convertir", true);
			}
			// Deshabilitar los colliders NO triger al transformarse
			BoxCollider2D[] colidersCaja = GetComponents<BoxCollider2D>();

			foreach(BoxCollider2D colider in colidersCaja)
				if(!colider.isTrigger)
					colider.enabled = false;

			// Luego de la animacion se dispara un evento (Animator) para subir la brea SubirBrea()

		}

		if (gameObject.tag == "prefab" &&
			col.gameObject.tag == "lineaSpawn"){
			//Una vez que el prefab termina de aparecer en la arena,  llamar al proximo
			//			if(controller.primerSpawn != true)
			controller.spawnCajas ();
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

		if (col.gameObject.tag == "ball") {
			// Deshabilitar los colliders NO triger al explotar
			GetComponent<BoxCollider2D>().enabled = false;

			if (!powerUpBajarBrea) {
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
			else {
				//Bajar Brea
				BajarBrea();

			}

			//Reproducir sonido
			SoundManager.soundManager.playSound (GetComponent<AudioSource> ());

			//Animar y destruir caja
			controller.explotarCaja (gameObject, true);

		}
	}

	public bool probabilidad(int porcentaje){
	
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
