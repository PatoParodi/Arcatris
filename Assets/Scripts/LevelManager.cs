using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Manejar los diferentes niveles. Controlar la forma de calcularlos y
// devolver un factor para ajustar las velocidades en el GameController
namespace LevelManager
{
	public class levelManager {

		private static float velocidadPelotaBase = 4.3f;

		private static float velocidadCajasBase = 0.22f;

		public static float velocidadPelota = 4.3f;

		public static float velocidadCajas = 0.22f;

		private static int golpesPorNivel = 3;

		private static int cantRebotes;

		public static int nivelActual;

		public static void addRebote ()
		{

			cantRebotes ++;

		}

//		public static void nivelControlador(){
//
//			determinarNivel ();
//
//		}

		//Devolvera un factor de dificultad para aplicar en el juego
		public static int determinarNivel(){
		
			int nivelCalculado;
			float factorDif = 0.05f;

			//Calcular nuevo nivel
			nivelCalculado = cantRebotes / golpesPorNivel;

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

			//Se parte de la premisa que el primer nivel es 3. por eso el factor debe ser 1 para Nivel = 3
			velocidadCajas = velocidadCajasBase   * (factorDif * nivelActual + 0.85f);
			velocidadPelota = velocidadPelotaBase * (factorDif * nivelActual + 0.85f);

			Debug.Log (nivelActual);
			return nivelActual;

		}
	}
}

