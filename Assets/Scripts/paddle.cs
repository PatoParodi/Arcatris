using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddle : MonoBehaviour {

	private GameController controller;

	private GameObject waterLevel;
//	public Transform waterLevel;
	public float floatHeight;
	public float bounceDamp;
	public Vector3 buoyancyCenterOffset;
	public Vector2 forceUp;

	private float forceFactor;
	private Vector3 actionPoint;
	private Vector3 upLift;



	void Awake(){
	
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void Update(){

		waterLevel = GameObject.Find ("NivelDeFlotacion");

//		GetComponent<Rigidbody2D> ().AddForceAtPosition (forceUp, waterLevel.transform.position);

		actionPoint = transform.position + transform.TransformDirection(buoyancyCenterOffset);
		forceFactor = 1f - ((actionPoint.y - waterLevel.transform.position.y) / floatHeight);

		if(forceFactor > 0)
		{
			upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody2D>().velocity.y * bounceDamp);
			GetComponent<Rigidbody2D>().AddForceAtPosition(upLift, actionPoint);
		}
			
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
