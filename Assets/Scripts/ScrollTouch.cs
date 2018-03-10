using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollTouch : MonoBehaviour {

	//Public Variables
	public RectTransform panel; // To hold the ScrollPanel
	public Button[] bttn;
	public RectTransform center; // Center to compare the distance for each button
	public float lerpVelocityToCenter; //Time for button to get to center

	//Private Variables
	private float[] distance; //All buttons' distance to the center
	private bool dragging = false; // Will be true while we drag the panel
	private int bttnDistance; //Will hold the distance between the buttons
	private int minButtonNum; //Number of the button with smalllest distance to the center


	void Start(){

		int bttnLength = bttn.Length;

		distance = new float[bttnLength];

		// Get distance between buttons
		bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);  

	}

	void Update(){
	
		for (int i = 0; i < bttn.Length; i++) {
		
			distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].transform.position.x);
		
		}

		float minDistance = Mathf.Min (distance); // Get the min distance

		for (int a = 0; a < bttn.Length; a++) {
		
			if (minDistance == distance [a]) {
			
				minButtonNum = a;
			}
		}

		if (!dragging) {
		
			LerpToBttn (minButtonNum * -bttnDistance);	
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
	}
}
