using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

	public static PowerUpManager Instance {
		get;
		private set;
	}

	public PowerUp RedBall;
	public PowerUp MultipleBall;
	public PowerUp BajarBrea;


	void Awake(){
		//First we check if there are any other instances conflicting
		if (Instance != null && Instance != this)
		{
			//Destroy other instances if they are not the same
			Destroy(gameObject);
		}
		//Save our current singleton instance
		Instance = this;
		//Make sure that the instance is not destroyed
		//between scenes (this is optional)
		DontDestroyOnLoad(gameObject);
	}


	public void LeerNiveles(){

		RedBall = new PowerUp("RedBall");
		MultipleBall = new PowerUp ("MultipleBall");
		BajarBrea = new PowerUp ("BajarBrea");

		RedBall.LeerNiveles ();
		MultipleBall.LeerNiveles ();
		BajarBrea.LeerNiveles ();
			
	
	}

	void CalcularPropiedades(){
	
//		Instance.RedBall.CalcularPropiedades (1);
//
//		Instance.MultipleBall.CalcularPropiedades (1);
//
//		Instance.MultipleBall.CalcularPropiedades (1);
	
	}

//	public void SubirNivel

}
