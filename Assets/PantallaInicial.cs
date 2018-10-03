using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaInicial : MonoBehaviour {

	public Button btnPlay, btnLeaderboard, btnSettings, btnShop;
	private bool BotonesActivos = true;

	public GameObject UIConfiguracion;

	// Activar o desactivar botones para que no funcionen cuando tienen un PopUp encima
	void Update(){
	
		if (BotonesActivos) {

			btnPlay.interactable = true;
			btnLeaderboard.interactable = true; 
			btnSettings.interactable = true; 
			btnShop.interactable = true;
		
		} else {

			btnPlay.interactable = false;
			btnLeaderboard.interactable = false; 
			btnSettings.interactable = false; 
			btnShop.interactable = false;

		}
	
	}

	public void DesactivarBotones(){
	
		BotonesActivos = false;
		
	}
		
	public void ActivarBotones(){

		BotonesActivos = true;

	}

	public void Configuracion(){

		UIConfiguracion.GetComponent<Animator> ().SetTrigger ("Config");

	}

}
