using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftAssignment : MonoBehaviour {

	public int cantidadDiamantes;
	public int cantidadBolasExtra;
	public int cantidadPelotas;
	public GameObject posicionBola;

	string numeroDeBola;

	void Awake(){
	
		//PREMIO SKIN DE PELOTA
		if (cantidadPelotas > 0) {

			//Obtener al azar un numero de bola que NO haya sido desbloqueado aun
			numeroDeBola = BallManager.Instance.ObtenerNuevaBolaAlAzar ();

			if (numeroDeBola != "NO") {
				//Cargar sprite de la bola elegida
//				bolaDesbloqueada.sprite = Resources.Load ("Images/" + numeroDeBola, typeof(Sprite)) as Sprite;
				//Instanciar bola 
				GameObject bolaInstanciada = Instantiate (Resources.Load ("Pelotas/" + numeroDeBola) as GameObject, posicionBola.transform);
				bolaInstanciada.transform.localScale = new Vector3 (350, 350, 0);
//				bolaInstanciada.transform.position = Vector3.zero;

				//Modificar los sorting layers
				SpriteRenderer[] sprites = bolaInstanciada.GetComponentsInChildren<SpriteRenderer>();
				foreach (SpriteRenderer sprite in sprites) {

					sprite.sortingOrder += 10;

				}

//				//Guardar nueva bola en BD
//				PlayerPrefs.SetInt (numeroDeBola, 1);
//				//Para habilitar el nuevo boton en el shop
//				BallManager.Instance.VerificarBolasCompradas ();

			}
	
		}
	}

	public void AsignarPremio(){
	
		GameController controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();


	//PREMIO DIAMANTES
		if(cantidadDiamantes > 0){

			controller.AddMoneda (cantidadDiamantes);

		}

	//PREMIO BOLAS EXTRA
		if(cantidadBolasExtra > 0){

			controller.addExtraBallToScreen (cantidadBolasExtra);

		}

	//PREMIO SKIN DE PELOTA
		if(cantidadPelotas > 0){

			//Obtener al azar un numero de bola que NO haya sido desbloqueado aun
//			string numeroDeBola = BallManager.Instance.ObtenerNuevaBolaAlAzar();
			if(numeroDeBola != "NO"){
				//Cargar sprite de la bola elegida
//				bolaDesbloqueada = Resources.Load("Images/" + numeroDeBola, typeof(Image)) as Image;

				//Guardar nueva bola en BD
				PlayerPrefs.SetInt (numeroDeBola, 1);
				//Para habilitar el nuevo boton en el shop
				BallManager.Instance.VerificarBolasCompradas ();
				
			}
		}

	}

	public void ReproducirSonido(){

//		//Sonido al moverse la caja
//		SoundManager.soundManager.playSound (GetComponent<AudioSource> ());

	}
		

}
