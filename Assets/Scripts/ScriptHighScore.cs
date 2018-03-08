using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptHighScore : MonoBehaviour {

	private GameController controller;
	public Text highScore;

	// Use this for initialization
	void Start () {

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		highScore.text = controller.getHighScore ().ToString();

	}
	
	// Update is called once per frame
	void Update () {
		
		//Recuperar High Score del Game Controller
		highScore.text = controller.getHighScore ().ToString();

	}
}
