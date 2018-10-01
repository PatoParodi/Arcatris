using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarritasPowerUp : MonoBehaviour {

	public int MaxNivel;
	public Color color;
	public GameObject contenedor;

	public bool Frecuencia, Poder, RedBall, MultipleBall, BajarBrea;

	public int Precio;
	public GameObject _NoCoinsPopUp;


	bool barritasGeneradas = false; 

	// Use this for initialization
	void OnEnable () {

		int nivel = 1;

		if (!barritasGeneradas) {

			barritasGeneradas = true;

			float celSize = (180 - (5 * (MaxNivel - 1))) / MaxNivel;
			
			contenedor.GetComponent<GridLayoutGroup> ().cellSize = new Vector3 (celSize, 10, 1);
			contenedor.GetComponent<GridLayoutGroup> ().spacing = new Vector2 (5, 0);

			GameObject barrita;

			for (int i = 1; i <= MaxNivel; i++) {
			
				barrita = Instantiate (Resources.Load ("Images/powerup_barra") as GameObject, Vector3.zero, Quaternion.identity, contenedor.transform);

				if (Frecuencia) {
					if (RedBall)
					if (i <= PowerUpManager.Instance.RedBall.NivelFrecuencia){
						barrita.GetComponent<Image> ().color = color;
						nivel = PowerUpManager.Instance.RedBall.NivelFrecuencia;
					}
				
					if (MultipleBall)
					if (i <= PowerUpManager.Instance.MultipleBall.NivelFrecuencia){
						barrita.GetComponent<Image> ().color = color;
						nivel = PowerUpManager.Instance.MultipleBall.NivelFrecuencia;
					}
				
					if (BajarBrea)
					if (i <= PowerUpManager.Instance.BajarBrea.NivelFrecuencia){
						barrita.GetComponent<Image> ().color = color;
						nivel = PowerUpManager.Instance.BajarBrea.NivelFrecuencia;
					}
	
				} else if (Poder) {
					if (RedBall)
					if (i <= PowerUpManager.Instance.RedBall.NivelPoder){
						barrita.GetComponent<Image> ().color = color;
						nivel = PowerUpManager.Instance.RedBall.NivelPoder;
					}

					if (MultipleBall)
					if (i <= PowerUpManager.Instance.MultipleBall.NivelPoder){
						barrita.GetComponent<Image> ().color = color;
						nivel = PowerUpManager.Instance.MultipleBall.NivelPoder;
					}

					if (BajarBrea)
					if (i <= PowerUpManager.Instance.BajarBrea.NivelPoder){
						barrita.GetComponent<Image> ().color = color;
						nivel = PowerUpManager.Instance.BajarBrea.NivelPoder;
					}
				
				}
			}
		}

		//Verificar si se alcanza el nivel maximo
		isNivelMaximoAlcanzado(nivel);

	}

	public void ComprarPowerUp(){

		int nivel = 1;

		GameController _controller =  GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		//Determiniar si tiene suficientes diamantes
		if(_controller.Comprar(Precio)){

			if (Frecuencia) {
				if (RedBall) {
					PowerUpManager.Instance.RedBall.SubirNivelFrecuencia ();
					nivel = PowerUpManager.Instance.RedBall.NivelFrecuencia;
				}

				if (MultipleBall){
					PowerUpManager.Instance.MultipleBall.SubirNivelFrecuencia ();
					nivel = PowerUpManager.Instance.MultipleBall.NivelFrecuencia;
				}

				if (BajarBrea){
					PowerUpManager.Instance.BajarBrea.SubirNivelFrecuencia ();
					nivel = PowerUpManager.Instance.BajarBrea.NivelFrecuencia;
				}

			} else if (Poder) {
				if (RedBall){
					PowerUpManager.Instance.RedBall.SubirNivelPoder ();
					nivel = PowerUpManager.Instance.RedBall.NivelPoder;
				}

				if (MultipleBall){
					PowerUpManager.Instance.MultipleBall.SubirNivelPoder ();
					nivel = PowerUpManager.Instance.MultipleBall.NivelPoder;
				}

				if (BajarBrea){
					PowerUpManager.Instance.BajarBrea.SubirNivelPoder ();
					nivel = PowerUpManager.Instance.BajarBrea.NivelPoder;
				}

			}

			ColorearBarritas (nivel);

			//Verificar si se alcanza el nivel maximo
			isNivelMaximoAlcanzado(nivel);

			//Reproducir sonido
			SoundManager.soundManager.playSound(GetComponent<AudioSource>());

			//Analytics estan en los metodos de SubirNivelPoder y SubirNivelFrecuencia

		} else {

			_NoCoinsPopUp.GetComponent<Animator> ().SetTrigger ("Show");

		}

	}

	public void isNivelMaximoAlcanzado(int nivel){
	
		if (nivel >= MaxNivel)
			GetComponent<Button> ().interactable = false;
	
	}

	public void ColorearBarritas(int nivel){
	
		Image[] imagenes = contenedor.GetComponentsInChildren<Image> ();
		int i = 0;
		foreach (Image imagen in imagenes) {
			i++;
			if(i <= nivel)
				imagen.color = color;
		}
	
	}

}
