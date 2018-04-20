using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpMultiplicador : MonoBehaviour {

	public Text textoMultiplicador;

	// Use this for initialization
	void Start () {

		textoMultiplicador.text = LevelManager.levelManager.multiplicadorPuntosCaja.ToString () + "X";

	}

	public void Destruir(){
	
		Destroy (gameObject);
	
	}

}
