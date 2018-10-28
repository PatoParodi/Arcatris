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
			PlayerPrefs.SetInt (LevelManager.levelManager.numeroBolaElegida, 1);

		}


		foreach (GameObject boton in botonesPelotas) {
			//El nombre de la variable guardada es el numero de bola
			int estaDesbloqueada = PlayerPrefs.GetInt (boton.GetComponent<SkinDePelotas> ().NumeroDeBola); //Ejemplo verificar si (Pelota) 01 ha sido comprada (0 NO, 1 SI)

			if (estaDesbloqueada == 1) {

				boton.SetActive (true);	//Prender el selector
				boton.GetComponent<Toggle> ().interactable = true; //Activar Toggle
				if (boton.GetComponent<SkinDePelotas> ().btnComprar != null) {
					boton.GetComponent<SkinDePelotas> ().btnComprar.SetActive (false); //Apagar boton de compra
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

	public string ObtenerNuevaBolaAlAzar(){
	//Devuelve el nombre del Sprite de la bola elegida

		string Salvavidas = "NO";

		foreach (GameObject boton in botonesPelotas) {
			//Solo aplica para las NO desbloqueadas
			if (boton.activeSelf == false) {

				if (Salvavidas == "NO") {
					//En caso de que la logica no devuelva ninguna, la primera encontrada sera el "salvavidas"
					Salvavidas = boton.GetComponent<SkinDePelotas> ().NumeroDeBola;
				}

				//Seleccionar un numero de bola al azar
				int numeroAlAzar = Random.Range(0,2);
				if (numeroAlAzar == 1) { 

					return boton.GetComponent<SkinDePelotas> ().NumeroDeBola;
				
				}

			}
		
		}
	//Si no se encontro bola para devolver, devolver la primera encontrada
		return Salvavidas;

	}



}
