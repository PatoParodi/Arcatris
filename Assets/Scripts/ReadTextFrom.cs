using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReadTextFrom : MonoBehaviour {

	public GameObject boton;
    public GameObject diamante;

	// Actualizar el precio de la mejora del power up en pantalla 
	void Start () {

		GetComponent<Text>().text = boton.GetComponent<BarritasPowerUp> ().Precio.ToString ();

	}
    void Update()
    {
        if (boton.GetComponent<BarritasPowerUp>().Precio == 0)
        {
            GetComponent<Text>().text = "MAX";
            diamante.SetActive(false);

        }
        else {
            GetComponent<Text>().text = boton.GetComponent<BarritasPowerUp>().Precio.ToString();
        }
    }

}
