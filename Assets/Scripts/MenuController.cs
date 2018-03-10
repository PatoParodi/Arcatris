using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	private static FloatingText popUpText;

	private GameController controller;

	public Text titleCongigMenu;

	void Awake(){

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

	}

	public void MostarConfiguracion(bool mostrar){

		gameObject.GetComponent<Animator> ().SetBool ("Mostrar", mostrar);

	}

	public void pauseGame(bool mostrar){

		if (controller.ballInPlay) {
			
			titleCongigMenu.text = "PAUSED";

			if (mostrar) {
				//Pause
				Time.timeScale = 0;
			} else {
				//Unpause
				Time.timeScale = 1;
				//Show Configuration Menu
			}
		} else {
			
			titleCongigMenu.text = "CONFIG";

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

	public void comprar(){

		popUpCompra ("100", gameObject.transform);

	}

	public void popUpCompra(string precio, Transform location){
	
		popUpText = Resources.Load<FloatingText> ("Prefabs/PopUpTextParent");

		FloatingText instance = Instantiate (popUpText);

		//Ubicar popUp Text en el boton correspondiente
		instance.transform.SetParent(location,false);

		//Crear objeto en pantalla
		instance.setText (precio);

	}
}
