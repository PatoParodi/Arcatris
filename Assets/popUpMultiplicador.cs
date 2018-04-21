using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpMultiplicador : MonoBehaviour {

	public Text textoMultiplicador;

	// Use this for initialization
	void Start () {

		GetComponent<Canvas> ().worldCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		GetComponent<Canvas> ().planeDistance = 8;
		GetComponent<Canvas> ().sortingLayerName = "UI";
		GetComponent<Canvas> ().sortingOrder = 6;

		textoMultiplicador.text = LevelManager.levelManager.multiplicadorPuntosCaja.ToString () + "X";

	}

	public void Destruir(){
	
		Destroy (gameObject);
	
	}

}
