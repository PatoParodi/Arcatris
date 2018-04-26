using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpMultiplicador : MonoBehaviour {

	public Text textoMultiplicador;

	private Color newColor;

	// Use this for initialization
	void Start () {

		GetComponent<Canvas> ().worldCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		GetComponent<Canvas> ().planeDistance = 8;
		GetComponent<Canvas> ().sortingLayerName = "UI";
		GetComponent<Canvas> ().sortingOrder = LevelManager.levelManager._sortingLayer;

		textoMultiplicador.text = "X" + LevelManager.levelManager.multiplicadorPuntosCaja.ToString ();
		//Definir color Random para el texto
		newColor = LevelManager.levelManager.seleccionarSiguienteColor();


	}

	public void Destruir(){
	
		Destroy (gameObject);
	
	}

	public void LateUpdate(){
		//Aplicar nuevo color generado en Start()
		textoMultiplicador.color = newColor;


	}

}
