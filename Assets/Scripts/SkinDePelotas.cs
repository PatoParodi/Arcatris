using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinDePelotas: MonoBehaviour {

	public string NumeroDeBola;

	public int Precio;
	public Text txtPrecio;
	public GameObject btnComprar;


	public void Awake(){
	
			GameObject ballSkin = Instantiate (Resources.Load ("Pelotas/" + NumeroDeBola) as GameObject, gameObject.transform);
			ballSkin.transform.localScale = new Vector3 (350, 350, 0);
			//Modificar los sorting layers
			SpriteRenderer[] sprites = ballSkin.GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer sprite in sprites) {

				sprite.sortingOrder += 13;

			}


	}

	public void GuardarValorEnMemoria(){
	
		if (GetComponent<Toggle> ().isOn) {
			PlayerPrefs.SetString (LevelManager.levelManager.s_BolaElegida, NumeroDeBola);
			LevelManager.levelManager.numeroBolaElegida = NumeroDeBola;
		}
	}


	public void PrenderBoton(){


		//Guardar compra en BD
		PlayerPrefs.SetInt (NumeroDeBola, 1); //Ejemplo guardar (Pelota) 01 si ha sido comprada (0 NO, 1 SI)

		//Analytics
		AnalyticsManager.Instance.ComprarPelota(NumeroDeBola);

		//Prendo y activo el Toggle
		gameObject.SetActive (true);
		btnComprar.SetActive(false); //Apagar boton de compra
		GetComponent<Toggle>().interactable = true;
		//Seleccionar Pelota comprada
		GetComponent<Toggle> ().isOn = true;

	}
		
}
