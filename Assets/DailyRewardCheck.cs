using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardCheck : MonoBehaviour {

	public int NumeroDeGift;

	void Awake(){

		if(NumeroDeGift <= DailyRewardManager.Instance.GiftCounter){

			GetComponent<Image> ().color = new Color (GetComponent<Image> ().color.r, GetComponent<Image> ().color.g, GetComponent<Image> ().color.b, 255);
			GetComponentInChildren<Text> ().color = new Color (GetComponentInChildren<Text> ().color.r, GetComponentInChildren<Text> ().color.g, GetComponentInChildren<Text> ().color.b, 255);

		}

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
	}



}
