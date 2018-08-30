using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using Language;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

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
	public Transform paddleSpawnInicial;
	public GameObject pelota;
//	public GameObject extraBall;
	public float fuerzaPelota;
	public GameObject ballSpawn;
	public float velocidadCaja;
	public int porcentajeSpawnDiamante;

	public GameObject BackgroundController;

	public float countdownInicial;
//	public float timeBetweenWaves;
	public botones BotonesEnPantalla;
	public textos textosEnPantalla;
	public GameObject PantallaInicial;
	public GameObject UI_inGame;
	public GameObject UI_highScore;
	public GameObject popUpContinue;
	public GameObject Brea;

	private float PrimerToqueX;
	private GameObject paddleVivo;
	private float movimientoPaddle;
	private float movimientoTouchPad;
	private float tiempoPresionandoR = 0;
	private float tiempoPresionandoL = 0;
	public int multiplicadorVelocidad;
	private GameObject pelotaViva;
	private bool validarTouchInicial = false;
	private float antiguaPosicionPaddle;
	private float contadorTiempoTapToStart = 0;
	public GameObject mensajeTapToStart;

	public bool ballInPlay = false;
	private int Puntaje;
	private int Monedas;
	private bool flagDerecha = false;
	private bool flagIzquierda = false;
	private bool firstTimeEverToPlay = true;
	private bool flagMovioIzquierda = false; 
	private bool flagMovioDerecha = false;
	public int contadorPartidas = 1;

	public int vidas;
	public int extraBalls;
	public bool c_TutorialTest;

	public tutorial tutorialObjetos;
	public SelectorControl Controles;
//	public float areaMuerta;
//	public Coroutine spawneoCajas;

