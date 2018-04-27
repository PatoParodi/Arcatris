using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamanteSpawn : MonoBehaviour {

	public float _speed;

	private Transform _diamanteTarget;
	private GameController _controller;
	private bool _destruido;


	// Use this for initialization
	void Start () {

		_diamanteTarget = GameObject.FindGameObjectWithTag ("InGametxtMonedas").GetComponent<Transform> ();

		_controller =  GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		SoundManager.soundManager.playSound (GetComponent<AudioSource> ());

	}
	
	// Update is called once per frame
	void Update () {

		float _step = _speed * Time.deltaTime;

		transform.position = Vector2.MoveTowards (transform.position, _diamanteTarget.position, _step);

		if (!_destruido) {
			if (Vector2.Distance (transform.position, _diamanteTarget.position) < 0.1f) {

				GetComponent<Animator> ().SetTrigger ("Morir");
				if (!GetComponent<AudioSource> ().isPlaying) {
					Destroy (gameObject, 0.3f);
					_destruido = true;
					//Agregar Moneda al contador
					_controller.AddMoneda (1);
				}

			}
		}

	}
}
