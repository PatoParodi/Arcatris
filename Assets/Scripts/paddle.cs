using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddle : MonoBehaviour {

	public float factorFlotacion;

	private GameController controller;
	private float move;

	void Awake(){
	
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	
		move = 0.5f;
		
	}
		
	void Update(){
//			StartCoroutine (reiniciarFlotacion (GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length));
//			paddleMoving = false;

			//Volver el animador Blend a 0
//			Mathf.MoveTowards(move,0.5f,0.01f);
//			GetComponent<Animator> ().SetFloat ("Move", move);
//
//			if (move > 0.5f)
//				move -= factorFlotacion;
//			if(move < 0.5f)
//				move += factorFlotacion;
//			move += 0.5f/(1 + factorFlotacion);
//			Mathf.MoveTowards(move,0.5f,0.01f);
			
	}


	void OnCollisionEnter2D(Collision2D col){

		ContactPoint2D contact = col.contacts [0];

//		foreach (ContactPoint2D contact in col.contacts) {

			if (col.gameObject.tag == "ball" ) {

				col.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

				// Calculo la diferencia entre el centro del pad y el punto x de colision
				float calc = contact.point.x - transform.position.x;

				// Calculo el porcentaje para usar como angulo de salida
				float porc = calc * 100 / GetComponent<BoxCollider2D> ().bounds.size.x;

				//Verifico de que lado reboto para la animacion correspondiente
				move = (porc/100) + 0.5f;
				GetComponent<Animator> ().SetFloat ("Move", move);
				GetComponent<Animator> ().SetBool ("Impacto", true);
				StartCoroutine(reiniciarFlotacion(1f));

				// Parto de los 90 grados como mi 0
				porc = 90 - porc;

				contact.rigidbody.AddForce(controller.obtenerVectorVelocidad (controller.fuerzaPelota, porc, porc));
				
//			}
		}
	}

	public IEnumerator reiniciarFlotacion(float duracion){

		//Esperar que termine la animacion actual
		yield return new WaitForSeconds (duracion);

		//Volver el animador Blend a 0
		GetComponent<Animator> ().SetBool ("Impacto", false);


	}


}
