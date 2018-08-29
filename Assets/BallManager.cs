using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallManager : MonoBehaviour {

	public static BallManager Instance {
		get;
		private set;
	}

	public Toggle[] botonesPelotas;

	void Awake(){
		//First we check if there are any other instances conflicting
		if (Instance != null && Instance != this)
		{
			//Destroy other instances if they are not the same
			Destroy(gameObject);
		}
		//Save our current singleton instance
		Instance = this;
		//Make sure that the instance is not destroyed
		//between scenes (this is optional)
		DontDestroyOnLoad(gameObject);
	}

	public string PelotaActiva(){

		string NroDeBola;

		NroDeBola = "00";

		foreach (Toggle boton in botonesPelotas) {
		
			if (boton.isOn) {
				NroDeBola = boton.GetComponent<SkinDePelotas> ().NumeroDeBola;
				break;
			}
				
		}

		return NroDeBola;

	}


	public void VerificarBolasCompradas(){

		//Leer bola elegida
		if (PlayerPrefs.HasKey (LevelManager.levelManager.s_BolaElegida)) {

			LevelManager.levelManager.numeroBolaElegida = PlayerPrefs.GetString (LevelManager.levelManager.s_BolaElegida);

		} else {

			LevelManager.levelManager.numeroBolaElegida = "00";

		}


		foreach (Toggle boton in botonesPelotas) {

			int estaDesbloqueada = PlayerPrefs.GetInt ("Pelota_" + boton.GetComponent<SkinDePelotas> ().NumeroDeBola); //Ejemplo verificar si Pelota_01 ha sido comprada
			
			if (estaDesbloqueada == 1) {

				boton.interactable = true; //Activar Toggle
				boton.GetComponent<SkinDePelotas>().btnComprar.SetActive(false); //Apagar boton de compra

				//Prender la ultima seleccionada
				if(boton.GetComponent<SkinDePelotas> ().NumeroDeBola == LevelManager.levelManager.numeroBolaElegida)
					boton.isOn = true;
				else
					boton.isOn = false;

			}

		}

	}



}
