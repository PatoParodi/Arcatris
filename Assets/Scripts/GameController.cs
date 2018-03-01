using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Para que Unity reconozca esta clase es necesaria esta sentencia
[System.Serializable]
public class botones{
	public GameObject derecha, izquierda, pausa;
}

[System.Serializable]
public class textos{
	
	public Text puntajeText,
				cantidadMonedas,
				highScoreValue,
				highScoreText,
				bestScoreText,
				extraBallsValue,
				extraBallsInGame;

}

[System.Serializable]
public class tutorial{
	public GameObject 	swipeText,
						arrows,
						objectiveText,
						challengeText,
						ladrillos,
						forceField;

}

[System.Serializable]
public class SelectorControl{
	public Toggle 	sw_Botones,
					sw_TouchPad;
}

public class GameController : MonoBehaviour {

	public GameObject paddle;
	public float velocidadPaddle;
	public Transform paddleSpawn;
	public Transform paddleSpawnInicial;
	public GameObject pelota;
	public GameObject extraBall;
	public float fuerzaPelota;
	public Transform ballSpawn;
	public GameObject cajas_01;
	public GameObject cajas_02;
	public GameObject cajas_03;
	public GameObject cajas_04;
	public Transform cajaSpawn;
	public float velocidadCaja;

	public float countdownInicial;
	public float timeBetweenWaves;
	public botones BotonesEnPantalla;
	public textos textosEnPantalla;
	public GameObject PantallaInicial;
	public GameObject UI_inGame;
	public GameObject UI_highScore;
	public GameObject UI_pause;
	public Text UI_pauseTitle;
	public GameObject popUpContinue;
	public GameObject Brea;
	public GameObject touchPad;

	public Slider touchPadSlider;
	public RectTransform handleSlider;

	private GameObject paddleVivo;
	private float movimientoPaddle;
	private float movimientoTouchPad;
	private GameObject pelotaViva;
	public bool ballInPlay = false;
	private int Puntaje;
	private int Monedas;
	private bool flagDerecha = false;
	private bool flagIzquierda = false;
	private bool firstTimeEverToPlay = true;
	private bool flagMovioIzquierda = false; 
	private bool flagMovioDerecha = false;
	public int vidas;
	public int extraBalls;

	public tutorial tutorialObjetos;
	public SelectorControl Controles;
	public float areaMuerta;
	public Coroutine spawneoCajas;

	// Use this for initialization
	void Start () {
		
///////////////////////////////////////////////////////////
		PlayerPrefs.SetInt ("High Score", 0);
		PlayerPrefs.SetInt ("ArcatrisMonedas", 1000);
///////////////////////////////////////////////////////////


		velocidadCaja = velocidadCaja / 100;

		//Puntaje a cero
		textosEnPantalla.puntajeText.text = "0";

		// Buscar variable de primera vez que juega
		string strJugoAntes = PlayerPrefs.GetString ("jugoAntes");

		if (strJugoAntes == "Si")
			//Primera vez que juega
			firstTimeEverToPlay = false;

		// &*&*&*&*&*&*&**&*&*&*&*&*&*&*&*&*&*&*&*&*&*
		// PARA PRUEBAS  de TUTORIAL SOLAMENTE!!!!!!! !! ! ! ! !! !! ! !!! !!!
		// &*&*&*&*&*&*&**&*&*&*&*&*&*&*&*&*&*&*&*&*&*
		firstTimeEverToPlay = true;


		//Tutorial How to Play
		if (firstTimeEverToPlay) {
			PantallaInicial.GetComponent<MenuController> ().MostrarPlay (false);
			paddleVivo = Instantiate (paddle, paddleSpawnInicial) as GameObject;

			tutorialObjetos.swipeText.SetActive (true);
			tutorialObjetos.arrows.SetActive (true);
			tutorialObjetos.ladrillos.SetActive (true);
			tutorialObjetos.forceField.SetActive (false);
			//En el Update() se apagan estos objetos al tocar
		} else {
			
			PantallaInicial.GetComponent<MenuController> ().MostrarPlay (true);

		}
		// Buscar High Score
		textosEnPantalla.highScoreValue.text = PlayerPrefs.GetInt ("High Score").ToString();
		if (textosEnPantalla.highScoreValue.text == "")
			textosEnPantalla.highScoreValue.text = "Get one!";

		// Monedas
		Monedas = 0;
		Monedas = PlayerPrefs.GetInt("ArcatrisMonedas");
		textosEnPantalla.cantidadMonedas.text = Monedas.ToString ();

		// Bolas Extra
		extraBalls = 0;
		extraBalls = PlayerPrefs.GetInt ("ExtraBall");
		textosEnPantalla.extraBallsValue.text = extraBalls.ToString ();
//		mostrarVidas(extraBalls,"ExtraBall");

	}

