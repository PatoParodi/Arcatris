using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpinManager : MonoBehaviour {

	public float spin = 0;

	public bool HasGlow = false;
	public GameObject Glow;
	public bool HasLights = false;
	public GameObject Light_1;
	public GameObject Light_2;


	void Awake(){

	}

	public void StartSpinning(){
	
		GetComponent<Animator> ().SetTrigger ("Start");

		//Dar valor inicial al giro (random entre 0 y 1)
		spin = Random.Range(-2f,2f) + Random.value;
	
		}

	// Update is called once per frame
	void Update () {

		GetComponent<Animator> ().SetFloat ("Spin", spin);

	}

	public void ActivarBolaRoja(){

		//Apagar el glow y luces durante PowerUp
		if (HasGlow)
			Glow.SetActive (false);

		//Apagar el glow durante PowerUp
		if (HasLights) {
			Light_1.SetActive (false);
			Light_2.SetActive (false);
		}
		

		GetComponent<Animator>().SetBool ("Red Ball", true);
	}

	public void DesactivarBolaRoja(){

		//Re-Activar el glow durante PowerUp
		if (HasGlow)
			Glow.SetActive (true);
		
		GetComponent<Animator>().SetBool ("Red Ball", false);
	}

}
