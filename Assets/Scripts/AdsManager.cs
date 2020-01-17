using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour {

    public static AdsManager Instance
    {
        get;
        private set;
    }


    [SerializeField] int TimeBetweenAds;
    [SerializeField] GameObject contador;

    GameController controller;
    bool flagShowAd = false;
    Transform SpawnPos;
    AudioSource ExtraBallSound;
    bool fromShop;
    bool rewarded = false;

    void Awake()
    {
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

    public void Start()
    {
        //First Request for Interstatial
        //RequestVideo();

        RequestRewarded();

        //StartCountdown to show ad every TimeBetweenAds seconds
        //StartCountdown();

        //Get GameController script
        controller = GameObject.Find("GameController").GetComponent<GameController>();

    }

    private void Update()
    {
        if(rewarded == true){

            if (fromShop)
            {
                //Add ExtraBall, called from Shop
                realizarCompraExtraBall(0, 1);

            }
            else
            {
                //Continuar con bola extra
                controller.extraBalls += 1;
                controller.utilizarExtraBall();

                contador.GetComponent<scriptContador>().ClosePopUp();
            }

            rewarded = false;
        }
    }

    public void StartCountdown(){
        //Start countdown 
        //StopAllCoroutines();
        StopCoroutine("StartCounting");
        StartCoroutine(StartCounting(TimeBetweenAds));

    }

    public void CheckToShowAd(){

        if(flagShowAd){

            ShowAd();
        }

    }

    public void ShowBanner(){

        //GetComponent<GoogleMobileAdsManager>().RequestBanner();

    }

    public void DestroyBanner(){

        //GetComponent<GoogleMobileAdsManager>().DestroyBanner();

    }

    public void BannerIsLoaded(){

        controller.BannerIsLoaded = true;

    }

    public void ShowRewardedVideo(bool shop, Transform pos, AudioSource sound){
    
        SpawnPos = pos;
        ExtraBallSound = sound;
        fromShop = shop;

        //RequestRewarded();

        GetComponent<GoogleMobileAdsManager>().ShowRewardBasedVideo();

        RequestRewarded();

    }

    public void RequestRewarded(){

        //Request Rewarded Video
        GetComponent<GoogleMobileAdsManager>().RequestRewardBasedVideo();

    }

    public bool IsRewardedLoaded(){

        return GetComponent<GoogleMobileAdsManager>().IsRewardedLoaded();

    }

    public void RewardUser(){

        Time.timeScale = 1;

        rewarded = true;

    }

    public void RewardedVideoClosed()
    {

        //If reward video was closed, go to Initial screen without rewarding user
        Time.timeScale = 1;

    }

    void realizarCompraExtraBall(int precio, int cantidad)
    {

        // Hace la compra si tiene suficientes diamantes, caso contrario devuelve false
        bool _compraHecha = controller.Comprar(precio);

        if (_compraHecha)
        {

            //Alimentar analytics
            //Analytics.CustomEvent("ComprasExtraBall", new Dictionary<string, object> {
            //    { "Cantidad", cantidad },
            //    { "Precio", precio }
            //});

            //Reproducir sonido
            SoundManager.soundManager.playSound(ExtraBallSound);

            //Instanciar pelota comprada, que volara hasta el contador
            GameObject _extraBall = Resources.Load("Prefabs/extraBallCompra") as GameObject;

            Instantiate(_extraBall, SpawnPos);
            //StartCoroutine(_instanciarBola(_extraBall, cantidad, 0.1f));

        } 

    }

    public void ShowAd(){

        //Show Ad
        ShowInterstatialVideo();

        //Set flag to false
        flagShowAd = false;

        //Request new ad
        RequestVideo();

        //Start counting again
        StartCountdown();

    }

    private IEnumerator StartCounting(int seconds){
        //Wait for time between ads
        yield return new WaitForSecondsRealtime(seconds);

        //Set flag to show ad
        flagShowAd = true;

    }

    public void RequestVideo(){
    
        //Request video Ad
        GetComponent<GoogleMobileAdsManager>().RequestInterstitial();
        
    }

    public bool InterstitialIsLoaded() {

        return GetComponent<GoogleMobileAdsManager>().InterstitialIsLoaded();
    }

    public void ShowInterstatialVideo(){
    
        //Show video ad
        GetComponent<GoogleMobileAdsManager>().ShowInterstitial();

    }
}
