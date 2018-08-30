using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraBallCompra : MonoBehaviour {

	public float _speed;

	private Transform _objExtraBalls;
	private GameController controller;

	void Awake(){
	
		controller =  GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		transform.localPosition = new Vector2(transform.localPosition.x + Random.Range(-80,80), transform.localPosition.y);
	}

	// Use this for initialization
	void Start () {

		_objExtraBalls = GameObject.FindGameObjectWithTag ("txtExtraBalls").GetComponent<Transform> ();

		gameObject.transform.SetParent (_objExtraBalls);

	}
	
	// Update is called once per frame
	void Update () {

		float _step = _speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, _objExtraBalls.position, _step);

		if (Vector2.Distance (transform.position, _objExtraBalls.position) < 0.1f) {

			Destroy (gameObject);
			//Agregar Extraball al contador
			controller.addExtraBallToScreen(1);

		}

	}
}
