using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Estrellas : MonoBehaviour {

	public int nroEstrella;

	public GameObject botonContinue;

	public void pintarEstrellas(){

		botonContinue.GetComponent<Button> ().interactable = true;

		for (int e = 1; e <= 5; e++) {

			// Apagar todas las estrellas
			GameObject.Find ("Estrella" + e).GetComponent<Toggle> ().isOn = false;

		}

			for (int i = 1; i <= nroEstrella; i++) {
			
				//Establece el mismo estado del Toggle tocado para las estrellas anteriores
			GameObject.Find ("Estrella" + i).GetComponent<Toggle> ().isOn = true;
		
		}
	}


}
