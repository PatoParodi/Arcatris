using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class scriptContador : MonoBehaviour {

	public GameObject pantallaInicial;
	public GameObject gameController;
	public GameObject popUpContinue;
	public Button botonSiUse;
	public Button botonSiVideo;
	public Sprite videoSprite;
	public Sprite yesSprite;

	private bool sinExtraBall;

	private bool contando;
	private GameController controller;

	void Awake(){
	
//		Advertisement.Initialize("1605669");

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
	
	}

	void OnEnable(){
	
		if (controller.extraBalls > 0){
//			botonSiUse.GetComponent<Image> ().sprite = yesSprite;
			sinExtraBall = false;
			botonSiUse.gameObject.SetActive(true);
			botonSiVideo.gameObject.SetActive(false);

		}
		else {
			sinExtraBall = true;
//			botonSiUse.GetComponent<Image> ().sprite = videoSprite;
			botonSiUse.gameObject.SetActive(false);

			botonSiVideo.gameObject.SetActive(true);
			botonSiVideo.gameObject.GetComponent<Button> ().interactable = false;


		}

	}

	// Update is called once per frame
	void Update () {

		if (Advertisement.IsReady())
			botonSiVideo.gameObject.GetComponent<Button> ().interactable = true;

		if (gameObject.activeSelf == true && contando == false) {

			contando = true;

			StartCoroutine ("contador");

		}

	}

	public IEnumerator contador(){

		int i;

		for (i = 9; i >= 0; i--) {
			
			GetComponent<Text> ().text = i.ToString();

			yield return new WaitForSecondsRealtime (1);

		}

		if (i < 0) {
		// Al terminar el tiempo volver al menu inicial
			buttonNo ();

		}
	
	}


	void ShowRewardedVideo ()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = ManagerShowResult;

		Advertisement.Show("rewardedVideo", options);
	}


	void ManagerShowResult (ShowResult result)
	{
		if(result == ShowResult.Finished) {
			//Utilizar una Bola Extra.
			controller.extraBalls += 1;
			controller.utilizarExtraBall();
			// Reward your player here.

		}else if(result == ShowResult.Skipped) {
			//Video was skipped - Do NOT reward the player
			buttonNo();

		}else if(result == ShowResult.Failed) {
			//Video failed to show
			buttonNo();

		}
		else			
			buttonNo();
	}

	public void buttonSi(){

		// Validar si le quedan Extra Ball
		sinExtraBall = true;
		if (sinExtraBall)
			//VER VIDEO PARA GANAR UNA BOLA EXTRA
			ShowRewardedVideo();
		else
		//Utilizar una Bola Extra.
			controller.utilizarExtraBall();

		contando = false;
		popUpContinue.SetActive (false);

	}

	public void buttonNo(){

		contando = false;
		//Ocultar pop up de Continue
		popUpContinue.SetActive (false);
		//Ocultar Canvas UI de inGame
		controller.UI_inGame.SetActive (false);

		//Mostrar HighScore si corresponde
		if (controller.getHighScore() > PlayerPrefs.GetInt ("High Score")) {

			controller.UI_highScore.SetActive (true);

			PlayerPrefs.SetInt ("High Score", controller.getHighScore());

			//Actualizar texto de Pantalla Inicial
			controller.textosEnPantalla.highScoreValue.text = controller.getHighScore ().ToString ();

		}

		else{
			//Animar pantalla de Play
			pantallaInicial.GetComponent<MenuController> ().MostrarPlay (true);
		}
			

	}
}
