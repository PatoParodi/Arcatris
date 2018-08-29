using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardCheck : MonoBehaviour {

	public string NumeroDeBola;

	private int UltimaBolaDesbloqueada;

//	void Awake(){
//	
//		UltimaBolaDesbloqueada = PlayerPrefs.GetInt (LevelManager.levelManager.s_BolasDesbloqueadas);
//
//		if(UltimaBolaDesbloqueada >= NumeroDeBola){
//			//Desbloquear
//			GetComponent<Toggle>().interactable = true;
//		
//		}
//
//		//Seleccionar la ultima guardada
//		if (NumeroDeBola.ToString () == PlayerPrefs.GetString (LevelManager.levelManager.s_BolaElegida)) {
//
//			GetComponent<Toggle> ().isOn = true;
//
//		} else {
//
//			GetComponent<Toggle> ().isOn = false;
//
//		}
//	
//	}
//
//	public void GuardarValorEnMemoria(){
//	
//		if (GetComponent<Toggle> ().isOn) {
//			PlayerPrefs.SetString (LevelManager.levelManager.s_BolaElegida, NumeroDeBola.ToString());
//			LevelManager.levelManager.numeroBolaElegida = NumeroDeBola.ToString();
//		}
//	}

}
