using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallManager : MonoBehaviour {

	public static BallManager Instance {
		get;
		private set;
	}

	public GameObject[] botonesPelotas;

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


	public void VerificarBolasCompradas(){

		//Leer bola elegida
		if (PlayerPrefs.HasKey (LevelManager.levelManager.s_BolaElegida)) {

			LevelManager.levelManager.numeroBolaElegida = PlayerPrefs.GetString (LevelManager.levelManager.s_BolaElegida);

		} else {

			LevelManager.levelManager.numeroBolaElegida = "00";
			PlayerPrefs.SetString (LevelManager.levelManager.s_BolaElegida, "00");
			PlayerPrefs.SetInt ("Pelota_00", 1);

		}


		foreach (GameObject boton in botonesPelotas) {

			int estaDesbloqueada = PlayerPrefs.GetInt ("Pelota_" + boton.GetComponent<SkinDePelotas> ().NumeroDeBola); //Ejemplo verificar si Pelota_01 ha sido comprada

			if (estaDesbloqueada == 1) {

				boton.GetComponent<Toggle> ().interactable = true; //Activar Toggle
				if (boton.GetComponent<SkinDePelotas> ().btnComprar != null) {
					boton.GetComponent<SkinDePelotas> ().btnComprar.SetActive (false); //Apagar boton de compra
					boton.SetActive (true);	//Prender el selector
				}

				//Prender la ultima seleccionada
				if (boton.GetComponent<SkinDePelotas> ().NumeroDeBola == LevelManager.levelManager.numeroBolaElegida)
					boton.GetComponent<Toggle> ().isOn = true;
				else
					boton.GetComponent<Toggle> ().isOn = false;

			} else {
			//Actualizar precio en boton
				boton.GetComponent<SkinDePelotas>().txtPrecio.text = boton.GetComponent<SkinDePelotas>().Precio.ToString ();
			
			}

		}

	}



}
