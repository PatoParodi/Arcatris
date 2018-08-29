using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinDePelotas: MonoBehaviour {

	public string NumeroDeBola;

	public int Precio;
	public Text txtPrecio;
	public GameObject btnComprar;


	void Awake(){

		if(txtPrecio != null)
			txtPrecio.text = Precio.ToString ();

//		//Seleccionar la ultima guardada
//		if (NumeroDeBola.ToString () == PlayerPrefs.GetString (LevelManager.levelManager.s_BolaElegida)) {
//
//			GetComponent<Toggle> ().isOn = true;
//
//		} else {
//
//			GetComponent<Toggle> ().isOn = false;
//
//		}
	
	}

	public void GuardarValorEnMemoria(){
	
		if (GetComponent<Toggle> ().isOn) {
			PlayerPrefs.SetString (LevelManager.levelManager.s_BolaElegida, NumeroDeBola);
			LevelManager.levelManager.numeroBolaElegida = NumeroDeBola;
		}
	}


	public void ComprarPelota(){

		Debug.Log ("Comprando pelota");
		btnComprar.SetActive(false); //Apagar boton de compra
	
	}

}
