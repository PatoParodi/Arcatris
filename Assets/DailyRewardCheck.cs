using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardCheck : MonoBehaviour {

	public int NumeroDeGift;

	void OnEnable(){

		Image im = GetComponent<Image> ();
		Text texto = GetComponentInChildren<Text> ();

		if (NumeroDeGift <= DailyRewardManager.Instance.GiftCounter) {

			im.color = new Color (im.color.r, im.color.g, im.color.b, 1f);
			texto.color = new Color (texto.color.r, texto.color.g, texto.color.b, 1f);

		} else {
		
			im.color = new Color (im.color.r, im.color.g, im.color.b, 0.5f);
			texto.color = new Color (texto.color.r, texto.color.g, texto.color.b, 0.5f);
		
		}

	}



}
