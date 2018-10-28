using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : ScriptableObject {

	public string Nombre;
	public int NivelFrecuencia = 1, NivelPoder = 1, PoderCantidad;
	public float Frecuencia, PoderDuracion, PoderBajar;

	float FactorFrecuencia = 2f, FactorPoder = 0.2f; //20%
	string s_freq, s_poder;

	float BajarBreaBase = 4;

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

		CalcularPropiedades ();
	}

	public void SubirNivelPoder(){
		NivelPoder++;
		PlayerPrefs.SetInt (s_poder,NivelPoder);

		//Analytics
		AnalyticsManager.Instance.ComprarPowerUp("Poder",Nombre);

		CalcularPropiedades ();
	}

	public void CalcularPropiedades(){

		//Calcular Frecuencia
        Frecuencia = FactorFrecuencia + (NivelFrecuencia * 1.1f); //(1 + FactorFrecuencia * NivelFrecuencia/10);

		//Se adiciona un FactorPoder% por cada nivel
		PoderBajar = BajarBreaBase + (BajarBreaBase * NivelPoder * FactorPoder);

		//El nivel determina la cantidad de Bolas
		PoderCantidad = 1 * NivelPoder;

		//Un segundo extra por nivel
		PoderDuracion = 6 + NivelPoder;

	}

}
