using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	private static FloatingText popUpText;

	private GameObject _extraBall;

	private GameController controller;

	public Text titleConfigMenu;
	public Text _monedas;
	public Text _extraBalls;

	void Awake(){

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

	}

	void Update(){
	
		if (_extraBalls != null) {
		
			_extraBalls.text = controller.extraBalls.ToString ();
		}

		if (_monedas != null) {

			_monedas.text = controller._getMonedas ().ToString ();
		}
			
	}

	public void MostarConfiguracion(bool mostrar){

		gameObject.GetComponent<Animator> ().SetBool ("Mostrar", mostrar);

	}

	public void pauseGame(bool mostrar){

		if (controller.ballInPlay) {
			
			titleConfigMenu.text = "PAUSED";

			if (mostrar) {
				//Pause
				Time.timeScale = 0;
			} else {
				//Unpause
				Time.timeScale = 1;
				//Show Configuration Menu
			}
		} else {
			
			titleConfigMenu.text = "CONFIG";

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
		
	public void comprarExtraBallVid(){

		//Mostrar video por Reward

		controller.comprarExtraBall (0, 1);

	}

	public void comprarExtraBall1(){
		//Mostrar PopUp desde el boton donde se compro
//		popUpCompra ("100", gameObject.transform);

		_extraBall = Resources.Load("Prefabs/extraBallCompra") as GameObject;

		Instantiate (_extraBall, gameObject.transform);

		//Mover nueva extraBall comprada hacia el contador de ExtraBalls

		controller.comprarExtraBall (100, 1);

	}

	public void comprarExtraBall2(){
		//Mostrar PopUp desde el boton donde se compro
		popUpCompra ("100", gameObject.transform);

		controller.comprarExtraBall (100, 1);

	}

	public void comprarExtraBall3(){
		//Mostrar PopUp desde el boton donde se compro
		popUpCompra ("100", gameObject.transform);

		controller.comprarExtraBall (100, 1);

	}

	public void comprarExtraBall4(){
		//Mostrar PopUp desde el boton donde se compro
		popUpCompra ("100", gameObject.transform);

		controller.comprarExtraBall (100, 1);

	}

	public void popUpCompra(string precio, Transform location){
	
		popUpText = Resources.Load<FloatingText> ("Prefabs/PopUpTextParent");



		FloatingText instance = Instantiate (popUpText, location.position, Quaternion.identity);

		//Ubicar popUp Text en el boton correspondiente
		instance.transform.SetParent(gameObject.transform,false);

		//Crear objeto en pantalla
		instance.setText (precio);

	}
}