	public void mostrarVidas(int vidas, string objeto){
	// Mostrar las vidas o ExtraBalls en pantalla

		GameObject bolaVida;

		// Apagar todas las vidas
		for (int i = 1; i <= 5; i++) {
			bolaVida = GameObject.Find (objeto + i.ToString()) as GameObject;
			if (bolaVida != null)
				bolaVida.GetComponent<SpriteRenderer> ().enabled = false;

		}

		//Prender las vidas actuales
		for (int i = 1; i <= vidas; i++) {
			bolaVida = GameObject.Find (objeto + i.ToString()) as GameObject;
			if (bolaVida != null)
				bolaVida.GetComponent<SpriteRenderer> ().enabled = true;
		
		}
			
	}
		
	public void comprarExtraBall(){
	
		if (Monedas >= 100) {
			//Quitar monedas de la compra
			AddMoneda (-100);

			//Agregar una Extra Ball
			extraBalls = extraBalls + 1;
			PlayerPrefs.SetInt ("ExtraBall", extraBalls);

			textosEnPantalla.extraBallsValue.text = extraBalls.ToString ();

		}

	}
		
	public IEnumerator tutorialDemo(){

		//Al terminar el tutorial nunca mas se mostrara
		if (firstTimeEverToPlay){
			// Setear variable de primera vez que juega
			PlayerPrefs.SetString ("jugoAntes", "Si");
			firstTimeEverToPlay = false;
		}
			
		yield return new WaitForSecondsRealtime (1);
		//Text del Objetivo
		tutorialObjetos.objectiveText.SetActive (true);
		tutorialObjetos.forceField.SetActive (true);
		yield return new WaitForSecondsRealtime (4);
		tutorialObjetos.objectiveText.SetActive (false);

		//Texto Your challenge starts now
		tutorialObjetos.challengeText.SetActive (true);
		yield return new WaitForSecondsRealtime (3.3f);
		tutorialObjetos.challengeText.SetActive (false);

		//Mostrar pantalla inicial
		PantallaInicial.GetComponent<MenuController> ().MostrarPlay (true);

		// Inicializar objetos en pantalla
		textosEnPantalla.highScoreText.text = "";
		textosEnPantalla.bestScoreText.enabled = false;
		textosEnPantalla.highScoreValue.text = "";
		BotonesEnPantalla.pausa.SetActive (false);
		textosEnPantalla.puntajeText.text = "";


	}

