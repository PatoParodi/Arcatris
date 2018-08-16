using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Language;


public class LanguageGetText : MonoBehaviour {

	public int posicion;

	private int posicionLanguage = 0;


	void OnEnable(){

		if(posicion >= 0)
			GetComponent<Text>().text = LanguageManager.textos [posicion];

		//On Enable para el boton de lenguaje con bandera
		if (posicion < 0) {
			//Buscar imagen para el boton
			SelectImageOnButton (LanguageManager.lanElegido);

			//identificar posicion en el array
			for (int i = 0; i < LanguageManager.languages.Length; i++) {
				if (LanguageManager.lanElegido == LanguageManager.languages [i])
					posicionLanguage = i;
			}

		}

	}

	void Update(){

		if(posicion >= 0) // && GetComponent<Text>().text == "")
			GetComponent<Text>().text = LanguageManager.textos [posicion];

	}

	public void switchLanguage(){

//		string[] boton = new string[2];
//
//		//Llenar array con idiomas posibles
//		boton [0] = LanguageManager.ingles;
//		//		boton [0].iconName = "icon_lan_ES";
//		boton [1] = LanguageManager.espanol;
//		//		boton [1].iconName = "icon_lan_EN";

		posicionLanguage += 1;
		//Verificar si esta posicionado en el ultimo icono
		if (posicionLanguage >= LanguageManager.languages.Length)
			posicionLanguage = 0;

		//Cambiar textos al idioma elegido
		LanguageManager.setLanguage (LanguageManager.languages [posicionLanguage]);

		//Imagen del boton
		SelectImageOnButton(LanguageManager.languages [posicionLanguage]);

		//Guardar idioma seleccionado en Memoria
		PlayerPrefs.SetString(LevelManager.levelManager.s_Language,LanguageManager.languages [posicionLanguage]);
		
		//Refrescar pantalla
//		GameObject.FindGameObjectWithTag("UI_Config").GetComponent<Animator>().SetTrigger("Refrescar");

	}

	public void SelectImageOnButton(string idioma){

		//Imagen del boton
		string _sprite = "Images/icon_lan_" + idioma;
		GetComponent<SpriteRenderer> ().sprite = Resources.Load (_sprite,typeof(Sprite)) as Sprite;
	
	}
		

}
