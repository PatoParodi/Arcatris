using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBallsBtnComprar : MonoBehaviour {

	public GameObject btnTogglePelota;
	public GameObject _txtPopUpNotYet;
	public GameObject PopUpNewBall;

	GameController _controller;

	public void ComprarPelota(){
	

		_controller =  GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		//Determiniar si tiene suficientes diamantes
		if(_controller.Comprar(btnTogglePelota.GetComponent<SkinDePelotas>().Precio)){

			//Reproducir sonido
			SoundManager.soundManager.playSound(GetComponent<AudioSource>());

			//Pop Up de Nueva Bola
			PopUpNewBall.SetActive(true);
			PopUpNewBall.GetComponent<PopUpNewBall> ().NumeroDeBola = btnTogglePelota.GetComponent<SkinDePelotas>().NumeroDeBola;
			PopUpNewBall.GetComponent<PopUpNewBall> ().InstanciarBola ();

			StartCoroutine (AnimacionBotones (1f));

		} else {

			_txtPopUpNotYet.GetComponent<Animator> ().SetTrigger ("Show");

		}
			
	}

	public IEnumerator AnimacionBotones(float segundos){
	
		yield return new WaitForSeconds (segundos);

		btnTogglePelota.GetComponent<SkinDePelotas> ().PrenderBoton ();

	}
}
