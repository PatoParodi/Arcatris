using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardCheck : MonoBehaviour {

	public int NumeroDeBola;

	private int UltimaBolaDesbloqueada;

	void Awake(){
	
		UltimaBolaDesbloqueada = PlayerPrefs.GetInt (LevelManager.levelManager.s_BolasDesbloqueadas);

		if(UltimaBolaDesbloqueada >= NumeroDeBola){
			//Desbloquear
			GetComponent<Toggle>().interactable = true;
		
		}

	
	}

	public void GuardarValorEnMemoria(){
	
		if (GetComponent<Toggle> ().isOn) {
			PlayerPrefs.SetInt (LevelManager.levelManager.s_BolaElegida, NumeroDeBola);
			LevelManager.levelManager.numeroBolaElegida = NumeroDeBola;
		}
	}

}
