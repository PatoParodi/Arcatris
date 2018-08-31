using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardCheck : MonoBehaviour {

	public int NumeroDeGift;

	void OnEnable(){

		Image[] images = GetComponentsInChildren<Image> ();
		Text[] textos = GetComponentsInChildren<Text> ();

		if (NumeroDeGift <= DailyRewardManager.Instance.GiftCounter) {

			foreach (Image image in images) {
				image.color = new Color (image.color.r, image.color.g, image.color.b, 1f);
			}
			foreach (Text texto in textos) {
				texto.color = new Color (texto.color.r, texto.color.g, texto.color.b, 1f);
			}

		} else {
		
			foreach (Image image in images) {
				image.color = new Color (image.color.r, image.color.g, image.color.b, 0.5f);
			}	
			foreach (Text texto in textos) {
				texto.color = new Color (texto.color.r, texto.color.g, texto.color.b, 0.5f);
			}

		}

	}



}
