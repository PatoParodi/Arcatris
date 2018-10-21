using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour {


//	private InterstitialAd video;
//	private RewardBasedVideoAd rewardVideo;
//
//	public static AdMobManager Instance {
//		get;
//		private set;
//	}
//
//	void Awake(){
//		//First we check if there are any other instances conflicting
//		if (Instance != null && Instance != this)
//		{
//			//Destroy other instances if they are not the same
//			Destroy(gameObject);
//		}
//		//Save our current singleton instance
//		Instance = this;
//		//Make sure that the instance is not destroyed
//		//between scenes (this is optional)
//		DontDestroyOnLoad(gameObject);
//
//		rewardVideo = RewardBasedVideoAd.Instance;
//
//	}
//
//
//	public void ShowVideo(){
//	
////		video = new InterstitialAd ("ca-app-pub-3940256099942544/1033173712");
////
////		video.LoadAd (new AdRequest.Builder ().Build ());
////	
////		if (video.IsLoaded()) {
////			video.Show();
////		} else {
////			Debug.Log("The interstitial wasn't loaded yet.");
////		}
//
//
//		LoadRewardBaseAd ();
//
//
//		ShowRewardBaseAd ();
//
//	}
//
//	private void ShowRewardBaseAd(){
//
//		if(rewardVideo.IsLoaded()){
//			Debug.Log ("Mostrar ad");
//			rewardVideo.Show();
//		} else{
//			Debug.Log("not loaded yet");
//		}
//
//	}
//
//	private void LoadRewardBaseAd(){
//	
//		//		#if UNITY_ANDROID
//		string adUnitId = "ca-app-pub-2321113512856314/6042377676";
//		//		#elif UNITY_IPHONE
//		//		string adUnitId = "ca-app-pub-3940256099942544/1712485313";
//		//		#else
//		//		string adUnitId = "unexpected_platform";
//		//		#endif
//
//		// Create an empty ad request.
//		AdRequest request = new AdRequest.Builder().Build();
//
//		// Load the rewarded video ad with the request.
//		rewardVideo.LoadAd(request, adUnitId);
//
//	
//	}
//
//	public void InitializeAds(){
//	
//		//		#if UNITY_ANDROID
//		string appId = "ca-app-pub-3940256099942544~3347511713";
//		//		#elif UNITY_IPHONE
//		//		string appId = "ca-app-pub-3940256099942544~1458002511";
//		//		#else
//		//		string appId = "unexpected_platform";
//		//		#endif
//
//
//		// Initialize the Google Mobile Ads SDK.
//		MobileAds.Initialize(appId);
//	
//	}

}
