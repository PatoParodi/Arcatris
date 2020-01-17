﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

public class scriptContador : MonoBehaviour {

	public GameObject pantallaInicial;
	public GameObject gameController;
	public GameObject popUpContinue;
	public Button botonSiUse;
	public Button botonSiVideo;
	public GameObject _UI_RateUs;

	private bool sinExtraBall;

	private bool contando = false;
	private GameController controller;

	void Start(){
	
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

	}

	void OnEnable(){

		contando = false;

        if(controller == null)
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();


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
            if (AdsManager.Instance.IsRewardedLoaded())
                botonSiVideo.gameObject.GetComponent<Button>().interactable = true;
            else
            {
                botonSiVideo.gameObject.GetComponent<Button>().interactable = false;
            }


        }

    }

	// Update is called once per frame
	void Update () {

		if (gameObject.activeSelf == true && contando == false) {

			contando = true;

			StartCoroutine ("contador");

		}

	}

	public IEnumerator contador(){

		int i;

		for (i = 9; i >= 0; i--) {
			
			GetComponent<Text> ().text = i.ToString();

			SoundManager.soundManager.playSound(GetComponent<AudioSource> ());

            yield return new WaitForSeconds (1);

		}

		if (i < 0) {
		// Al terminar el tiempo volver al menu inicial
			buttonNo ();

		}
	
	}


	void ShowRewardedVideo ()
	{

        //Mostrar video por Reward
        AdsManager.Instance.ShowRewardedVideo(false, null, null);

  //      ShowOptions options = new ShowOptions();
		//options.resultCallback = ManagerShowResult;

		//Advertisement.Show("rewardedVideo", options);
	}


	//void ManagerShowResult (ShowResult result)
	//{
		
	//	if(result == ShowResult.Finished) {
	//		//Utilizar una Bola Extra.
	//		controller.extraBalls += 1;
	//		controller.utilizarExtraBall();
	//		// Reward your player here.

	//	}else if(result == ShowResult.Skipped) {
	//		//Video was skipped - Do NOT reward the player
	//		buttonNo();

	//	}else if(result == ShowResult.Failed) {
	//		//Video failed to show
	//		buttonNo();

	//	}
	//	else			
	//		buttonNo();
	//}

	public void buttonSi(){

		int _conExtraBall = 0, _conVideo = 0;

		// Validar si le quedan Extra Ball
		if (sinExtraBall){
            //VER VIDEO PARA GANAR UNA BOLA EXTRA
            Time.timeScale = 0;
            ShowRewardedVideo ();
			_conVideo++;

		}
		else{
		//Utilizar una Bola Extra.
			controller.utilizarExtraBall ();
			_conExtraBall++;

            ClosePopUp();
        }	

		//Metricas - Analytics - Cuantas extraball se usan y cuantas por video
		Analytics.CustomEvent ("ContinueExtraBall", new Dictionary<string, object> {
			{ "ConExtraBall", _conExtraBall},
			{ "ConVideo", _conVideo}
		});

	}

    public void ClosePopUp(){

        contando = false;
        popUpContinue.SetActive(false);

    }

	public void buttonNo(){

		contando = false;
		//Ocultar pop up de Continue
		popUpContinue.SetActive (false);
		//Ocultar Canvas UI de inGame
		controller.UI_inGame.SetActive (false);

        //Check if it is time to show ad
        AdsManager.Instance.CheckToShowAd();

		pantallaInicial.GetComponent<MenuController> ().MostrarPlay (true);


	}
}
