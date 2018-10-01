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
			
			btnComprar.SetActive(false); //Apagar boton de compra
			//Prendo y activo el Toggle
			gameObject.SetActive (true);
			GetComponent<Toggle>().interactable = true;
			//Seleccionar Pelota comprada
			GetComponent<Toggle> ().isOn = true;

			//Guardar compra en BD
			PlayerPrefs.SetInt (NumeroDeBola, 1); //Ejemplo guardar (Pelota) 01 si ha sido comprada (0 NO, 1 SI)

			//Reproducir sonido
			SoundManager.soundManager.playSound(GetComponent<AudioSource>());

			//Analytics
			AnalyticsManager.Instance.ComprarPelota(NumeroDeBola);

		} else {

			_txtPopUpNotYet.GetComponent<Animator> ().SetTrigger ("Show");

		}
	}

}
