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

    private void Awake()
    {

        //Calcular precios FRECUENCIA segun Nivel
        if(Frecuencia)
        {
            if (RedBall)
                Precio = BuscarPrecioFrecuencia(PowerUpManager.Instance.RedBall.NivelFrecuencia + 1);
            if (BajarBrea)
                Precio = BuscarPrecioFrecuencia(PowerUpManager.Instance.BajarBrea.NivelFrecuencia + 1);
            if (MultipleBall)
                Precio = BuscarPrecioFrecuencia(PowerUpManager.Instance.MultipleBall.NivelFrecuencia + 1);
        }

        //Calcular precios PODER segun Nivel
        if (Poder)
        {
            if (RedBall)
                Precio = BuscarPrecioPoder(PowerUpManager.Instance.RedBall.NivelPoder + 1);
            if (BajarBrea)
                Precio = BuscarPrecioPoder(PowerUpManager.Instance.BajarBrea.NivelPoder + 1);

        }

    }

    int BuscarPrecioFrecuencia(int nivel){

        switch(nivel){
            case 1:
                return 100;
            case 2:
                return 200;
            case 3:
                return 350;
            case 4:
                return 600;
            case 5:
                return 900;
        }

        return 0;
    }

    int BuscarPrecioPoder(int nivel)
    {

        switch (nivel)
        {
            case 1:
                return 200;
            case 2:
                return 300;
            case 3:
                return 425;
            case 4:
                return 600;
            case 5:
                return 800;
        }

        return 0;
    }

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
                    Precio = BuscarPrecioFrecuencia(nivel + 1);
                }

				if (MultipleBall){
					PowerUpManager.Instance.MultipleBall.SubirNivelFrecuencia ();
					nivel = PowerUpManager.Instance.MultipleBall.NivelFrecuencia;
				}

				if (BajarBrea){
					PowerUpManager.Instance.BajarBrea.SubirNivelFrecuencia ();
					nivel = PowerUpManager.Instance.BajarBrea.NivelFrecuencia;
				}

                Precio = BuscarPrecioFrecuencia(nivel + 1);

            }
            else if (Poder) {
				if (RedBall){
					PowerUpManager.Instance.RedBall.SubirNivelPoder ();
					nivel = PowerUpManager.Instance.RedBall.NivelPoder;
                    Precio = BuscarPrecioPoder(nivel + 1);
                }

				if (MultipleBall){
					PowerUpManager.Instance.MultipleBall.SubirNivelPoder ();
					nivel = PowerUpManager.Instance.MultipleBall.NivelPoder;
				}

				if (BajarBrea){
					PowerUpManager.Instance.BajarBrea.SubirNivelPoder ();
					nivel = PowerUpManager.Instance.BajarBrea.NivelPoder;
                    Precio = BuscarPrecioPoder(nivel + 1);
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
        {
            GetComponent<Button>().interactable = false;
            Precio = 0;
        }
	
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
