using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateDailyReward : MonoBehaviour {


	void Start(){
	
		GetComponent<Text> ().text = Screen.width.ToString ();
	
	}

	// Update is called once per frame
	void Update () {
	
//		if (GetComponent<Text> ().text == "null") {
//		
//			GetComponent<Text> ().text = PlayerPrefs.GetString (LevelManager.levelManager.s_UltimoInicioSesion);
//		
//		}

	}
}
