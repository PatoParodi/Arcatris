using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class ScriptHighScore : MonoBehaviour {

	private GameController controller;
	public Text highScore;

	// Use this for initialization
	void Start () {

		SoundManager.soundManager.playSound (GetComponent<AudioSource> ());

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		highScore.text = controller.getHighScore ().ToString();

		if(controller.getHighScore () > 100000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA100.000", 1}
			});
		else if(controller.getHighScore () > 50000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA50.000", 1}
			});
		else if(controller.getHighScore () > 25000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA25.000", 1}
			});
		else if(controller.getHighScore () > 10000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA10.000", 1}
			});
		else if(controller.getHighScore () > 5000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA5.000", 1}
			});
	}
	
	// Update is called once per frame
	void Update () {
		
		//Recuperar High Score del Game Controller
//		highScore.text = controller.getHighScore ().ToString();

	}

	public void ShareAndroid(){

		// Create Refernece of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		// Create Refernece of AndroidJavaObject class intent
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

		// Set action for intent
		intentObject.Call("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

		intentObject.Call<AndroidJavaObject>("setType", "text/plain");

		//Set Subject of action
		intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Text Sharing ");
		//Set title of action or intent
		intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
		// Set actual data which you want to share
		intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Text Sharing Android Demo");

		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		// Invoke android activity for passing intent to share data
		currentActivity.Call("startActivity", intentObject);


	}
}
