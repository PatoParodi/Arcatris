using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class DailyRewardManager : MonoBehaviour {

	public static DailyRewardManager Instance {
		get;
		private set;
	}


	private System.DateTime UltimoInicioSesion;

	public int GiftCounter; //Numero de ultima bola desbloqueada
	public GameObject AvisoPopUp;
	public GameObject DailyGifts;	//Pop up con todos los gifts
	public GameObject PantallaInicial;

	void Awake(){
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


		//Limpiar Pelotas compradas
//		PlayerPrefs.SetInt("Pelota_00",1);
//		PlayerPrefs.DeleteKey("Pelota_01");
//		PlayerPrefs.DeleteKey("Pelota_02");
//		PlayerPrefs.DeleteKey("Pelota_03");
//		PlayerPrefs.DeleteKey("Pelota_04");
//		PlayerPrefs.DeleteKey("Pelota_05");
//		PlayerPrefs.DeleteKey("Pelota_06");
	}

	void Start(){

		if (PlayerPrefs.HasKey (LevelManager.levelManager.s_UltimoInicioSesion)) {
			//Recupero el ultimo inicio de sesion para validar luego
			System.DateTime.TryParse (PlayerPrefs.GetString (LevelManager.levelManager.s_UltimoInicioSesion), out UltimoInicioSesion);

		
		} else {
			//En caso sea el primer inicio de sesion
			UltimoInicioSesion = System.DateTime.Now;
			PlayerPrefs.SetString (LevelManager.levelManager.s_UltimoInicioSesion, UltimoInicioSesion.ToString());
			GiftCounter = 0;
			PlayerPrefs.SetInt (LevelManager.levelManager.s_GiftCounter, GiftCounter);

		}

//Calcular diferencia de dias entre Dia De inicio y fecha de hoy
//Calcular si transcurrio al menos 1 dia y menos de 2 desde el ultimo inicio de sesion
		if ((System.DateTime.Now - UltimoInicioSesion).TotalHours > 24 &&
			(System.DateTime.Now - UltimoInicioSesion).TotalHours < 48) {

			GiftCounter = PlayerPrefs.GetInt (LevelManager.levelManager.s_GiftCounter);

			DesbloquearGift ();

		} else if((System.DateTime.Now - UltimoInicioSesion).TotalHours > 48){
			//Al pasar 48 horas se reinicializan los Gifts
			GiftCounter = 0;
			DesbloquearGift ();

		}

	}


	public void DesbloquearGift(){

		////// PARA TEST ONLY
		GiftCounter = PlayerPrefs.GetInt (LevelManager.levelManager.s_GiftCounter);
		///// ELIMINAR LUEGO


		if (GiftCounter == 5) {
			//Al llegar al maximo gift, resetear contador
			GiftCounter = 0;			
		}

		//Sumo 1 al contador de Gifts
		GiftCounter++;
		Debug.Log ("Se desbloqueo Gift " + GiftCounter.ToString ());
		PlayerPrefs.SetInt (LevelManager.levelManager.s_GiftCounter, GiftCounter);

		//Guardar nuevo dia y horario para contar 1 dia desde este momento.
		PlayerPrefs.SetString (LevelManager.levelManager.s_UltimoInicioSesion, System.DateTime.Now.ToString ());

		//Desactivar botones de PantallaInicial
		PantallaInicial.GetComponent<PantallaInicial>().DesactivarBotones();

		//Mostrar Pop Up de aviso
		AvisoPopUp.SetActive(true);

//		//Analytics de nueva bola desbloqueada
//		Analytics.CustomEvent ("DailyGiftShop", new Dictionary<string, object> {
//			{ "DiaGiftDesbloqueado", GiftCounter }
//		});

	}
}
