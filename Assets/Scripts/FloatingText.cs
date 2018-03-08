using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

	public Animator animator;

	private Text priceText;

	// Use this for initialization
	void Start () {

//Obtener info de la animacion del objeto
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
//Destruir el objeto despues de la duracion de la animacion
		Destroy(gameObject,clipInfo[0].clip.length);
//Actualizar el componente texto con el precio del prod comprado
		priceText = animator.GetComponent<Text> ();
			
		
	}
	
	// Update is called once per frame
	public void setText (string precio) {

//		priceText.text = precio;
		animator.GetComponent<Text> ().text = precio;



	}
}
