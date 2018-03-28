using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour {

	public int puntos;

	public float velocidad;
	public GameObject moneda;

	private GameController controller;

	void Awake(){
	
	
	}

	void Start(){
	
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		if (gameObject.tag == "Caja") {
			velocidad = 0;
		}
		else {
			velocidad = controller.velocidadCaja;

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
			StartCoroutine( brea.GetComponent<LimiteBrea> ().SubirPiso ());
//			brea.GetComponent<LimiteBrea> ().SubirPiso ();

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
			BoxCollider2D[] colidersCaja = GetComponents<BoxCollider2D>();
			foreach(BoxCollider2D colider in colidersCaja)
				if(!colider.isTrigger)
					colider.enabled = false;

			//Explotar moneda con 25% de probabilidad
			if (probabilidad (controller.porcentajeSpawnDiamante)) {
				Instantiate (moneda, new Vector3 (transform.position.x, transform.position.y, -1), Quaternion.identity);
				//El objeto moneda viajara hasta el contador y luego se destruira
			}

			//Animar y destruir caja
			controller.explotarCaja (gameObject, true);

			//Instanciar particulas y acumular puntos
			Instantiate(Resources.Load("Prefabs/puntosParticulas"),transform.position,Quaternion.identity);

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
