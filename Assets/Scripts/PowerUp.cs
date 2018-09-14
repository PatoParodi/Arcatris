using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : ScriptableObject {

	public string Nombre;
	public int NivelFrecuencia = 1, NivelPoder = 1, PoderCantidad;
	public float Frecuencia, PoderDuracion, PoderBajar;

	float FactorFrecuencia = 2.5f, FactorPoder;
	string s_freq, s_poder;

	//Constructor
	public PowerUp(string nombre){
	
		Nombre = nombre;

		s_freq = "NivelFrecuencia" + Nombre;
		s_poder = "NivelPoder" + Nombre;
	
	}
		
	public void LeerNiveles(){

		if (PlayerPrefs.HasKey (s_freq))
			NivelFrecuencia = PlayerPrefs.GetInt (s_freq);

		if (PlayerPrefs.HasKey (s_poder))
			NivelPoder = PlayerPrefs.GetInt (s_poder);

		//Calcular Propiedades en base al nivel
		CalcularPropiedades ();

	}

	public void SubirNivelFrecuencia(){
		NivelFrecuencia++;
		PlayerPrefs.SetInt (s_freq, NivelFrecuencia);

		//Analytics
		AnalyticsManager.Instance.ComprarPowerUp("Frecuencia",Nombre);
	}

	public void SubirNivelPoder(){
		NivelPoder++;
		PlayerPrefs.SetInt (s_poder,NivelPoder);

		//Analytics
		AnalyticsManager.Instance.ComprarPowerUp("Poder",Nombre);
	}

	public void CalcularPropiedades(){

		//Calcular Frecuencia
		Frecuencia += FactorFrecuencia * NivelFrecuencia;

		//Calcular Poder
		if(PoderBajar != null){

			PoderBajar = 3 * NivelPoder;
		
		}

		if (PoderCantidad != null) {

			PoderCantidad = 1 * NivelPoder;
		
		}

		if (PoderDuracion != null) {

			PoderDuracion = 6 * NivelPoder;
		
		}

	}

}
