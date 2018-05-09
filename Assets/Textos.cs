using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textos : MonoBehaviour {

	public Text txtNivel;
	public Text txtDificultad;
	public Text txtVelocidadCajas;
	
	// Update is called once per frame
	void Update () {

		if (txtNivel != null)
			txtNivel.text = LevelManager.levelManager.nivelActual.ToString();

		if (txtDificultad != null)
			txtDificultad.text = LevelManager.levelManager.dificultadActual.ToString();

		if (txtVelocidadCajas != null)
			txtVelocidadCajas.text = LevelManager.levelManager.velocidadCajas.ToString();


	}
}
