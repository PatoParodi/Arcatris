using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateUsManager : MonoBehaviour {

    [SerializeField] Toggle[] estrellas;
    [SerializeField] GameObject btnLater;
    [SerializeField] GameObject btnContinue;
    [SerializeField] GameObject txtThankYou;
    [SerializeField] float CloseInSegs;
    [SerializeField] PantallaInicial pant;
    [SerializeField] MenuController menu;

    private void OnEnable()
    {

        foreach (Toggle estrella in estrellas)
        {
            estrella.isOn = false;
        }

        //Show buttons and hide Thank You text
        ToggleObjects(true);

    }

    public void ClickContinue(){

        int rated = 0;

        foreach(Toggle estrella in estrellas){
            if(estrella.isOn)
                rated++;
        }

        //If user selects 4 or 5 stars
        if (rated > 3)
            GoToMarket(); //Show market to user to rate

        //Say thank you and close pop up
        ToggleObjects(false);
        StartCoroutine(ClosePopUp(CloseInSegs));

        //Analytics
        AnalyticsManager.Instance.RateUsPopUp(rated);

    }

    private void ToggleObjects(bool show){

            btnLater.SetActive(show);
            btnContinue.SetActive(show);
            txtThankYou.SetActive(!show);
  
    }

    public IEnumerator ClosePopUp(float seconds){

        yield return new WaitForSecondsRealtime(seconds);

        HidePopUpAndContinue();

    }

    public void HidePopUpAndContinue(){

        gameObject.SetActive(false);
        menu.MostrarPlay(true);
        pant.ActivarBotones();

    }

    private void GoToMarket()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.PardeSotas.Arcatris");

        PlayerPrefs.SetString(LevelManager.levelManager.s_Rated, "Si");

    }

}
