using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.IO;
using Language;

public class ScriptHighScore : MonoBehaviour {

	private GameController controller;
	public Text highScore;

	// Use this for initialization
	void Start () {

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		highScore.text = controller.getScore ().ToString();

		if(controller.getScore () > 100000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA100.000", 1}
			});
		else if(controller.getScore () > 50000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA50.000", 1}
			});
		else if(controller.getScore () > 25000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA25.000", 1}
			});
		else if(controller.getScore () > 10000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA10.000", 1}
			});
		else if(controller.getScore () > 5000)
			//Metricas - Analytics - Checkpoints de HighScore
			Analytics.CustomEvent ("HighScore", new Dictionary<string, object> {
				{ "MayorA5.000", 1}
			});

	}
		

	void OnEnable(){
		
		SoundManager.soundManager.playSound (GetComponent<AudioSource> ());

	}



	// Update is called once per frame
	void Update () {
		
		//Recuperar High Score del Game Controller
		highScore.text = controller.getScore ().ToString();

	}

//	public void ShareAndroid(){
//
//		// Create Refernece of AndroidJavaClass class for intent
//		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
//		// Create Refernece of AndroidJavaObject class intent
//		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
//
//		// Set action for intent
//		intentObject.Call("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
//
//		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
//
//		//Set Subject of action
//		intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Text Sharing ");
//		//Set title of action or intent
//		intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
//		// Set actual data which you want to share
//		intentObject.Call("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Text Sharing Android Demo");
//
//		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
//		// Invoke android activity for passing intent to share data
//		currentActivity.Call("startActivity", intentObject);
//
//
//	}


//	public GameObject CanvasShareObj;

//	private bool isProcessing = false;
//	private bool isFocus = false;
//
//	public void ShareBtnPress()
//	{
//		if (!isProcessing)
//		{
////			CanvasShareObj.SetActive(true);
//			StartCoroutine(ShareScreenshot());
//		}
//	}
//
//	IEnumerator ShareScreenshot()
//	{
//		isProcessing = true;
//
//		yield return new WaitForEndOfFrame();
//
//		ScreenCapture.CaptureScreenshot ("screenshot.png", 2);
//		string destination = Path.Combine(Application.persistentDataPath, "screenshot.png");
//
//		yield return new WaitForSecondsRealtime(0.3f);
//
//		if (!Application.isEditor)
//		{
//			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
//			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
//			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
//			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
//			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
//			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
//				uriObject);
//			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
//				"Can you beat my score?");
//			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
//			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
//			AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
//				intentObject, "Share your new score");
//			currentActivity.Call("startActivity", chooser);
//
//			yield return new WaitForSecondsRealtime(1);
//		}
//
//		yield return new WaitUntil(() => isFocus);
////		CanvasShareObj.SetActive(false);
//		isProcessing = false;
//	}
//
//	private void OnApplicationFocus(bool focus)
//	{
//		isFocus = focus;
//	}


	string subject = "Arcatrix"; //Asunto del mail
	string justMadeText;
	string pointsText;
	string body; //Se pasa al mensaje en Whatsapp / Instagram por mensaje / Cuerpo del mail

		private bool isProcessing = false;
		private bool isFocus = false;


	public void shareText()
	{
		
		justMadeText = LanguageManager.textos [48];
		pointsText = LanguageManager.textos [49];
		body = LanguageManager.textos [50]; //Se pasa al mensaje en Whatsapp / Instagram por mensaje / Cuerpo del mail


		if (!isProcessing)
		{
			//			CanvasShareObj.SetActive(true);
			StartCoroutine(ShareScreenshot());
		}
	}

	IEnumerator ShareScreenshot()
	{
		isProcessing = true;

//		yield return new WaitForEndOfFrame();

		ScreenCapture.CaptureScreenshot ("screenshot.png", 2);
		string destination = Path.Combine(Application.persistentDataPath, "screenshot.png");

		yield return new WaitForSecondsRealtime(0.5f);

		
		//execute the below lines if being run on a Android device
//		#if UNITY_ANDROID

		//Reference of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		//Reference of AndroidJavaObject class for intent
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		//call setAction method of the Intent object created
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

		//set the type of sharing that is happening
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
//		intentObject.Call<AndroidJavaObject>("setType", "image/png");
//		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
//		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
//		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

		//add data to be passed to the other activity i.e., the data to be sent
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), justMadeText + controller.getScore().ToString() + pointsText + body);

		//get the current activity
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

		//start the activity by sending the intent data
		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
		currentActivity.Call("startActivity", jChooser);

		isProcessing = false;

//		#endif

	}


}
