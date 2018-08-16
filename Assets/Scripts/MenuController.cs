using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.Advertisements;
using Language;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


[System.Serializable]
public class langButton{
	public string lang, iconName;

}

public class MenuController : MonoBehaviour {

	private static FloatingText popUpText;

	private GameObject _extraBall;

	private GameController controller;
	private bool _juegoPausado = false;

	private bool _RateShowed  = false;
	private bool _publiShowed = false;

	public Text titleConfigMenu;
	public Toggle soundOnOff;
	public Toggle TouchPad;
	public Toggle Botones;

	public GameObject _pantallaInicial;
	public GameObject _inGame;
	public GameObject _UI_RateUs;
	public Text _monedas;
	public Text _extraBalls;
	public GameObject _txtPopUp;
	public GameObject _UI_RateUS;
	public Text _txtNivel;
	public AudioSource musicaMenu;
	public bool tieneSonidos;
//	public Text _velocidadPelotaText;


	void Start(){

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		//Levantar de memoria la config del Sonido
		if (soundOnOff != null) {
			if (PlayerPrefs.GetString (LevelManager.levelManager.s_sound) == LevelManager.levelManager.s_On)
				soundOnOff.isOn = true;
			else if(PlayerPrefs.GetString (LevelManager.levelManager.s_sound) == LevelManager.levelManager.s_Off) {
				soundOnOff.isOn = false;
			}
			// Actualizar controlador global del sonido
			SoundManager.soundManager.enableSound (soundOnOff.isOn);
		}

		//Levantar de memoria la config de los controles
		if (TouchPad != null) {
			if (PlayerPrefs.GetString (LevelManager.levelManager.s_TouchPad) == LevelManager.levelManager.s_Off) {
				TouchPad.isOn = false;
				Botones.isOn = true;
			} else{
				TouchPad.isOn = true;
			}
		}


	}

	public void controlTouchPad(){
	
		if (TouchPad.isOn)
			PlayerPrefs.SetString (LevelManager.levelManager.s_TouchPad,LevelManager.levelManager.s_On);
		else
			PlayerPrefs.SetString (LevelManager.levelManager.s_TouchPad,LevelManager.levelManager.s_Off);
				
	
	}

	void OnEnable(){

		if (musicaMenu != null)
			SoundManager.soundManager.playSound (musicaMenu);

		//Sonido al mostrar
		if (tieneSonidos)
			SoundManager.soundManager.playSound (GetComponent<AudioSource> ());
	}

	void Update(){
	
		if (_extraBalls != null) {
		
			_extraBalls.text = controller.extraBalls.ToString ();
		}

		if (_monedas != null) {

			_monedas.text = controller._getMonedas ().ToString ();
		}
			
//		//Velocidad variable de pelota
//		if (_velocidadPelotaText != null)
//			LevelManager.levelManager.velocidadPelota = float.Parse (_velocidadPelotaText.text);
//
		
	}
		

	public void buttonCloseConfig(){

		if (_juegoPausado) {
			Time.timeScale = 1;
			titleConfigMenu.GetComponent<LanguageGetText> ().posicion = 0;

		} else {

//			MostrarPlay (true);
		}

		_juegoPausado = false;

	}

	public void pauseGame(){

		_juegoPausado = true;

		Time.timeScale = 0;

		titleConfigMenu.GetComponent<LanguageGetText> ().posicion = 10;
	
	}

	public void MostrarPlay(bool mostrar){

		bool popUp;

		popUp = false;

		//Buscar GameController si aun no se recupero
		if(controller == null)
			controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		
		//Mostrar HighScore si corresponde
		if (controller.getHighScore () > PlayerPrefs.GetInt (LevelManager.levelManager.s_HighScore)) {

			PlayerPrefs.SetInt (LevelManager.levelManager.s_HighScore, controller.getHighScore ());

			//Actualizar texto de Pantalla Inicial
			controller.textosEnPantalla.highScoreValue.text = controller.getHighScore ().ToString ();

			popUp = true;

			controller.UI_highScore.SetActive (true);

		}

		//Mostrar Rate US a las 5 partidas y luego cada 20
		if (!_RateShowed && !popUp && PlayerPrefs.GetString (LevelManager.levelManager.s_Rated) != "Si") //Si aun no reateo
		if(controller.contadorPartidas == 5 || 
			(controller.contadorPartidas > 1 && controller.contadorPartidas%20 == 0)) //Mostrar cada 20 partidas
				{	

//					controller.contadorPartidas++;
					_RateShowed = true;
					popUp = true;

					_UI_RateUs.SetActive (true);

				}

		//Mostrar publicidad cada 7 partidas
		if (!popUp && !_publiShowed) //Si aun no reateo
		if(controller.contadorPartidas > 1 && controller.contadorPartidas%7 == 0) //Mostrar cada 7 partidas
		{	

			_publiShowed = true;

			popUp = true;

			Advertisement.Show ();

			MostrarPlay (true);

		}


		//Ir a pantalla de PLAY
		if (!popUp) {
			
			_publiShowed = false;
			_RateShowed = false;

//			controller.contadorPartidas++;
//			PlayerPrefs.SetInt(
			
			//Ajustar el nivel al terminar la partida
//			LevelManager.levelManager.determinarNivel (true);

			// Al ir a pantalla de Play siempre volver la Brea a su posicion inicial
			controller.breaPosicionInicial ();
			_pantallaInicial.SetActive (true);
		}
			
	}