//	private static LanguageManager _languageManager;

	private int _BloquesSpawneados = 0;

	// Use this for initialization
	void Start () {

// &*&*&*&*&*&*&**&*&*&*&*&*&*&*&*&*&*&*&*&*&*
// PARA PRUEBAS  de TUTORIAL SOLAMENTE!!!!!!! !! ! ! ! !! !! ! !!! !!!
// &*&*&*&*&*&*&**&*&*&*&*&*&*&*&*&*&*&*&*&*&*
///////////// TEST MODE ONLY ///////////////////////////
//		PlayerPrefs.DeleteAll();
//		PlayerPrefs.SetString ("jugoAntes","Si");
//		PlayerPrefs.SetInt ("High Score", 0);
//		PlayerPrefs.SetInt ("ArcatrisMonedas", 1000);
//		PlayerPrefs.SetInt ("ExtraBall", 0);
///////////////////////////////////////////////////////////

		//Cargar array de colores para el multiplicador
		LevelManager.levelManager.llenarColoresMultiplicador();

		//Setear sonido elegido anteriormente

		//Setear idioma elegido anteriormente
		//Llenar array con los distintos idiomas
		LanguageManager.populateLanguages();	
		//Leer idioma de memoria
		string _language;
		if (PlayerPrefs.HasKey (LevelManager.levelManager.s_Language)) {
		
			_language = PlayerPrefs.GetString (LevelManager.levelManager.s_Language);
		
		} else {
			_language = "EN";
		
		}

		//Setear idioma
		LanguageManager.setLanguage (_language);

		//Verificar bolas compradas y ultima elegida
		BallManager.Instance.VerificarBolasCompradas ();

		//Leer info de Power Ups
		//Red Ball
		leerDataPowerUps(LevelManager.levelManager.s_PowerUpRebBallProb, ref LevelManager.levelManager.PowerUpRedBall);
		leerDataPowerUps(LevelManager.levelManager.s_PowerUpRebBallDurac, ref LevelManager.levelManager.PowerUpRedBallDuracion);
		//Bajar Brea
		leerDataPowerUps(LevelManager.levelManager.s_PowerUpBajarBreaProb, ref LevelManager.levelManager.PowerUpBajarBrea);
		//Multiple Ball
		leerDataPowerUps(LevelManager.levelManager.s_PowerUpMultipleBallCant, ref LevelManager.levelManager.PowerUpMultipleBallCant);

		velocidadCaja = velocidadCaja / 100;

		//Inicializar Nivel y leer Dificultad
		LevelManager.levelManager.ReinicializarNivel ();

		//Puntaje a cero
		textosEnPantalla.puntajeText.text = "0";

		//Google Play Services
		googlePlayServicesStart ();

		googlePlayServicesSignIn ();

		// Buscar variable de primera vez que juega
		string strJugoAntes = PlayerPrefs.GetString ("jugoAntes");

		if (strJugoAntes == "Si")
			//Es la Primera vez que juega?
			firstTimeEverToPlay = false;

		///////////// TEST MODE ONLY ///////////////////////////
		 if(c_TutorialTest)
				firstTimeEverToPlay = c_TutorialTest;


		//Tutorial How to Play
		if (firstTimeEverToPlay) {

//			LevelManager.levelManager.nivelActual++;
			_BloquesSpawneados++;

			paddleVivo = Instantiate (paddle, new Vector2(paddleSpawnInicial.transform.position.x,paddleSpawnInicial.transform.position.y), Quaternion.identity) as GameObject;

			Controles.sw_TouchPad.isOn = true;

			PlayerPrefs.SetInt ("ExtraBall", 1);


			tutorialObjetos.swipeText.SetActive (true);
			tutorialObjetos.arrows.SetActive (true);
			tutorialObjetos.ladrillos.SetActive (true);
			tutorialObjetos.forceField.SetActive (false);

			//En el Update() se apagan estos objetos al tocar
		} else {
			
			//Ajustar dificultad
//			LevelManager.levelManager.determinarNivel (false);
			LevelManager.levelManager.ReinicializarNivel ();

			PantallaInicial.SetActive (true);

		}

		// Buscar High Score
		if (PlayerPrefs.HasKey (LevelManager.levelManager.s_HighScore)) {

			textosEnPantalla.highScoreValue.text = PlayerPrefs.GetInt (LevelManager.levelManager.s_HighScore).ToString();

			SubmitScoreToPlayServices (int.Parse(textosEnPantalla.highScoreValue.text));

		} else {
		
			textosEnPantalla.highScoreValue.text = "-";
		}
						

		// Monedas
		Monedas = 0;
		Monedas = PlayerPrefs.GetInt("ArcatrisMonedas");
		textosEnPantalla.cantidadMonedas.text = Monedas.ToString ();

		// Bolas Extra
		extraBalls = 0;
		extraBalls = PlayerPrefs.GetInt ("ExtraBall");
		textosEnPantalla.extraBallsValue.text = extraBalls.ToString ();

	}

	public void SubmitScoreToPlayServices(int score){

		PlayGamesPlatform.Instance.ReportScore(score,
			"CgkIkavI79INEAIQAQ",
			(bool success) =>
			{
				Debug.Log("LeaderBoard Update: " + success);
			});

	}

	void googlePlayServicesStart(){
		
		//Create client configuration
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

		//Enable debugging output 
		PlayGamesPlatform.DebugLogEnabled = true;

		//Initialiaze and activate the platform
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate ();
	
	}

	void googlePlayServicesSignIn (){

		PlayGamesPlatform.Instance.SignOut ();

		if (!PlayGamesPlatform.Instance.localUser.authenticated) {
			//Sign in with PlayGame Services
			PlayGamesPlatform.Instance.Authenticate (googlePlayServicesSignInCallBack, false);
		
		} 

	}

	public void googlePlayServicesSignInCallBack (bool success){
	
		if (success)
			Debug.Log ("Signed in!");
	
	}

	public void mostrarVidas(int vidas, string objeto){
	// Mostrar las vidas o ExtraBalls en pantalla

		GameObject bolaVida;

		// Apagar todas las vidas
		for (int i = 1; i <= 5; i++) {
			bolaVida = GameObject.Find (objeto + i.ToString()) as GameObject;
			if (bolaVida != null)
//				bolaVida.GetComponent<Animator> ().SetBool ("Ocultar", false);
				bolaVida.GetComponent<SpriteRenderer> ().enabled = false;

		}

		//Prender las vidas actuales
		for (int i = 1; i <= vidas; i++) {
			bolaVida = GameObject.Find (objeto + i.ToString()) as GameObject;
			if (bolaVida != null)
				bolaVida.GetComponent<Animator> ().SetBool ("Ocultar", false);
		
		}
			
	}
		
	public void leerDataPowerUps(string nombre, ref float variable){
		//Si existe la actualiza, si no existe se crea/inicializa
		if (PlayerPrefs.HasKey (nombre)) { //Si existe
			variable = PlayerPrefs.GetFloat (nombre);

		}
			else
			//Si no existe
			PlayerPrefs.SetFloat (nombre, variable);
	
	}

	public bool Comprar(int precio){
	
		if (Monedas >= precio) {
			//Quitar monedas de la compra
			AddMoneda (-precio);

			return true;

		} else {
		// No hay suficientes diamantes para comprar
			return false;
		}

	}

	public void addExtraBallToScreen(int cantidad){

		//Agregar una Extra Ball
		extraBalls +=  cantidad;
		PlayerPrefs.SetInt ("ExtraBall", extraBalls);

		textosEnPantalla.extraBallsValue.text = extraBalls.ToString ();
	
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
		yield return new WaitForSecondsRealtime (2.3f);
		tutorialObjetos.objectiveText.SetActive (false);

		//Texto Your challenge starts now
		tutorialObjetos.challengeText.SetActive (true);
		yield return new WaitForSecondsRealtime (2.5f);
		tutorialObjetos.challengeText.SetActive (false);

		//Setear nivel inicial como 4 para que la nueva partida arranque en 3
//		PlayerPrefs.SetInt (LevelManager.levelManager.s_Level, 5);
		//Ajustar el nivel
//		LevelManager.levelManager.determinarNivel (false);
		LevelManager.levelManager.ReinicializarNivel ();
		//Por el prefab que ya esta generado
		_BloquesSpawneados = 0;

		//Mostrar pantalla inicial
		IniciarJuego(true);


	}


	// Update is called once per frame
	void Update () {

		//Exit app on Back Button
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
				activity.Call<bool>("moveTaskToBack", true);
			}
			else
			{
				Application.Quit();
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 

		//Toque inicial durante el Tutorial
		if (firstTimeEverToPlay){
			if (paddleVivo != null) {

				//Flashing del touchPad para que lo vea
//				touchPadSlider.GetComponent<Animator>().SetBool("Flashing",true);

				if (paddleVivo.transform.position.x < -0.75f)
					flagMovioIzquierda = true;
				if (paddleVivo.transform.position.x > 0.75f)
					flagMovioDerecha = true;
			}
			if (flagMovioIzquierda && flagMovioDerecha) { 
				tutorialObjetos.swipeText.SetActive (false);
				tutorialObjetos.arrows.SetActive (false);

				//Cortar flashing del TouchPad
//				touchPadSlider.GetComponent<Animator>().SetBool("Flashing",false);

				StartCoroutine ("tutorialDemo");
			}
		}
			
		//Mover pelota junto con el pad al iniciar
		if (!ballInPlay && pelotaViva != null) {
			if(pelotaViva.GetComponent<Rigidbody2D>().velocity == Vector2.zero)	
				pelotaViva.transform.position = new Vector3(Mathf.Clamp(paddleVivo.transform.position.x,-2f,2f), paddleVivo.GetComponent<paddle>().pelotaSpawnPaddle.position.y ,0);

		}
			

		//Verificar toque inicial para soltar pelota
		if (ballInPlay == false) {
			if (pelotaViva != null) {
				if (pelotaViva.GetComponent<ballScript> ().pelotaSpawneada == true) { //Esto es true cuando termina la animacion de Spawn
					if (contadorTiempoTapToStart == 0) {
						contadorTiempoTapToStart = Time.time;
					}

					if (Input.touchCount > 0) {
						contadorTiempoTapToStart = 0;
						//Verificar que se haga un toque "rapido" sin mover el pad
						if (Input.touches [0].phase == TouchPhase.Began) {
							antiguaPosicionPaddle = paddleVivo.transform.position.x;
						} else if (Input.touches [0].phase == TouchPhase.Ended &&
						           (antiguaPosicionPaddle == paddleVivo.transform.position.x)) {
			
							validarTouchInicial = true;
							mensajeTapToStart.GetComponent<Animator>().SetBool("Mostrar",false);
						
						}
					} else {
						if (Time.time - contadorTiempoTapToStart > 3) { //3 segundos
						//Tap to Start Message
							mensajeTapToStart.GetComponent<Animator>().SetBool("Mostrar",true);
						}
					
					}
				}
			}
		}else{
			validarTouchInicial = false;
		}
	}

	void FixedUpdate(){
	

		if (paddleVivo != null) {
			// Verificar control elegido
			if (Controles.sw_TouchPad.isOn) {
				// TouchPad
				//				touchPadSlider.gameObject.SetActive (true);
				//				BotonesEnPantalla.derecha.SetActive (false);
				//				BotonesEnPantalla.izquierda.SetActive (false);

				//				float diferencia = (handleSlider.transform.position.x * 1.3f) - paddleVivo.transform.position.x;
				//
				//				// Si la diferencia es mayor a 0.3 empezar a mover el pad
				//				if (Mathf.Abs (diferencia) < areaMuerta)
				//					movimientoPaddle = 0;
				//				else if (diferencia > 0)
				//					movimientoPaddle = velocidadPaddle;
				//				else if (diferencia < 0)
				//					movimientoPaddle = -velocidadPaddle;
				//
				float _FuerzaDrag = 0;

				if (Input.touchCount == 0){
					movimientoPaddle = 0;
					_FuerzaDrag = 0;

					paddleVivo.GetComponent<Rigidbody2D> ().velocity = Vector2.MoveTowards(paddleVivo.GetComponent<Rigidbody2D> ().velocity, new Vector2 (0f, paddleVivo.GetComponent<Rigidbody2D> ().velocity.y),0.1f);

				}
				else {

					if (Mathf.Abs (paddleVivo.GetComponent<Rigidbody2D> ().velocity.x) > 0) {
						if (Mathf.Abs (Input.GetTouch (0).deltaPosition.x) > 0.5f) {
//							_FuerzaDrag = Input.GetTouch (0).deltaPosition.x * paddleVivo.GetComponent<paddle> ().VelocidadControl;
							_FuerzaDrag = Input.GetTouch (0).deltaPosition.x * LevelManager.levelManager.VelocidadPaddle;

						}

					} else{
//						_FuerzaDrag = Input.GetTouch (0).deltaPosition.x * paddleVivo.GetComponent<paddle> ().VelocidadControl;
						_FuerzaDrag = Input.GetTouch (0).deltaPosition.x * LevelManager.levelManager.VelocidadPaddle;

					}

					//					Mathf.Clamp (movimientoPaddle, -velocidadPaddle, velocidadPaddle);

				}

				//				if(Mathf.Abs(_FuerzaDrag) > Mathf.Abs(movimientoPaddle))
				movimientoPaddle = _FuerzaDrag;

//				//Clamp manual
//				if (movimientoPaddle > LevelManager.levelManager.MaxVelocidadPaddle)
////					movimientoPaddle = velocidadPaddle;
//					movimientoPaddle = LevelManager.levelManager.MaxVelocidadPaddle;
//
//
//				if (movimientoPaddle < -LevelManager.levelManager.MaxVelocidadPaddle)
////					movimientoPaddle = -velocidadPaddle;
//					movimientoPaddle = -LevelManager.levelManager.MaxVelocidadPaddle;
//
//				movimientoPaddle = movimientoPaddle / 10;
//				paddleVivo.GetComponent<Rigidbody2D> ().velocity = new Vector2 (movimientoPaddle / 10, paddleVivo.GetComponent<Rigidbody2D> ().velocity.y);



			} else if (Controles.sw_Botones.isOn) {
				//Botones de media pantalla
				//				BotonesEnPantalla.derecha.SetActive (true);
				//				BotonesEnPantalla.izquierda.SetActive (true);
				//				touchPadSlider.gameObject.SetActive (false);


				Vector3 touchPosWorld;


				//Verificar si esta tocando la pantalla para mover el pad
				if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)) {

					if (Input.touches.Length > 1) {
						touchPosWorld = Camera.main.ScreenToWorldPoint (Input.touches [1].position);
					} else {
						//We transform the touch position into world space from screen space and store it.
						touchPosWorld = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
					}

//					if (tiempoPresionando == 0)
//						tiempoPresionando = Time.deltaTime;
//					else

					if (touchPosWorld.x > 0) {
//						movimientoPaddle = velocidadPaddle;
						tiempoPresionandoR = tiempoPresionandoR + (Time.deltaTime * multiplicadorVelocidad);
						tiempoPresionandoL = 0;
						movimientoPaddle = LevelManager.levelManager.VelocidadPaddle * tiempoPresionandoR;

					}else if (touchPosWorld.x < 0){
//						movimientoPaddle = -velocidadPaddle;
						tiempoPresionandoL = tiempoPresionandoL + (Time.deltaTime * multiplicadorVelocidad);
						tiempoPresionandoR = 0;
						movimientoPaddle = -LevelManager.levelManager.VelocidadPaddle * tiempoPresionandoL;
					}
				}

				if (Input.touchCount == 0) {
					movimientoPaddle = 0;
					//					tiempoPresionando = 0;

				}

				Debug.Log (movimientoPaddle);


			}

			//Clamp manual
			if (movimientoPaddle > LevelManager.levelManager.MaxVelocidadPaddle)
				//					movimientoPaddle = velocidadPaddle;
				movimientoPaddle = LevelManager.levelManager.MaxVelocidadPaddle;


			if (movimientoPaddle < -LevelManager.levelManager.MaxVelocidadPaddle)
				//					movimientoPaddle = -velocidadPaddle;
				movimientoPaddle = -LevelManager.levelManager.MaxVelocidadPaddle;

