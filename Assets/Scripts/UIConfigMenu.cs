using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfigMenu : MonoBehaviour {

	private GameController controller;

	void Awake(){

		//		Advertisement.Initialize("1605669");

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

	}

	public void BackToMainMenu(){

		LevelManager.levelManager.homeButton = true;

		//Quitar pausa y cerrar ventana
		GetComponent<MenuController>().buttonCloseConfig();

		//Ocultar Canvas UI de inGame
		controller.TerminarPartida ();

	}

}