	public void controlSound(){

		SoundManager.soundManager.enableSound (soundOnOff.isOn);

		if (soundOnOff.isOn) {
			PlayerPrefs.SetString (LevelManager.levelManager.s_sound, LevelManager.levelManager.s_On);
			if (_pantallaInicial != null && _pantallaInicial.activeSelf)
				_pantallaInicial.GetComponent<MenuController>().musicaMenu.Play();
			else if(_inGame != null && _inGame.activeSelf)
				_inGame.GetComponent<MenuController>().musicaMenu.Play();
			

		} else {
			PlayerPrefs.SetString (LevelManager.levelManager.s_sound, LevelManager.levelManager.s_Off);
			if (_pantallaInicial != null && _pantallaInicial.activeSelf)
				_pantallaInicial.GetComponent<MenuController>().musicaMenu.Pause();
			else if(_inGame != null && _inGame.activeSelf)
				_inGame.GetComponent<MenuController>().musicaMenu.Pause();

		}

	}

	void realizarCompraExtraBall(int precio, int cantidad){

		// Hace la compra si tiene suficientes diamantes, caso contrario devuelve false
		bool _compraHecha = controller.Comprar (precio);

		if (_compraHecha) {

			//Alimentar analytics
			Analytics.CustomEvent ("ComprasExtraBall", new Dictionary<string, object> {
				{ "Cantidad", cantidad },
				{ "Precio", precio }
			});

			//Reproducir sonido
			SoundManager.soundManager.playSound(GetComponent<AudioSource>());

			//Instanciar pelota comprada, que volara hasta el contador
			_extraBall = Resources.Load ("Prefabs/extraBallCompra") as GameObject;

			StartCoroutine (_instanciarBola (cantidad, 0.1f));

		} // No hay suficientes diamantes
		else{ 
			
			_txtPopUp.GetComponent<Animator> ().SetTrigger ("Show");

		}

	}

	public IEnumerator _instanciarBola(int cantidad, float tiempo){

		for (int i = 0; i < cantidad; i++) {
		
			Instantiate (_extraBall, gameObject.transform);

			yield return new WaitForSeconds (tiempo);
		
		}
	}


	public void comprarExtraBallVid(){

		//Mostrar video por Reward
		if (Advertisement.IsReady ()) {
			ShowOptions options = new ShowOptions();
			options.resultCallback = ManagerShowResult;

			Advertisement.Show("rewardedVideo", options);

		}

	}

	void ManagerShowResult (ShowResult result)
	{
		if(result == ShowResult.Finished) {
			// Reward your player here.
			//Sumar una Bola Extra.
			realizarCompraExtraBall (0, 1);

		}else if(result == ShowResult.Skipped) {
			//Video was skipped - Do NOT reward the player

		}else if(result == ShowResult.Failed) {
			//Video failed to show

		}

	}

	public void comprarExtraBall1(){
		int _cantidad = 1;
		int _precio = 100;

		realizarCompraExtraBall (_precio, _cantidad);

	}

	public void comprarExtraBall2(){
		int _cantidad = 5;
		int _precio = 400;

		realizarCompraExtraBall (_precio, _cantidad);

	}

	public void comprarExtraBall3(){

		realizarCompraExtraBall (600, 10);

	}

	public void comprarExtraBall4(){

		realizarCompraExtraBall (1000, 25);

	}

	public void comprarPowerUp1(){
//RedBall -> Probabilidad +3%  
		//Mostrar video por Reward
		if (Advertisement.IsReady ()) {
			ShowOptions options = new ShowOptions();
			options.resultCallback = ManagerShowResultPowerUp;

			Advertisement.Show("rewardedVideo", options);

		}

	}