	// Update is called once per frame
	void Update () {

		//Toque inicial durante el Tutorial
		if (firstTimeEverToPlay){
			if (paddleVivo != null) {
				if (paddleVivo.transform.position.x < -1.3f)
					flagMovioIzquierda = true;
				if (paddleVivo.transform.position.x > 1.3f)
					flagMovioDerecha = true;
			}
			if (flagMovioIzquierda && flagMovioDerecha) { 
				tutorialObjetos.swipeText.SetActive (false);
				tutorialObjetos.arrows.SetActive (false);

				StartCoroutine ("tutorialDemo");
			}
		}
			
		//Mover pelota junto con el pad al iniciar
		if (!ballInPlay && pelotaViva != null) {

			pelotaViva.transform.position = new Vector3(paddleVivo.transform.position.x, pelotaViva.transform.position.y,pelotaViva.transform.position.z);

		}

		if (ballInPlay && pelotaViva != null) {

		}

		#if UNITY_EDITOR 

//
//		if (Input.GetKeyDown (KeyCode.A)) {
//			if(paddleVivo != null){
////				moverIzquierda(true);
//				movimientoTouchPad = -200;
//
//			}
//		}else if (Input.GetKeyUp (KeyCode.A)) {
//			if(paddleVivo != null){
////				moverIzquierda(false);
//				movimientoTouchPad = 0;
//
//			}
//		}
//		else if (Input.GetKeyDown (KeyCode.D)) {
//			if(paddleVivo != null){
////				moverDerecha(true);
//				movimientoTouchPad = 200;
//
//			}
//		}else if (Input.GetKeyUp (KeyCode.D)) {
//			if(paddleVivo != null){
////				moverDerecha(false);
//				movimientoTouchPad = 0;
//
//			}
//		}

		#endif

		if (paddleVivo != null) {
			// Verificar control elegido
			if (Controles.sw_TouchPad.isOn) {
				// TouchPad
				touchPadSlider.gameObject.SetActive (true);
				BotonesEnPantalla.derecha.SetActive (false);
				BotonesEnPantalla.izquierda.SetActive (false);

				float diferencia = handleSlider.transform.position.x - paddleVivo.transform.position.x;

				// Si la diferencia es mayor a 0.3 empezar a mover el pad
				if (Mathf.Abs (diferencia) < areaMuerta)
					movimientoPaddle = 0;
				else if (diferencia > 0)
					movimientoPaddle = velocidadPaddle;
				else if (diferencia < 0)
					movimientoPaddle = -velocidadPaddle;
				
			} else if (Controles.sw_Botones.isOn) {
				//Botones de media pantalla
				BotonesEnPantalla.derecha.SetActive (true);
				BotonesEnPantalla.izquierda.SetActive (true);
				touchPadSlider.gameObject.SetActive (false);

			}

			paddleVivo.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (movimientoPaddle, 0));

			// Limito la posicion en X del pad para que no se suba a las paredes
			paddleVivo.transform.position = new Vector3(Mathf.Clamp(paddleVivo.transform.position.x,-2f,2f),paddleVivo.transform.position.y,paddleSpawn.transform.position.z);

		}

	}


		
	public void gameOver(){
		// GameOver
		ballInPlay = false;

		//Bajar una vida y actualizarlas en pantalla
		vidas = vidas - 1; 
		mostrarVidas (vidas, "vida");


		if (vidas > 0)
			//Re-spawnear
			ContinuarJuego (pelota);
		else {
			
			popUpContinue.SetActive (true);
//			//Frenar Prefabs de la Cajas
//			GameObject[] cajas = GameObject.FindGameObjectsWithTag ("prefab");
//			foreach (GameObject caja in cajas) {
//				caja.GetComponent<Caja> ().velocidad = 0;	
//			}

			BotonesEnPantalla.pausa.SetActive(false);
			BotonesEnPantalla.izquierda.SetActive (false);
			BotonesEnPantalla.derecha.SetActive (false);

		}
			
			
	}

	void OnLateUpdate(){

	}

	public void IniciarJuego(){
//Comienza el juego desde el principio

		PantallaInicial.GetComponent<MenuController> ().MostrarPlay (false);
//		PantallaInicial.SetActive (false);
		UI_inGame.SetActive (true);

		//Mostrar vidas iniciales (3)
		vidas = 3;
		mostrarVidas (vidas, "vida");


		textosEnPantalla.extraBallsInGame.text = extraBalls.ToString ();

		//Inicializar Objetos
		StartCoroutine(inicializarObjetos(countdownInicial, false, pelota));

	}

	public void ContinuarJuego(GameObject ballType){
//Continuar el juego sin volver a empezar

		//		inicializarObjetos ();
		StartCoroutine(inicializarObjetos(countdownInicial, true, ballType));

	}

	public IEnumerator inicializarObjetos(float seconds, bool continueFlag, GameObject ballType){

		GameObject[] cajas;
		GameObject[] prefabs;

		//Habilitar Boton de Pausa
		BotonesEnPantalla.pausa.SetActive(true);

		//Instanciar Pelota y Paddle
		pelotaViva  = Instantiate (ballType,new Vector3 (ballSpawn.position.x,ballSpawn.position.y,ballSpawn.position.z),Quaternion.identity) as GameObject;
		if (paddleVivo == null) {
			paddleVivo = Instantiate (paddle, paddleSpawnInicial) as GameObject;

		}

		//Frenar cualquier movimiento del pad
		moverDerecha(false);
		moverIzquierda(false);

	// En caso de NO continuar despues de perder
		if (!continueFlag) {
			// Destruir cajas
			cajas = GameObject.FindGameObjectsWithTag("Caja");
			foreach (GameObject caja in cajas) {
					Destroy (caja);
			}

			// Destruir prefabs
			prefabs = GameObject.FindGameObjectsWithTag("prefab");
			foreach (GameObject prefab in prefabs) {
				Destroy (prefab);
			}

			//Brea a su posicion inicial
			Brea.transform.position = new Vector3 (Brea.transform.position.x, -3.9f, Brea.transform.position.z);

			//paddle a su posicion inicial
//			paddleVivo.transform.position = new Vector3 (paddleSpawnInicial.transform.position.x,paddleSpawnInicial.transform.position.y,paddleSpawnInicial.transform.position.z);

			//TouchPad a su posicion inicial
//			touchPadSlider.value = 0.5f;

			//Puntaje a cero
			Puntaje = 0;
			textosEnPantalla.puntajeText.text = Puntaje.ToString();

			spawnCajas ();

		}
		else{

			// Solo para cuando se utiliza una Bola Extra
			if (vidas == 0) {
//				//Volver a mover  Prefabs de la Cajas
//				prefabs = GameObject.FindGameObjectsWithTag ("prefab");
//				foreach (GameObject prefab in prefabs) {
//					prefab.GetComponent<Caja> ().velocidad = velocidadCaja;
//				}


				// Limpiar cajas explotando
				float posicionConvertidor = GameObject.FindGameObjectWithTag ("Convertidor").transform.position.y;
				float lineaDestruccion = 3.9f - posicionConvertidor;
				//Limpiar 2/3 de la pantalla entre la brea y el limite superior
				lineaDestruccion = lineaDestruccion * 2 / 3;
				lineaDestruccion = lineaDestruccion + posicionConvertidor;
				limpiarCajas (lineaDestruccion);	
			}
		}

		paddleVivo.transform.position = new Vector2 (paddleVivo.transform.position.x, GameObject.FindGameObjectWithTag ("padPosition").transform.position.y);
		//Posicionar pelota arriba del pad a medida que vaya subiendo
		pelotaViva.transform.position = new Vector3 (ballSpawn.transform.position.x, 
			paddleSpawn.transform.position.y + 0.27f, 
			ballSpawn.transform.position.z);

		touchPad.transform.position = new Vector3 (0, touchPad.transform.position.y, touchPad.transform.position.z);

		yield return new WaitForSeconds (seconds);

		// Dar fuerza inicial a la pelota
		pelotaViva.GetComponent<Rigidbody2D>().AddForce (obtenerVectorVelocidad(fuerzaPelota,50f,130f));

		ballInPlay = true;


	}

	public void limpiarCajas(float lineaDestruccion){

		GameObject[] cajas = GameObject.FindGameObjectsWithTag("Caja");

		foreach (GameObject caja in cajas) {
			if(caja.transform.position.y < lineaDestruccion)
				//Animar y destruir caja
				explotarCaja (caja, false);

		}

	}

	public void explotarCaja(GameObject caja, bool explotadoPorBola){

		// Activar Animacion de explosion
		caja.GetComponent<Animator> ().SetBool ("Destruir", true);

		//Destruir caja luego de que termine la animacion
		StartCoroutine (destruirCaja(caja, 1.5f));

	}

	public IEnumerator destruirCaja(GameObject caja, float duracionAnimacion){

		yield return new WaitForSeconds (duracionAnimacion);

		Destroy(caja);

	}

	public Vector2 obtenerVectorVelocidad(float fuerza, float anguloMin, float anguloMax){

		float angulo = Random.Range (anguloMin, anguloMax);
		Vector2 vector2D;

		angulo *= Mathf.Deg2Rad;

		vector2D.y = Mathf.Sin (angulo) * fuerza;

		vector2D.x = Mathf.Cos (angulo) * fuerza;

		return vector2D;

	}

	public void spawnCajas (){


		//Elegir el prefab aleatroriamente
		//Generar el prefab 
		switch (Random.Range (1, 4)){
		case 1:
			Instantiate(cajas_01);
			break;
		
		case 2:
			Instantiate(cajas_02);
			break;

		case 3:
			Instantiate(cajas_03);
			break;

		case 4:
			Instantiate(cajas_04);
			break;

		}

	}

	public int AddScore(int puntos){

		Puntaje += puntos;
		textosEnPantalla.puntajeText.text = Puntaje.ToString ();

		return Puntaje;
	
	}

	public void AddMoneda(int cantidad){
	
		Monedas += cantidad;
		textosEnPantalla.cantidadMonedas.text = Monedas.ToString ();
		//Guardar monedas en BD
		PlayerPrefs.SetInt("ArcatrisMonedas", Monedas);

	
	}

	public void utilizarExtraBall(){

		extraBalls -= 1;
		textosEnPantalla.extraBallsValue.text = extraBalls.ToString ();

		PlayerPrefs.SetInt ("ExtraBall", extraBalls);

		textosEnPantalla.extraBallsInGame.text = extraBalls.ToString ();

		//Continuar Juego con Extra Ball
		ContinuarJuego (extraBall);

	}

	// Movimiento del paddle con botones
	public void moverIzquierda(bool flagBoton){

		flagIzquierda = flagBoton;

		if (flagIzquierda && paddleVivo != null)
			movimientoPaddle = -velocidadPaddle;

		if(!flagDerecha && !flagIzquierda)
			movimientoPaddle = 0;
			
	}

	// Movimiento del paddle con botones
	public void moverDerecha(bool flagBoton){

		flagDerecha = flagBoton;

		if (flagDerecha && paddleVivo != null)
			movimientoPaddle = velocidadPaddle;

		if(!flagDerecha && !flagIzquierda)
			movimientoPaddle = 0;

	}

	public void pauseGame(){

		if (ballInPlay) {
			if (Time.timeScale == 0) {
				//Unpause
				Time.timeScale = 1;
				UI_pauseTitle.text = "CONFIG";
			}

			else if (Time.timeScale == 1){
				//Pause
				Time.timeScale = 0;
				//Show Configuration Menu
//				UI_pause.SetActive(true);
				UI_pauseTitle.text = "PAUSED";
			}
		}

	}

	public int getHighScore(){
	
		return Puntaje;

	}
}
