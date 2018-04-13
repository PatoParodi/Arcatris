using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Estrellas : MonoBehaviour {

	public int nroEstrella;

	public GameObject botonContinue;
	public Text textContinue;


	public void pintarEstrellas(){

		botonContinue.GetComponent<Button> ().interactable = true;
		textContinue.color = new Color (textContinue.color.r, textContinue.color.g, textContinue.color.b, 255);

		for (int e = 1; e <= 5; e++) {

			// Apagar todas las estrellas
			GameObject.Find ("Estrella" + e).GetComponent<Toggle> ().isOn = false;
//			GameObject.Find ("Estrella" + e).GetComponent<Toggle> ().interactable = false;

		}

			for (int i = 1; i <= nroEstrella; i++) {
			
				//Establece el mismo estado del Toggle tocado para las estrellas anteriores
			GameObject.Find ("Estrella" + i).GetComponent<Toggle> ().isOn = true;
//			GameObject.Find ("Estrella" + i).GetComponent<Toggle> ().interactable = true;

		
		}
	}


}
