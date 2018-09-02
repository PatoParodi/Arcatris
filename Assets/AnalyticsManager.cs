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


	public void NuevoJuego(){
	
		Analytics.CustomEvent ("NuevaPartida", new Dictionary<string, object> {
			{ "Sonido", SoundManager.soundManager.soundEnabled },
			{ "Dificultad", LevelManager.levelManager.dificultadActual}
		});
	
	}

}
