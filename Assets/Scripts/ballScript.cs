using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManager;

public class ballScript : MonoBehaviour {

	private float velocidadConstante; //valor original 4.4
	private GameController controller;

	public bool pelotaSpawneada = false;

	public bool RedBallFlag = false;
    [SerializeField] AudioClip m_spawnSound;

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

    //Method is called from Spawn Animation
    public void StartBall(){

        // Dar fuerza inicial a la pelota
        GetComponent<Rigidbody2D>().AddForce(controller.obtenerVectorVelocidad(controller.fuerzaPelota, 50f, 130f));
        GetComponent<CircleCollider2D>().enabled = true;
        StartSpinning();

        controller.ballInPlay = true;

    }

    //Method is called from Spawn Animation
    public void SpawnSound(){

        AudioSource AudioS = gameObject.AddComponent<AudioSource>();

        AudioS.clip = m_spawnSound;
        SoundManager.soundManager.playSound(AudioS);

    }

    void Update(){
	
		gameObject.GetComponent<Rigidbody2D> ().velocity = velocidadConstante * (gameObject.GetComponent<Rigidbody2D> ().velocity.normalized);

	}

	public void PowerUpRedBall(float duracion){

        StopCoroutine("activarPowerUpRedBall");

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
