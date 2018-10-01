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

	private GameObject ballSkin;

	private BallSpinManager ballSkinManager;

	void Awake(){
	
		velocidadConstante = LevelManager.levelManager.velocidadPelota;

		//Play spawn sound
		SoundManager.soundManager.playSound(GetComponent<AudioSource>());

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

	}

	public void LoadSkin(){

		pelotaSpawneada = true;

		ballSkin = Instantiate (Resources.Load ("Pelotas/" + levelManager.numeroBolaElegida) as GameObject, transform.position, Quaternion.identity, gameObject.transform);

		ballSkinManager = ballSkin.GetComponent<BallSpinManager>();

//		ballSkin.transform.position = Vector3.zero;

	}

	public void StartSpinning(){
	
		ballSkinManager.StartSpinning();
	
	}

	void Update(){
	
		gameObject.GetComponent<Rigidbody2D> ().velocity = velocidadConstante * (gameObject.GetComponent<Rigidbody2D> ().velocity.normalized);

//		//Para que al spawnear la skin no inicie girando
//		if (!controller.ballInPlay) {
//			if (pelotaSpawneada)
//				GetComponentInChildren<Animator> ().speed = 0;
//			
//		} else {
//
//			GetComponentInChildren<Animator> ().speed = 1;
//
//		}

//		if(_colaExtraBall != null)
//			if (controller.ballInPlay)
//				_colaExtraBall.enabled = true;

	}

	public void PowerUpRedBall(float duracion){

		StopAllCoroutines ();

		//Iniciar comportamiento durante "duracion" segundos
		StartCoroutine (activarPowerUpRedBall (duracion));


	}

	// Power Up Red Ball - Activar
	public IEnumerator activarPowerUpRedBall(float duracion){
	
		//Animar bola para que cambie de color
		ballSkinManager.ActivarBolaRoja ();
		RedBallFlag = true;

		yield return new WaitForSeconds (duracion);

		RedBallFlag = false;
		ballSkinManager.DesactivarBolaRoja ();

	}


	void OnCollisionEnter2D(Collision2D col){

		//Hacer girar la pelota al chocar con el marco
		if (col.gameObject.tag == "Marco"){

			GetComponentInChildren<BallSpinManager> ().spin = (GetComponent<Rigidbody2D> ().velocity.x / GetComponent<Rigidbody2D> ().velocity.y) + 0.5f;

		}


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
