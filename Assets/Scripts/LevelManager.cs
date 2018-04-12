using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Manejar los diferentes niveles. Controlar la forma de calcularlos y
//Nivelador del juego segun skills del jugador
namespace LevelManager
{
	public class levelManager {

		private static float velocidadPelotaBase = 4.3f;

		private static float velocidadCajasBase = 0.22f;

		public static float velocidadPelota = 4.3f;

		public static float velocidadCajas = 0.22f;

		private static int golpesPorNivelMinimo = 12;

		private static int golpesPorNivel = 30;

		private static int cantRebotes;

		public static int nivelActual;

		public static void addRebote ()
		{

			cantRebotes ++;

		}

		//Devolvera un factor de dificultad para aplicar en el juego
		public static int determinarNivel(){
		
			float factorDif = 0.05f;

//			//Calcular nuevo nivel
//			nivelCalculado = cantRebotes / golpesPorNivel;
//
			//Verificar si el nuevo nivel supera el anterior
			nivelActual = PlayerPrefs.GetInt ("TEST_Nivel");

			//Subir o bajar el nivel segun corresponda
//			if (nivelCalculado > nivelActual)
			if(cantRebotes > golpesPorNivel)
				nivelActual++;
			else if(cantRebotes < golpesPorNivelMinimo)
				nivelActual--;

			if (nivelActual < 1)
				nivelActual = 1;

			//Guardar nuevo nivel
			PlayerPrefs.SetInt ("TEST_Nivel", nivelActual);

			//Reiniciar cantidad de Rebotes para siguiente partida
			cantRebotes = 0;

			//Se parte de la premisa que el primer nivel es 5. por eso el factor debe ser 1 para Nivel = 5
			velocidadCajas = velocidadCajasBase   * (factorDif * nivelActual + 0.85f);
			velocidadPelota = velocidadPelotaBase * (factorDif * nivelActual + 0.85f);

			Debug.Log (nivelActual);
			return nivelActual;

		}
	}
}

