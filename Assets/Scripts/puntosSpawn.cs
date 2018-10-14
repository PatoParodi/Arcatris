﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntosSpawn : MonoBehaviour {


	public float _speed;
	public int _puntos;

	private Transform _diamanteTarget;
	private GameController _controller;
	private bool _destruido;


	// Use this for initialization
	void Start () {

		_diamanteTarget = GameObject.FindGameObjectWithTag ("InGametxtPuntos").GetComponent<Transform> ();

		_controller =  GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

	}

	// Update is called once per frame
	void Update () {

		float _step = _speed * Time.deltaTime;

		transform.position = Vector2.MoveTowards (transform.position, _diamanteTarget.position, _step);

		//Al ir al Home desde el juego
		if (LevelManager.levelManager.homeButton) {
			_destruido = true;
			Destroy (gameObject);
		}

		if (!_destruido) {
			if (Vector2.Distance (transform.position, _diamanteTarget.position) < 0.1f) {

				Destroy (gameObject, 0.6f);
				_destruido = true;
				//Agregar Puntos al contador
				_controller.AddScore (_puntos);

			}
		}

	}
}
