using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour {

	public static AnalyticsManager Instance {
		get;
		private set;
	}


	// Use this for initialization
	void Awake () {
		//First we check if there are any other instances conflicting
		if (Instance != null && Instance != this)
		{
			//Destroy other instances if they are not the same
			Destroy(gameObject);
		}
		//Save our current singleton instance
		Instance = this;
		//Make sure that the instance is not destroyed
		//between scenes (this is optional)
		DontDestroyOnLoad(gameObject);

	}


	public void NuevoJuego(string BolaElegida){
	
		Analytics.CustomEvent ("NuevaPartida", new Dictionary<string, object> {
			{ "Sonido", SoundManager.soundManager.soundEnabled },
			{ "Dificultad", LevelManager.levelManager.dificultadActual},
			{ "BolaElegida", BolaElegida}
		});
	
	}

	public void ComprarPelota(string numeroDePelota){
	
		Analytics.CustomEvent ("CompraNuevaPelota", new Dictionary<string, object> {
			{ "NumeroDePelota", numeroDePelota }
		});
	
	}

	public void ComprarPowerUp(string tipoMejora, string powerup){
	
		//Analytics de nueva compra de Mejora de Power Up
		Analytics.CustomEvent ("CompraMejoraPowerUp", new Dictionary<string, object> {
			{ "TipoMejora", tipoMejora },
			{ "PowerUp", powerup }
		});
	
	}

	public void GiftDesbloqueado(int DiaDesbloqueado){
	
		//Analytics de nueva bola desbloqueada
		Analytics.CustomEvent ("DailyGiftShop", new Dictionary<string, object> {
			{ "GiftDesbloqueado", DiaDesbloqueado.ToString() }
		});
	
	}

    public void RateUsPopUp(int Stars){
        //Analytics de Rate from pop up
        Analytics.CustomEvent("RateUsPopUp", new Dictionary<string, object> {
            { "CantidadEstrellas", Stars.ToString() }
        });
    }
}
