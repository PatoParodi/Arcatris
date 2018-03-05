using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour {


	void Update(){
	
		gameObject.GetComponent<Rigidbody2D> ().velocity = 4.4f * (gameObject.GetComponent<Rigidbody2D> ().velocity.normalized);

	}


	void OnCollisionEnter2D(Collision2D col){

		// Evitar que al chocar con poco angulo contra algun marco la pelota quede pegada a la pared
		if (col.gameObject.tag == "Marco" &&
		    col.contacts [0].point.y < 4.25f) {
			//Forzar vector de velocidad a magnitud fija
			gameObject.GetComponent<Rigidbody2D> ().velocity = 4.4f * (gameObject.GetComponent<Rigidbody2D> ().velocity.normalized);

			// RelativeVelocity POSITIVA
			if (col.relativeVelocity.x < 1 && col.relativeVelocity.x > 0)
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1.1f, GetComponent<Rigidbody2D> ().velocity.y));
		
			// RelativeVelocity NEGATIVA
			else if (col.relativeVelocity.x > -1 && col.relativeVelocity.x < 0)
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-1.1f, GetComponent<Rigidbody2D> ().velocity.y));
			
			// RelativeVelocity POSITIVA
			if (col.relativeVelocity.y < 1 && col.relativeVelocity.y >= 0){
//				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, -1.1f);
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, (-col.relativeVelocity.y) - (1 - col.relativeVelocity.y)/2);

		}
			// RelativeVelocity NEGATIVA
			else if (col.relativeVelocity.y > -1 && col.relativeVelocity.y < 0){
//				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, 1.1f);
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, (-col.relativeVelocity.y) + (1 + col.relativeVelocity.y)/2);

		}
				
		}
	
		//Forzar vector de velocidad a magnitud fija
//		if (col.gameObject.tag == "Caja" || col.gameObject.tag == "Marco") { 				

			gameObject.GetComponent<Rigidbody2D> ().velocity = 4.4f * (gameObject.GetComponent<Rigidbody2D> ().velocity.normalized);

//		}
			
	}

}
