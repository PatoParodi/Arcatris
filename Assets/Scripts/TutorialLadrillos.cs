using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLadrillos : MonoBehaviour {

	Vector2 PosicionInicial;
	GameController controller;
	bool flag = false;

	void Start(){
	
		controller = GameObject.Find ("GameController").GetComponent<GameController>();

		PosicionInicial = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

		//Validar que solo se ejecute la primera vez
		if (!flag) {
			if (!controller.ballInPlay) {
				transform.position = Vector2.Lerp (transform.position, new Vector2 (PosicionInicial.x, PosicionInicial.y - 1.2f), 0.1f);
			}
		}

		if (controller.ballInPlay)
			flag = true;
	}
}
