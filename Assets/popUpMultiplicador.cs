using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
