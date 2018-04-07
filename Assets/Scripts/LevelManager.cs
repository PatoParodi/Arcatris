using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Manejar los diferentes niveles. Controlar la forma de calcularlos y
// devolver un factor para ajustar las velocidades en el GameController
namespace LevelManager
{
	public class levelManager {

		public static float velocidadPelota = 4.3f;

		public static float velocidadCajas = 0.22f;

		private static int golpesPorNivel = 10;

		private int cantRebotes = 0;

		public void addRebote ()
		{

			cantRebotes += 1;

		}

		public static void nivelControlador(){

			determinarNivel ();

		}

		//Devolvera un factor de dificultad para aplicar en el juego
		public float determinarNivel(){
		
			int nivelActual, nivelCalculado;
			float factorDif = 0.3f;

			//Calcular nuevo nivel
			nivelCalculado = cantRebotes % golpesPorNivel;

			//Verificar si el nuevo nivel supera el anterior
			nivelActual = PlayerPrefs.GetInt ("TEST_Nivel");

			//Subir o bajar el nivel segun corresponda
			if (nivelCalculado > nivelActual)
				nivelActual++;
			else if (nivelCalculado < nivelActual)
				nivelActual--;

			if (nivelActual < 1)
				nivelActual = 1;

			//Guardar nuevo nivel
			PlayerPrefs.SetInt ("TEST_Nivel", nivelActual);

			//Reiniciar cantidad de Rebotes para siguiente partida
			cantRebotes = 0;

			velocidadCajas = velocidadCajas * factorDif * nivelActual;
			velocidadPelota = velocidadPelota * factorDif * nivelActual;


			return nivelActual * factorDif;

		}
	}
}