	void ManagerShowResultPowerUp (ShowResult result)
	{
		if(result == ShowResult.Finished) {
			// Reward your player here.
			//Sumar 3% a la probabilidad de RedBall
			LevelManager.levelManager.PowerUpRedBall += LevelManager.levelManager.PowerUpRedBall * 0.03f;

			PlayerPrefs.SetFloat (LevelManager.levelManager.s_PowerUpRebBallProb, LevelManager.levelManager.PowerUpRedBall);

			//Reproducir sonido de compra
			SoundManager.soundManager.playSound(GetComponent<AudioSource>());

			//Alimentar analytics
			Analytics.CustomEvent ("ComprasPowerUp", new Dictionary<string, object> {
				{ "PowerUp", "RedBallProb" }
			});

		}else if(result == ShowResult.Skipped) {
			//Video was skipped - Do NOT reward the player

		}else if(result == ShowResult.Failed) {
			//Video failed to show

		}

	}

	public void comprarPowerUp2(){
//BajarBrea -> Probabilidad +7%

		if (realizarCompraPowerUp (95, "BajarBreaProb")) {

			LevelManager.levelManager.PowerUpBajarBrea += LevelManager.levelManager.PowerUpBajarBrea * 0.07f;

			PlayerPrefs.SetFloat (LevelManager.levelManager.s_PowerUpBajarBreaProb, LevelManager.levelManager.PowerUpBajarBrea);

		}

	}

	public void comprarPowerUp3(){
//RedBall -> Tiempo +1 segundo

		if (realizarCompraPowerUp (100, "RedBallTiempo")) {

			LevelManager.levelManager.PowerUpRedBallDuracion += 1;

			PlayerPrefs.SetFloat (LevelManager.levelManager.s_PowerUpRebBallDurac, LevelManager.levelManager.PowerUpRedBallDuracion);

		}
	}

	public void comprarPowerUp4(){
//MultipleBalls -> Cantidad +1 bola

		if (realizarCompraPowerUp (450, "MultipleBallCant")) {

			LevelManager.levelManager.PowerUpMultipleBallCant += 1;

			PlayerPrefs.SetFloat (LevelManager.levelManager.s_PowerUpMultipleBallCant, LevelManager.levelManager.PowerUpMultipleBallCant);

		}
	}

	public void comprarPowerUp5(){
//BajarBrea -> Probabilidad +10%

		if (realizarCompraPowerUp (140, "BajarBreaProb")) {

			LevelManager.levelManager.PowerUpBajarBrea += LevelManager.levelManager.PowerUpBajarBrea * 0.10f;

			PlayerPrefs.SetFloat (LevelManager.levelManager.s_PowerUpBajarBreaProb, LevelManager.levelManager.PowerUpBajarBrea);

		}
	}

	bool realizarCompraPowerUp(int precio, string tipoDePowerUp){

		// Hace la compra si tiene suficientes diamantes, caso contrario devuelve false
		bool _compraHecha = controller.Comprar(precio);

		if (_compraHecha) {

			//Alimentar analytics
			Analytics.CustomEvent ("ComprasPowerUp", new Dictionary<string, object> {
				{ "PowerUp", tipoDePowerUp }
			});

			//Reproducir sonido
			SoundManager.soundManager.playSound(GetComponent<AudioSource>());

			return true;

		} // No hay suficientes diamantes
		else{ 

			_txtPopUp.GetComponent<Animator> ().SetTrigger ("Show");

			return false;
		}

	}

	public void GoToMarket(){

		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.PardeSotas.Arcatris");

		PlayerPrefs.SetString (LevelManager.levelManager.s_Rated, "Si");

	}


	public void ShowLeaderboards(){
	
		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			//Submit Score to Play Services
			if(controller.getHighScore() > 0)
				controller.SubmitScoreToPlayServices(controller.getHighScore());

			PlayGamesPlatform.Instance.SetDefaultLeaderboardForUI ("CgkIkavI79INEAIQAQ");
			PlayGamesPlatform.Instance.ShowLeaderboardUI ();

		
		} else {
			PlayGamesPlatform.Instance.Authenticate (controller.googlePlayServicesSignInCallBack, false);	
//			StartCoroutine(MostrarLeaderBoardsAfterAuth ());

		}
	
	}

//	public void isAuthenticated(bool success){
//
//		success = false;
//
//		if (PlayGamesPlatform.Instance.localUser.authenticated)
//			success = true;
//
//	}
//
//	public IEnumerator MostrarLeaderBoardsAfterAuth(){
//	
//		yield return new WaitUntil (isAuthenticated);
//
//		PlayGamesPlatform.Instance.SetDefaultLeaderboardForUI ("CgkIkavI79INEAIQAQ");
//		PlayGamesPlatform.Instance.ShowLeaderboardUI ();
//	
//	}
		
}
