using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraBallCompra : MonoBehaviour {

	public float _speed;

	private Transform _objExtraBalls;

	// Use this for initialization
	void Start () {

		_objExtraBalls = GameObject.FindGameObjectWithTag ("txtExtraBalls").GetComponent<Transform> ();

		gameObject.transform.SetParent (_objExtraBalls);

	}
	
	// Update is called once per frame
	void Update () {

			float _step = _speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, _objExtraBalls.position, _step);

		if (transform.position == _objExtraBalls.position)
			Destroy (gameObject);

	}
}