//			movimientoPaddle = movimientoPaddle / 10;

			paddleVivo.GetComponent<Rigidbody2D> ().velocity = new Vector2 (movimientoPaddle / 10, paddleVivo.GetComponent<Rigidbody2D> ().velocity.y);


			//			paddleVivo.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (movimientoPaddle, 0));
//			paddleVivo.GetComponent<Rigidbody2D> ().velocity = new Vector2 (movimientoPaddle / 10, paddleVivo.GetComponent<Rigidbody2D> ().velocity.y);
			// Limito la posicion en X del pad para que no se suba a las paredes
//			paddleVivo.transform.position = new Vector2(Mathf.Clamp(paddleVivo.transform.position.x,-2f,2f),paddleVivo.transform.position.y);

		}
	
	}

		
	public void gameOver(){
		// GameOver
		ballInPlay = false;

		//Bajar una vida y actualizarlas en pantalla
		if(vidas > 0){
			vidas--; 
//			//Apagar la 
//			for (int i = 3; i >= vidas; i--) {
//				GameObject bolaVida = GameObject.Find ("vida" + i.ToString()) as GameObject;
//				if (bolaVida != null)
//					bolaVida.GetComponent<Animator> ().SetBool ("Ocultar", true);
//
//				}
		}
//		mostrarVidas (vidas, "vida");


		if (vidas > 0)
			//Re-spawnear
			ContinuarJuego ("WhiteBall");	//WhiteBall
		
		else {
			
			popUpContinue.SetActive (true);

			BotonesEnPantalla.pausa.SetActive(false);

		}
			
	}
		

	public void IniciarJuego(bool continueFlag){
//Comienza el juego desde el principio
		string _controlElegido = "1";

		if(!continueFlag){
			//Nueva partida
		
			//Alimentar analytics
			if(Controles.sw_TouchPad.isOn)
				_controlElegido = "TouchPad";
			else if(Controles.sw_Botones.isOn)
				_controlElegido = "Botones";

			Analytics.CustomEvent ("NuevaPartida", new Dictionary<string, object> {
				{ "Sonido", SoundManager.soundManager.soundEnabled },
				{ "ControlElegido", _controlElegido},
				{ "Dificultad", LevelManager.levelManager.dificultadActual}
			});
		
		}

		UI_inGame.SetActive (true);

		//Mostrar vidas iniciales
		vidas = 3;
		//Prender las vidas actuales
		for (int i = 1; i <= vidas; i++) {
			GameObject bolaVida = GameObject.Find ("vida" + i.ToString()) as GameObject;
			if (bolaVida != null)
				bolaVida.GetComponent<Animator> ().SetBool ("Ocultar", false);
		}

		textosEnPantalla.extraBallsInGame.text = extraBalls.ToString ();

		//Reinicializar velocidad de Cajas
		LevelManager.levelManager.ResetearVelocidadCajas();

		//Determinar Dificultad
		LevelManager.levelManager.determinarDificultad();

		//Inicializar Objetos
		StartCoroutine(inicializarObjetos(countdownInicial, continueFlag, "WhiteBall")); //WhiteBall

	}

	public void ContinuarJuego(string ballType){
//Continuar el juego sin volver a empezar

		//		inicializarObjetos ();
		StartCoroutine(inicializarObjetos(countdownInicial, true, ballType));

	}

	public IEnumerator inicializarObjetos(float seconds, bool continueFlag, string ballType){

		GameObject[] cajas;
		GameObject[] prefabs;

		//Reinicializar Multiplicador de Puntos
		LevelManager.levelManager.ReinicializarMultiplicadorPuntos();

		//Habilitar Boton de Pausa
		BotonesEnPantalla.pausa.SetActive(true);
	
		//Instanciar Pelota y Paddle
		if (paddleVivo == null) {
			paddleVivo = Instantiate (paddle, new Vector2(paddleSpawnInicial.transform.position.x,paddleSpawnInicial.transform.position.y), Quaternion.identity) as GameObject;

		}
		paddleVivo.transform.position = new Vector2 (paddleVivo.transform.position.x, GameObject.FindGameObjectWithTag ("padPosition").transform.position.y);

		//Identificar bola seleccionada y devolver el numero en string

//		pelotaViva.GetComponent<ballScript>().SetTipoDeBola(ballType);
//		LevelManager.levelManager.numeroBolaElegida = ballType;

		SoundManager.soundManager.playSound (ballSpawn.GetComponent<AudioSource> ());

		//Apagar la 
		GameObject bolaVida = GameObject.Find ("vida" + vidas.ToString()) as GameObject;
		if (bolaVida != null)
			bolaVida.GetComponent<Animator> ().SetBool ("Ocultar", true);


		//Frenar cualquier movimiento del pad
		moverDerecha(false);
		moverIzquierda(false);

	// En caso de NO continuar despues de perder
		if (!continueFlag) {

			LevelManager.levelManager.numeroBolaElegida = PlayerPrefs.GetString(LevelManager.levelManager.s_BolaElegida);

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

			//Contador de Cajas derretidas
			LevelManager.levelManager.contadorCajasDerretidas = 0;

			//Reinicializar Nivel y get Dificultad
			LevelManager.levelManager.ReinicializarNivel ();

			//Reinicializar contador de Spawns de prefabs antes del Boss
			_BloquesSpawneados = 0;

			//Contador de partidas
			contadorPartidas ++;

			//Paddle a su posicion inicial
//			touchPadSlider.value = 0.5f;

			//Brea y paddle a su posicion inicial
			breaPosicionInicial();

			//Puntaje a cero
			Puntaje = 0;
			textosEnPantalla.puntajeText.text = Puntaje.ToString();

			GameObject _instanciaCajas = spawnCajas ();
			//Spawnear las cajas un poco mas abajo al empezar
			_instanciaCajas.GetComponent<Caja>().primeraCaja = true;

			//Esperar a que la Brea este en posicion inicial para arrancar
			yield return new WaitUntil( Brea.GetComponent<LimiteBrea>().BreaEstaEnPosicionInicial );

		}
		else{

			// Solo para cuando se utiliza una Bola Extra
			if (vidas <= 0) {

				// Limpiar cajas explotando
				float posicionConvertidor = GameObject.FindGameObjectWithTag ("Convertidor").transform.position.y;
				float lineaDestruccion = 3.9f - posicionConvertidor;
				//Limpiar 2/3 de la pantalla entre la brea y el limite superior
				lineaDestruccion = lineaDestruccion * 2 / 3;
				lineaDestruccion = lineaDestruccion + posicionConvertidor;
				limpiarCajas (lineaDestruccion);	
			}
		}

		pelotaViva  = Instantiate (pelota,new Vector3 (paddleVivo.transform.position.x,ballSpawn.transform.position.y,ballSpawn.transform.position.z),Quaternion.identity) as GameObject;

		//Posicionar pelota arriba del pad a medida que vaya subiendo
//		pelotaViva.transform.position = new Vector2(paddleVivo.transform.position.x, paddleVivo.transform.position.y + paddleVivo.GetComponentInChildren<SpriteRenderer>().bounds.size.y/2 + pelotaViva.GetComponentInChildren<SpriteRenderer>().bounds.size.y/2);

		yield return new WaitUntil (() => validarTouchInicial == true);

		validarTouchInicial = false;
			
//		yield return new WaitForSeconds(seconds);


		// Dar fuerza inicial a la pelota
		pelotaViva.GetComponent<Rigidbody2D>().AddForce (obtenerVectorVelocidad(fuerzaPelota,50f,130f));
		pelotaViva.GetComponent<CircleCollider2D> ().enabled = true;

		ballInPlay = true;


	}

	public void breaPosicionInicial(){
	//Llevar brea a su posicion inicial
		Brea.GetComponent<LimiteBrea>().moverBrea(new Vector3 (Brea.transform.position.x, -3.9f, Brea.transform.position.z));

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
		Destroy (caja, 1.5f);

	}

	public Vector2 obtenerVectorVelocidad(float fuerza, float anguloMin, float anguloMax){

		float angulo = Random.Range (anguloMin, anguloMax);
		Vector2 vector2D;

		angulo *= Mathf.Deg2Rad;

		vector2D.y = Mathf.Sin (angulo) * fuerza;

		vector2D.x = Mathf.Cos (angulo) * fuerza;

		return vector2D;

	}
