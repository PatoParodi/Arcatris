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
		public static int nivelActual;
		public static int puntosBaseCaja = 20;
		public static int multiplicadorPuntosCaja = 0;

		private static int golpesPorNivelMinimo = 10;
		private static int golpesPorNivel = 36;
		private static bool flagBajarNivel = false;
		public static int cantRebotes;
		public static int contadorCajasDerretidas;

		public static string s_Level = "TEST_Nivel";
		public static string s_sound = "Sound";
		public static string s_TouchPad = "SelectorTouchPad";
		public static string s_Rated = "Rated";

		public static string s_On = "On";
		public static string s_Off = "Off";

		public static void ReinicializarMultiplicadorPuntos(){
		
			multiplicadorPuntosCaja = 0;
		
		}

		public static void addRebote ()
		{

			cantRebotes ++;

		}

		//Devolvera un factor de dificultad para aplicar en el juego
		public static int determinarNivel(bool calcular){
		
			float factorDif = 0.05f; // Aumento progresivo de dificultad 5%

			//Verificar si el nuevo nivel supera el anterior
			nivelActual = PlayerPrefs.GetInt (s_Level);

			//Subir o bajar el nivel segun corresponda
			if (calcular) {
				//Subir Nivel
				if (cantRebotes >= golpesPorNivel) {
					nivelActual++;
					flagBajarNivel = false;
				}
				//Bajar Nivel a la segunda vez que no llega al minimo
				if (flagBajarNivel == true) {
					if (cantRebotes <= golpesPorNivelMinimo){
						nivelActual--;
						flagBajarNivel = false;
					}
				} else
					flagBajarNivel = true;

				if (nivelActual < 1)
					nivelActual = 1;

				if (nivelActual > 10)
					nivelActual = 10;
			}

			//Guardar nuevo nivel
			PlayerPrefs.SetInt (s_Level, nivelActual);

			//Reiniciar cantidad de Rebotes para siguiente partida
			cantRebotes = 0;

			//Se parte de la premisa que el primer nivel es 5. por eso el factor debe ser 1 para Nivel = 5
			velocidadCajas = velocidadCajasBase   * (factorDif * nivelActual + 0.85f);
			velocidadPelota = velocidadPelotaBase * (factorDif * nivelActual + 0.85f);

			return nivelActual;

		}
	}
		
}

