using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinDePelotas: MonoBehaviour {

	public string NumeroDeBola;

	public int Precio;
	public Text txtPrecio;
	public GameObject btnComprar;
	public GameObject _txtPopUpNotYet;

	private GameController _controller;
 

	public void GuardarValorEnMemoria(){
	
		if (GetComponent<Toggle> ().isOn) {
			PlayerPrefs.SetString (LevelManager.levelManager.s_BolaElegida, NumeroDeBola);
			LevelManager.levelManager.numeroBolaElegida = NumeroDeBola;
		}
	}


	public void ComprarPelota(){

		_controller =  GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		//Determiniar si tiene suficientes diamantes
		if(_controller.Comprar(Precio)){
			
			Debug.Log ("Compro pelota " + NumeroDeBola);
			btnComprar.SetActive(false); //Apagar boton de compra
			//Prendo y activo el Toggle
			gameObject.SetActive (true);
			GetComponent<Toggle>().interactable = true;

			PlayerPrefs.SetInt ("Pelota_" + NumeroDeBola, 1); //Ejemplo guardar Pelota_01 si ha sido comprada

		} else {

			_txtPopUpNotYet.GetComponent<Animator> ().SetTrigger ("Show");

		}
	}

}
