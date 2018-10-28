using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpLevels : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
		
        GetComponent<Text>().text = "RedBall: " + PowerUpManager.Instance.RedBall.Frecuencia + "\nMB: " + PowerUpManager.Instance.MultipleBall.Frecuencia + "\nBajarBrea: " + PowerUpManager.Instance.BajarBrea.Frecuencia;

	}
}
