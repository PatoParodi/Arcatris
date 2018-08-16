using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManager;

public class ballScript : MonoBehaviour {

	private float velocidadConstante; //valor original 4.4
	private GameController controller;

	public bool pelotaSpawneada = false;

//	public TrailRenderer _colaExtraBall;

	public bool RedBallFlag = false;


	void Awake(){
	
		velocidadConstante = LevelManager.levelManager.velocidadPelota;

		//Play spawn sound
		SoundManager.soundManager.playSound(GetComponent<AudioSource>());

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();


	}

	//Animacion de spawn
	public void animarSpawning(){
		
		GetComponentInChildren<Animator> ().SetTrigger ("Instanciar");

	}

	//Animacion para tipo de bola
	/// <summary>
	/// Setear la Animacion para cada tipo de Bola
	/// </summary>
	/// <param name="tipoDeBola">Nombre del tipo de bola.</param>
	public void SetTipoDeBola(string tipoDeBola){

		if (tipoDeBola == "WhiteBall") {

			GetComponentInChildren<Animator> ().SetInteger("NumeroDeBola",levelManager.numeroBolaElegida);

		} else {

			GetComponentInChildren<Animator> ().SetTrigger (tipoDeBola);
		}

	}


	void Update(){
	
		gameObject.GetComponent<Rigidbody2D> ().velocity = velocidadConstante * (gameObject.GetComponent<Rigidbody2D> ().velocity.normalized);

//		if(_colaExtraBall != null)
//			if (controller.ballInPlay)
//				_colaExtraBall.enabled = true;

	}

	public void PowerUpRedBall(float duracion){

		//Iniciar comportamiento durante "duracion" segundos
		StartCoroutine (activarPowerUpRedBall (duracion));

	}

	// Power Up Red Ball - Activar
	public IEnumerator activarPowerUpRedBall(float duracion){
	
		//Animar bola para que cambie de color
		GetComponentInChildren<Animator>().SetBool("RedBall",true);
		RedBallFlag = true;

		yield return new WaitForSeconds (duracion);

		RedBallFlag = false;
		GetComponentInChildren<Animator>().SetBool("RedBall",false);

	}

	void OnCollisionEnter2D(Collision2D col){

		// Evitar que al chocar con poco angulo contra algun marco la pelota quede pegada a la pared
		if (col.gameObject.tag == "Marco" &&
		    col.contacts [0].point.y < 4.25f) {
			//Forzar vector de velocidad a magnitud fija
			gameObject.GetComponent<Rigidbody2D> ().velocity = velocidadConstante * (gameObject.GetComponent<Rigidbody2D> ().velocity.normalized);

			// RelativeVelocity POSITIVA
			if (col.relativeVelocity.x < 1 && col.relativeVelocity.x > 0)
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1.1f, GetComponent<Rigidbody2D> ().velocity.y));
		
			// RelativeVelocity NEGATIVA
			else if (col.relativeVelocity.x > -1 && col.relativeVelocity.x < 0)
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-1.1f, GetComponent<Rigidbody2D> ().velocity.y));
			
			// RelativeVelocity POSITIVA
			if (col.relativeVelocity.y < 1 && col.relativeVelocity.y >= 0){
//				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, -1.1f);
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, (-col.relativeVelocity.y) - (1 - col.relativeVelocity.y)/2);

		}
			// RelativeVelocity NEGATIVA
			else if (col.relativeVelocity.y > -1 && col.relativeVelocity.y < 0){
//				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, 1.1f);
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, (-col.relativeVelocity.y) + (1 + col.relativeVelocity.y)/2);

		}
				
		}

		//Forzar vector de velocidad a magnitud fija
		gameObject.GetComponent<Rigidbody2D> ().velocity = velocidadConstante * (gameObject.GetComponent<Rigidbody2D> ().velocity.normalized);
	
	}

}
