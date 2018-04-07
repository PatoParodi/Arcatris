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
	public GameObject _pantallaInicial;
	public Text _monedas;
	public Text _extraBalls;
	public GameObject _UI_RateUS;
//	public Text _velocidadPelotaText;


	void Awake(){

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

	}

	void OnEnable(){

		//Cambiar el titulo del menu a PAUSA durante el juego
//		if (titleConfigMenu != null) 
//			if (controller.ballInPlay)
//			//PAUSA
//			else
//			//CONFIG
//				titleConfigMenu.GetComponent<LanguageGetText> ().posicion = 0;

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

		//Ir a pantalla de PLAY
		_pantallaInicial.SetActive(true);
		_pantallaInicial.GetComponent<Animator> ().SetBool ("Mostrar", mostrar);

	}

	public void controlSound(){
	
		SoundManager.soundManager.enableSound (soundOnOff.isOn);

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
	
	}

}
