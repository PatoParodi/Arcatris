using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScrollTouch : MonoBehaviour {

	//Public Variables
	public RectTransform panel; // To hold the ScrollPanel
	public RectTransform panelFather;  // To hold the Parent Panel 
	public Button[] bttn;
	public RectTransform center; // Center to compare the distance for each button
	public float lerpVelocityToCenter; //Time for button to get to center

	//Private Variables
	private float[] distance; //All buttons' distance to the center
	private bool dragging = false; // Will be true while we drag the panel
	private bool draggable = true; // Flag for interaction with the scroll
	private int bttnDistance; //Will hold the distance between the buttons
	public int minButtonNum; //Number of the button with smalllest distance to the center


	void Start(){

		int bttnLength = bttn.Length;

		distance = new float[bttnLength];

		// Get distance between buttons
		bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);  

	}

	void Update(){

		for (int i = 0; i < bttn.Length; i++) {
		
			distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].transform.position.x);

//			if (!i.Equals (minButtonNum)) {
//				bttn [i].GetComponent<Button> ().interactable = false;
//			}
		
		}

	//Enable/Disable dragging if start or end buttons are gone too far
		if (minButtonNum == 0 || minButtonNum == (bttn.Length - 1)) {
			if (distance [minButtonNum] > 1.5f) {
				draggable = false;
//				LerpToBttn (minButtonNum * -bttnDistance);	
			}
			else {
				draggable = true;
			}

			} else{ 
				draggable = true;

		}
			
		draggable = true;
		//Enable/Disable dragging if start or end buttons are gone too far
//		panelFather.GetComponent<ScrollRect> ().horizontal = draggable;
//		panelFather.GetComponent<ScrollRect> ().inertia = draggable;

		float minDistance = Mathf.Min (distance); // Get the min distance

		for (int a = 0; a < bttn.Length; a++) {
		
			if (minDistance == distance [a]) {
			
				minButtonNum = a;
			}
		}

		if (!dragging) {

			draggable = true;
//			LerpToBttn (minButtonNum * -bttnDistance);
//			bttn [minButtonNum].GetComponent<Button> ().interactable = true;

		}
	
	}

	void LerpToBttn(int position){
	
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * lerpVelocityToCenter);
		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);

		panel.anchoredPosition = newPosition;
	}

	public void StartDrag(){
	
		dragging = true;
	}

	public void EndDrag(){
	
		dragging = false;
		draggable = false;
	}
}
