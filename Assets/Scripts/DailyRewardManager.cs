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
	GameObject gift;

	public int GiftCounter; //Numero de ultimo gift desbloqueada
	public GameObject DailyAvisoPopUp;
	public float OpeningBoxTime = 2.5f;	//Tiempo de animacion al abrir la caja
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

	}

	void Start(){

		if (PlayerPrefs.HasKey (LevelManager.levelManager.s_UltimoInicioSesion)) {
			//Recupero el ultimo inicio de sesion para validar luego
			System.DateTime.TryParse (PlayerPrefs.GetString (LevelManager.levelManager.s_UltimoInicioSesion), out UltimoInicioSesion);

		
		} else {
			//En caso sea el primer inicio de sesion
			UltimoInicioSesion = System.DateTime.Now;
			PlayerPrefs.SetString (LevelManager.levelManager.s_UltimoInicioSesion, UltimoInicioSesion.ToString ());
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

	public void LeerNumeroGiftDeMemoria(){
	
		////// PARA TEST ONLY
//		GiftCounter = 2;
//		GiftCounter = PlayerPrefs.GetInt (LevelManager.levelManager.s_GiftCounter);
		///// ELIMINAR LUEGO

	}

	public void DesbloquearGift(){


		if (GiftCounter == 5) {
			//Al llegar al maximo gift, resetear contador
			GiftCounter = 0;			
		}

		//Sumo 1 al contador de Gifts
		GiftCounter++;
		PlayerPrefs.SetInt (LevelManager.levelManager.s_GiftCounter, GiftCounter);

		//Guardar nuevo dia y horario para contar 1 dia desde este momento.
		PlayerPrefs.SetString (LevelManager.levelManager.s_UltimoInicioSesion, System.DateTime.Now.ToString ());

		//Desactivar botones de PantallaInicial
		PantallaInicial.GetComponent<PantallaInicial>().DesactivarBotones();

		//Mostrar Pop Up de aviso
		DailyAvisoPopUp.SetActive(true);

		StartCoroutine (abriendoPopUp (DailyAvisoPopUp.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length));
			
		//Analytics
		AnalyticsManager.Instance.GiftDesbloqueado(GiftCounter);

	}

	public IEnumerator abriendoPopUp(float tiempo){
	
		yield return new WaitForSecondsRealtime (tiempo);

		//Buscar prefab del Gift correspondiente
		gift = Instantiate (Resources.Load ("Gifts/Gift_" + GiftCounter.ToString ()) as GameObject);
		gift.transform.SetParent (DailyAvisoPopUp.transform);
		gift.transform.localScale = Vector3.one;
	
	}

	public void AbrirCaja(){
		
		//Abrir caja
		gift.GetComponent<Animator> ().SetTrigger ("Open");

		//Atenuar pop up
		DailyAvisoPopUp.GetComponent<Animator> ().SetTrigger ("FadeOut");

		//Contar tiempo hasta mostrar la siguiente pantalla con los gift por dia
		StartCoroutine (MostarDailyGifts ());

		//Reproducir sonido al abrir caja
		SoundManager.soundManager.playSound(GetComponent<AudioSource>());

	}

	public IEnumerator MostarDailyGifts(){
	
		yield return new WaitForSecondsRealtime (OpeningBoxTime);

		DailyAvisoPopUp.SetActive (false);
		DailyGifts.SetActive (true);

		//Sumar el premio
		gift.GetComponent<GiftAssignment> ().AsignarPremio ();

		//Destruir objeto
		Destroy(gift);
	
	}

}
