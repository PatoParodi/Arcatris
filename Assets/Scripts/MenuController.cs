using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.Advertisements;
using Language;


[System.Serializable]
public class langButton{
	public string lang, iconName;

}

public class MenuController : MonoBehaviour {

	private static FloatingText popUpText;

	private GameObject _extraBall;

	private GameController controller;
	private bool _juegoPausado = false;

	public Text titleConfigMenu;
	public Toggle soundOnOff;
	public Toggle TouchPad;
	public Toggle Botones;

	public GameObject _pantallaInicial;
	public GameObject _UI_RateUs;
	public Text _monedas;
	public Text _extraBalls;
	public GameObject _txtPopUp;
	public GameObject _UI_RateUS;
	public Text _txtNivel;
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

		AudioSource audio = GetComponent<AudioSource> ();

		if (audio != null)
			SoundManager.soundManager.playSound (audio);
		
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

			MostrarPlay (true);
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
		if (controller.getHighScore () > PlayerPrefs.GetInt ("High Score")) {

			PlayerPrefs.SetInt ("High Score", controller.getHighScore ());

			//Actualizar texto de Pantalla Inicial
			controller.textosEnPantalla.highScoreValue.text = controller.getHighScore ().ToString ();

			popUp = true;

			controller.UI_highScore.SetActive (true);

		}

		if (!popUp && PlayerPrefs.GetString (LevelManager.levelManager.s_Rated) != "Si") //Si aun no reateo
		if(controller.contadorPartidas == 5 || 
			(controller.contadorPartidas > 1 && controller.contadorPartidas%20 == 0)) //Mostrar cada 3 partidas
				{	

					controller.contadorPartidas++;
				
					popUp = true;

					_UI_RateUs.SetActive (true);

				}

		//Ir a pantalla de PLAY
		if (!popUp) {
			
			//Ajustar el nivel al terminar la partida
			LevelManager.levelManager.determinarNivel (true);

			// Al ir a pantalla de Play siempre volver la Brea a su posicion inicial
			controller.breaPosicionInicial ();
			_pantallaInicial.SetActive (true);
		}
			
	}

	public void controlSound(){

		//Musica de cada menu
		AudioSource audio = GetComponent<AudioSource> ();

		SoundManager.soundManager.enableSound (soundOnOff.isOn);

		if (soundOnOff.isOn) {
			PlayerPrefs.SetString (LevelManager.levelManager.s_sound, LevelManager.levelManager.s_On);
			if (audio != null)
				audio.Play ();

		} else {
			PlayerPrefs.SetString (LevelManager.levelManager.s_sound, LevelManager.levelManager.s_Off);
			audio.Stop ();

		}
		

	}

	void realizarCompraExtraBall(int precio, int cantidad){

		// Hace la compra si tiene suficientes diamantes, caso contrario devuelve false
		bool _compraHecha = controller.comprarExtraBall (precio, cantidad);

		if (_compraHecha) {

			//Alimentar analytics
			Analytics.CustomEvent ("ComprasExtraBall", new Dictionary<string, object> {
				{ "Cantidad", cantidad },
				{ "Precio", precio }
			});

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

	public void GoToMarket(){
	
		Application.OpenURL ("market://details?id=" + "Limbo"); //Application.productName);

		PlayerPrefs.SetString (LevelManager.levelManager.s_Rated, "Si");

	}
		
}
