using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SoundManager
{
	public static class soundManager {

		private static bool soundEnabled = true;

		public static void playSound (AudioSource audio)
		{

			if (soundEnabled)
				audio.Play ();
		}

		public static void enableSound(bool _enabled){

			soundEnabled = _enabled;

		}
	}
}

