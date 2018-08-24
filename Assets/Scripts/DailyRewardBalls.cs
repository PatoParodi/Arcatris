using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardBalls : MonoBehaviour {


	private System.DateTime UltimoInicioSesion;

	public int BolasDesbloqueadas; //Numero de ultima bola desbloqueada
	public GameObject AvisoPopUp;
	public GameObject PantallaInicial;

	void Start(){

		if (PlayerPrefs.HasKey (LevelManager.levelManager.s_UltimoInicioSesion)) {
			
			System.DateTime.TryParse (PlayerPrefs.GetString (LevelManager.levelManager.s_UltimoInicioSesion), out UltimoInicioSesion);
			//Calcular diferencia de dias entre Dia De inicio y fecha de hoy
			Debug.Log((System.DateTime.Now - UltimoInicioSesion).TotalDays);	//ESTE FUNCIONA

		
		} else {
			UltimoInicioSesion = System.DateTime.Now;
			PlayerPrefs.SetString (LevelManager.levelManager.s_UltimoInicioSesion, UltimoInicioSesion.ToString());
			PlayerPrefs.SetInt (LevelManager.levelManager.s_BolasDesbloqueadas, 0);

		}
				
//Calcular si transcurrio al menos 1 dia desde el ultimo inicio de sesion
		if ((System.DateTime.Now - UltimoInicioSesion).TotalDays > 1) {

			BolasDesbloqueadas = PlayerPrefs.GetInt (LevelManager.levelManager.s_BolasDesbloqueadas);
			BolasDesbloqueadas++;
			PlayerPrefs.SetInt (LevelManager.levelManager.s_BolasDesbloqueadas, BolasDesbloqueadas);

			//Guardar nuevo dia y horario para contar 1 dia desde este momento.
			PlayerPrefs.SetString (LevelManager.levelManager.s_UltimoInicioSesion, System.DateTime.Now.ToString ());

			//Desactivar botones de PantallaInicial
			PantallaInicial.GetComponent<PantallaInicial>().DesactivarBotones();

			//Mostrar Pop Up de aviso
			AvisoPopUp.SetActive(true);

		}

	}

}