//
//	public GameObject spawnCajas (){
//
//		GameObject _instancia = null;
//
//		//Despues de cada Boss subir el nivel
//		if(_BloquesSpawneados == 0){
//			//subir de nivel
//			LevelManager.levelManager.SubirNivel();
//			//Cambiar color del fondo
//			BackgroundController.GetComponent<Animator>().SetInteger("Nivel",LevelManager.levelManager.nivelActual);
//
//		}
//
//		//Elegir el prefab aleatroriamente
//		if (_BloquesSpawneados < LevelManager.levelManager.BloquesPorNivel ()) {
//
//			//por ejemplo L1_bloque2
//			string prefabRandom = "L" + LevelManager.levelManager.nivelActual + "_bloque" + Random.Range (1, 5);
//
//			_instancia = Instantiate (Resources.Load ("Prefabs/" + prefabRandom)) as GameObject;
//
//			_BloquesSpawneados++;
//
//		} else {
//		//Viene un boss
//			string prefabRandom = "L" + LevelManager.levelManager.nivelActual + "_boss";
//
//			_instancia = Instantiate (Resources.Load ("Prefabs/" + prefabRandom)) as GameObject;
//
//			_BloquesSpawneados = 0;
//
//		}
//
//		return _instancia;
//
//	}


	public GameObject spawnCajas (){

		GameObject _instancia = null;

		//Elegir el prefab aleatroriamente
		string prefabRandom = "L1" + "_bloque" + Random.Range (1, 5);

		_instancia = Instantiate (Resources.Load ("Prefabs/" + prefabRandom)) as GameObject;

		return _instancia;

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
		textosEnPantalla.extraBallsInGame.text = extraBalls.ToString ();

		PlayerPrefs.SetInt ("ExtraBall", extraBalls);

		//Continuar Juego con Extra Ball
		LevelManager.levelManager.numeroBolaElegida = "ExtraBall";
		ContinuarJuego ("ExtraBall");

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

	public int getHighScore(){
	
		return Puntaje; 

	}

	public int _getMonedas(){
	
		return Monedas;
	}
}
