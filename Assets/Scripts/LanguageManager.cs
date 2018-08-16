using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Language{
public static class LanguageManager {

	public static string espanol = "ES", ingles = "EN";

	public static string[] textos = new string[30];

	public static int posicion;

	public static string[] languages = new string[2];
		public static string lanElegido;

//	public string _settingsTitle, _language, _sound, _vintageEffect, _control;

	public static void populateLanguages(){
		
		//Llenar array con idiomas posibles
		languages [0] = ingles;
		languages [1] = espanol;
		
	}

	public static void setLanguage(string language){

		lanElegido = language;

		setStrings (language);
	
	}


	static void setStrings(string language){
	
		switch (language) 
		{

			case "EN":
				textos [0] = "Settings";
				textos [1] = "Language";
				textos [2] = "Sound";
				textos [3] = "Vintage Effect";
				textos [4] = "Control";
				textos [5] = "BEST SCORE";
				textos [6] = "SHOP";
				textos [7] = "CONTINUE?";
				textos [8] = "Use One";
				textos [9] = "Get One";
				textos [10] = "PAUSED";
				textos [11] = "Not enough diamonds";
				textos [12] = "swipe";
				textos [13] = "HIT";
				textos [14] = "THE OIL BRICKS";
				textos [15] = "BEFORE\nTHEY REACH";
				textos [16] = "OUR\nFORCE FIELD";


				break;

			case "ES":
				textos [0] = "Configuracion";
				textos [1] = "Idioma";
				textos [2] = "Sonido";
				textos [3] = "Efecto Vintage";
				textos [4] = "Controles";
				textos [5] = "MEJOR PUNTAJE";
				textos [6] = "TIENDA";
				textos [7] = "CONTINUAR?";
				textos [8] = "Usar Una";
				textos [9] = "Obtener";
				textos [10] = "PAUSA";
				textos [11] = "No tiene suficientes diamantes";
				textos [12] = "desliza";
				textos [13] = "GOLPEA";
				textos [14] = "LOS BLOQUES";
				textos [15] = "ANTES\nQUE ALCANCEN";
				textos [16] = "NUESTRO\nCAMPO DE FUERZA";


				break;

			default:
				textos [0] = "Settings";
				textos [1] = "Language";
				textos [2] = "Sound";
				textos [3] = "Vintage Effect";
				textos [4] = "Control";
				textos [5] = "BEST SCORE";
				textos [6] = "SHOP";
				textos [7] = "CONTINUE?";
				textos [8] = "Use One";
				textos [9] = "Get One";
				textos [10] = "PAUSED";
				textos [11] = "Not enough diamonds";
				textos [12] = "swipe";
				textos [13] = "HIT";
				textos [14] = "THE OIL BRICKS";
				textos [15] = "BEFORE\nTHEY REACH";
				textos [16] = "OUR\nFORCE FIELD";


				break;
			}
	
	}

//	void OnEnable(){
//
//		if(posicion != null)
//			GetComponent<Text> ().text = textos [posicion];
//	
//	}

}
}
