using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Manejar los diferentes niveles. Controlar la forma de calcularlos y
//Nivelador del juego segun skills del jugador
namespace LevelManager
{
	public class levelManager {

		//Pelota
		public static float velocidadPelota = 4.3f;
		public static float velocidadPelotaBase = 3.4f;
		public static float velocidadPelotaAumento = 0.4f; //10% por nivel
		public static int dificultadActual = 1;
		public static int dificultadMaxima = 7;
		public static string numeroBolaElegida;

		//Cajas
		private static float velocidadCajasBase = 0.24f;
		public static float velocidadCajas = 0.22f;
		public static float aumentoVelocidadCajas = 0.02f;
		public static int puntosBaseCaja = 20;
		public static int multiplicadorPuntosCaja = 0;

		public static int nivelActual;

		//Paddle
		public static float VelocidadPaddle = 6;
		public static float MaxVelocidadPaddle = 400;
		public static float SensibilidadPaddle = 9;

		//Customizing: Variables globales configuracion del juego
		//POWER UPS
		//Red Ball
		public static float PowerUpRedBallDuracion 	= 6f;
		public static float PowerUpRedBall 			= 2.5f;
		//Bajar Brea
		public static float PowerUpBajarBrea 		= 4.3f;
		//Multiple Balls
		public static float PowerUpMultipleBallProb = 2.5f;
		public static float PowerUpMultipleBallCant = 1;

		//Subir de nivel
		private static int golpesPorNivelMinimo = 10;
//		private static int golpesPorNivel = 36;
		private static bool flagBajarNivel = false;
		public static int cantRebotes;
		public static int contadorCajasDerretidas;

		//Variables gloables PlayerPrefs
		public static string s_Level = "TEST_Nivel";
		public static string s_Dificultad = "Dificultad_X";
		public static string s_sound = "Sound";
		public static string s_TouchPad = "SelectorTouchPad";
		public static string s_Rated = "Rated";
		public static string s_CantPartidas = "CantidadPartidas";
		public static string s_PowerUpRebBallProb 		= "PowerUpRedBallProb";
		public static string s_PowerUpRebBallDurac 		= "PowerUpRedBallDurac";
		public static string s_PowerUpMultipleBallCant 	= "PowerUpMultipleBallCant";
		public static string s_PowerUpBajarBreaProb 	= "PowerUpBajarBreaProb";
		public static string s_Language	= "Idioma";
		public static string s_UltimoInicioSesion	= "UltimoInicioSesion";
		public static string s_GiftCounter = "Gift_Counter";
		public static string s_HighScore	= "New_High_Score";
		public static string s_BolaElegida = "Bola_Elegida";


		public static string s_On = "On";
		public static string s_Off = "Off";

		public static Color[] coloresMultiplicador = new Color[10];
		private static int _siguienteColor = 0;
		public static int _sortingLayer = 6;

		public static bool gameOver = false;

		public static bool homeButton = false;

		public static void ReinicializarMultiplicadorPuntos(){
		
			multiplicadorPuntosCaja = 0;
			_siguienteColor = 0;
			_sortingLayer = 6;
		
		}

//		public static int BloquesPorNivel(){
//			int _CantBloques = 0;
//
//			switch (nivelActual) {
//			case 1:
//				_CantBloques = 2;
//				break;
//			case 2:
//				_CantBloques = 2;
//				break;
//			default:
//				_CantBloques = 3;
//				break;
//			}
//
//			return _CantBloques;
//
//		}
	

		public static void ReinicializarNivel(){

//			nivelActual = 0;
//
//			if (PlayerPrefs.HasKey(s_Dificultad))
//				dificultadActual = PlayerPrefs.GetInt (s_Dificultad);
//			else
//				dificultadActual = 1;
//			
//			//Aumento progresivo de factorDif% en cada Dificultad
//			velocidadCajasBase = velocidadCajasBase * (1 + factorDif * dificultadActual);

		}

//		public static void SubirNivel(){
//		
//			nivelActual++;
//
//			//Al terminar el nivel 5 se aumenta la Dificultad y vuelven a comenzar los niveles
//			if (nivelActual == 6)
//				determinarDificultad ();
//			
//			//Velocidad cajas
//			velocidadCajas = velocidadCajasBase * (1 + nivelActual * porcentajeAumentoNivel);
//			
//		}

		public static void addRebote ()
		{

			cantRebotes ++;

		}

        public static void AumentarVelocidadCajas()
        {

            if (velocidadCajas < 0.7f)
                velocidadCajas *= 1.05f;
            else
                velocidadCajas += aumentoVelocidadCajas;

        }

		public static void ResetearVelocidadCajas(){
		
			velocidadCajas = velocidadCajasBase;
		
		}

