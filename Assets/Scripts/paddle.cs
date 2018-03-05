using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddle : MonoBehaviour {

	private GameController controller;


	void Awake(){
	
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		transform.position = new Vector2 (0, GameObject.Find("NivelDeFlotacion").transform.position.y);

	}

	void Update(){


	}
		

	void OnCollisionEnter2D(Collision2D col){

		foreach (ContactPoint2D contact in col.contacts) {

			if (col.gameObject.tag == "ball" ) {

				col.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

				// Calculo la diferencia entre el centro del pad y el punto x de colision
				float calc = contact.point.x - transform.position.x;

				// Calculo el porcentaje para usar como angulo de salida
				float porc = calc * 100 / GetComponent<BoxCollider2D> ().bounds.size.x;

				// Parto de los 90 grados como mi 0
				porc = 90 - porc;

				contact.rigidbody.AddForce(controller.obtenerVectorVelocidad (controller.fuerzaPelota, porc, porc));
				
			}
		}}


}
