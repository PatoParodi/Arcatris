using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReadTextFrom : MonoBehaviour {

	public GameObject boton;

	// Actualizar el precio de la mejora del power up en pantalla 
	void Start () {

		GetComponent<Text>().text = boton.GetComponent<BarritasPowerUp> ().Precio.ToString ();

	}

}