		public static void determinarDificultad(){

			if (PlayerPrefs.HasKey (s_Dificultad)){
				
				dificultadActual = PlayerPrefs.GetInt (s_Dificultad);

				//Evaluar si sube o baja de dificultad
				if (cantRebotes > 0) {
					if (cantRebotes >= golpesPorNivel ()) {
						//Subir Dificultad
						dificultadActual++;
						flagBajarNivel = false;
					}
				//Bajar Nivel 
				else if (cantRebotes <= golpesPorNivelMinimo) {
						if (flagBajarNivel) {
							//a la segunda vez seguida que no llega al minimo
							dificultadActual--;
							flagBajarNivel = false;
						} else
							flagBajarNivel = true;
					} else //Quedo en el mismo nivel
					flagBajarNivel = false;
				}

				if (dificultadActual < 1)
					dificultadActual = 1;

				if (dificultadActual > dificultadMaxima)
					dificultadActual = dificultadMaxima;

		}

			else //Primera vez al setear la Dificultad 
				dificultadActual = 3;

			//Guardar nueva Dificultad
			PlayerPrefs.SetInt (s_Dificultad, dificultadActual);

			//Aumentar la velocidad de la pelota a medida que se avanza de Dificultad
			velocidadPelota = velocidadPelotaBase + (dificultadActual * velocidadPelotaAumento);

			//Reiniciar cantidad de Rebotes para siguiente partida
			cantRebotes = 0;
				
		}

		public static int golpesPorNivel(){

			if (dificultadActual < 3)	//de 1 a 2 20 golpes para subir
				return 20;
			else if (dificultadActual >= 3 && dificultadActual < 5)	//de 3 a 5 36 golpes para subir
				return 36;
			else //Mayor o igual a 5 50 golpes para subir
				return 50;

		}

		//Devolvera un factor de dificultad para aplicar en el juego
//		public static int determinarNivel(bool calcular){
//		
//
////			//Verificar si el nuevo nivel supera el anterior
////			dificultadActual = PlayerPrefs.GetInt (s_Level);
////
////			//Subir o bajar el nivel segun corresponda
////			if (calcular) {
////
////				if (dificultadActual < 5)	//de 1 a 4 20 golpes para subir
////					golpesPorNivel = 20;
////				else if (dificultadActual >= 5 && dificultadActual < 8)	//de 5 a 7 36 golpes para subir
////					golpesPorNivel = 36;
////				else //Mayor a 7 50 golpes para subir
////					golpesPorNivel = 50;
////
////				//Subir Nivel
////				if (cantRebotes >= golpesPorNivel) {
////					dificultadActual++;
////					flagBajarNivel = false;
////				}
////				//Bajar Nivel 
////				else if (cantRebotes <= golpesPorNivelMinimo) {
////					if (flagBajarNivel == true) {
////						//a la segunda vez seguida que no llega al minimo
////						dificultadActual--;
////						flagBajarNivel = false;
////					} else
////						flagBajarNivel = true;
////				}
////				else //Quedo en el mismo nivel
////					flagBajarNivel = false;
////				
////
////				if (dificultadActual < 1)
////					dificultadActual = 1;
////
////				if (dificultadActual > 10)
////					dificultadActual = 10;
////			}
////
////			//Guardar nuevo nivel
////			PlayerPrefs.SetInt (s_Level, dificultadActual);
////
////			//Reiniciar cantidad de Rebotes para siguiente partida
////			cantRebotes = 0;
////
////			//Se parte de la premisa que el primer nivel es 5. por eso el factor debe ser 1 para Nivel = 5
//////			velocidadCajas = velocidadCajasBase   * (factorDif * dificultadActual + 0.85f);
//////			velocidadPelota = velocidadPelotaBase * (factorDif * dificultadActual + 0.85f);
////
////			return dificultadActual;
////
//		}

		public static void llenarColoresMultiplicador(){

			_siguienteColor = -1;

			coloresMultiplicador [0]  = new Color (137f / 255f, 165f / 255f, 253f / 255f);
			coloresMultiplicador [1]  = new Color (130f / 255f, 183f / 255f, 248f / 255f);
			coloresMultiplicador [2]  = new Color (124f / 255f, 201f / 255f, 243f / 255f);
			coloresMultiplicador [3]  = new Color (117f / 255f, 219f / 255f, 238f / 255f);
			coloresMultiplicador [4]  = new Color (111f / 255f, 237f / 255f, 233f / 255f);
			coloresMultiplicador [5]  = new Color (104f / 255f, 255f / 255f, 228f / 255f);
			coloresMultiplicador [6]  = new Color (154f / 255f, 237f / 255f, 168f / 255f);
			coloresMultiplicador [7]  = new Color (236f / 255f, 233f / 255f, 151f / 255f);
			coloresMultiplicador [8]  = new Color (235f / 255f, 139f / 255f, 123f / 255f);
			coloresMultiplicador [9]  = new Color (255f / 255f, 104f / 255f, 131f / 255f);
	
		}

		public static Color seleccionarSiguienteColor(){

			int _siguienteColorAux;

			if (_siguienteColor >= coloresMultiplicador.Length) {
				_siguienteColor = coloresMultiplicador.Length - 1;
			}

			//Paso una variable auxiliar para poder incrementar antes del return
			_siguienteColorAux = _siguienteColor;
			_siguienteColor++;

			return coloresMultiplicador [_siguienteColorAux];

		}
	}

		
}

