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
	
	}

	public void switchLanguage(){

		string[] boton = new string[2];

		//Llenar array con idiomas posibles
		boton [0] = LanguageManager.espanol;
		boton [1] = LanguageManager.ingles;

		posicionLanguage += 1;
		//Verificar si esta posicionado en el ultimo icono
		if (posicionLanguage >= boton.Length)
			posicionLanguage = 0;

		string _sprite = "Images/icon_lan_" + boton [posicionLanguage];
		GetComponent<SpriteRenderer> ().sprite = Resources.Load (_sprite,typeof(Sprite)) as Sprite;

		//Cambiar textos al idioma elegido
		LanguageManager.setLanguage (boton [posicionLanguage]);
	
		//Refrescar pantalla
		GameObject.FindGameObjectWithTag("UI_Config").GetComponent<Animator>().SetTrigger("Refrescar");

	}
		
}
