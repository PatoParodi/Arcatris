using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	private GameController controller;


	void Start(){

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

	}

	public void MostarConfiguracion(bool mostrar){

		gameObject.GetComponent<Animator> ().SetBool ("Mostrar", mostrar);

	}

	public void pauseGame(bool mostrar){

		if (controller.ballInPlay) {
		if (mostrar) {
				//Pause
				Time.timeScale = 0;
			}

			else{
				//Unpause
				Time.timeScale = 1;
				//Show Configuration Menu
			}
		}

		gameObject.GetComponent<Animator> ().SetBool ("Mostrar", mostrar);

	}

	public void MostrarPlay(bool mostrar){

		if (!controller.ballInPlay) {
			gameObject.GetComponent<Animator> ().SetBool ("Mostrar", mostrar);
		}

	}

	public void MostarShop(bool mostrar){

		gameObject.GetComponent<Animator> ().SetBool ("Mostrar", mostrar);


	}
}
