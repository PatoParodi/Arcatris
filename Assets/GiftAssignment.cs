using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftAssignment : MonoBehaviour {

	public int cantidadDiamantes;
	public int cantidadBolasExtra;
	public int cantidadPelotas;


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

		}

	}

}
