using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour {

	public int puntos;

	public float velocidad;
	public GameObject moneda;
	public Sprite _cajaRoja;

	private GameController controller;
	private bool b_tienePowerUp;

	void Start(){
	
		//Determinar si esta caja tendra un POWER UP
		if (gameObject.tag == "Caja" && probabilidad (21)) {

			b_tienePowerUp = true;

		}

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		if (gameObject.tag == "Caja") {
			velocidad = 0;
		}
		else {
			velocidad = LevelManager.levelManager.velocidadCajas;

		}
			
	}

	// Update is called once per frame
	void Update () {

		if(controller.ballInPlay)
			//Solo moverse cuando la pelota esta en juego
			transform.position = new Vector3(transform.position.x, transform.position.y - velocidad * Time.deltaTime, transform.position.z);

	}



	void OnTriggerEnter2D(Collider2D col){

		//Animar ladrillo a derretirse al pasar por el convertidor
		if (gameObject.tag == "Caja" && col.gameObject.tag == "Convertidor") {
			// Convertir la caja a liquido
			GetComponent<Animator> ().SetBool ("Convertir", true);
			// Deshabilitar los colliders NO triger al transformarse
			BoxCollider2D[] colidersCaja = GetComponents<BoxCollider2D>();
			foreach(BoxCollider2D colider in colidersCaja)
				if(!colider.isTrigger)
					colider.enabled = false;

			GameObject brea = GameObject.FindGameObjectWithTag ("Brea");

			// Subir el nivel de la brea
//			StartCoroutine( brea.GetComponent<LimiteBrea> ().SubirPiso ());
			brea.GetComponent<LimiteBrea> ().subirBrea ();

		}

		if (gameObject.tag == "prefab" &&
			col.gameObject.tag == "lineaSpawn"){
			//Una vez que el prefab termina de aparecer en la arena,  llamar al proximo
			//			if(controller.primerSpawn != true)
			controller.spawnCajas ();
		}
			
	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "ball") {
			// Deshabilitar los colliders NO triger al explotar
			GetComponent<BoxCollider2D>().enabled = false;

			//Explotar moneda con 25% de probabilidad
			if (probabilidad (controller.porcentajeSpawnDiamante)) {
				Instantiate (moneda, new Vector3 (transform.position.x, transform.position.y, -1), Quaternion.identity);
				//El objeto moneda viajara hasta el contador y luego se destruira
			}

			//Animar y destruir caja
			controller.explotarCaja (gameObject, true);

			//Instanciar particulas y acumular puntos
			Instantiate(Resources.Load("Prefabs/puntosParticulas"),transform.position,Quaternion.identity);

			//Reproducir sonido
			SoundManager.soundManager.playSound(GetComponent<AudioSource>());

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
		
}
