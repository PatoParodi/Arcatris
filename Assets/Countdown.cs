using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    [Tooltip("Time in seconds between Reward Ads")]
    [SerializeField] float time = 10;

    [SerializeField] Button btnBall4Video;
    [SerializeField] Text countdownText;

    float currCountdownValue;
    private IEnumerator StartCountdown(float countdownValue)
    {
        int minutes = 0, seconds = 0;

        btnBall4Video.interactable = false;
        //Show Text
        countdownText.color = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, 1f);
        btnBall4Video.GetComponent<ButtonExt>().isCounting = true;

        currCountdownValue = countdownValue;
        while (currCountdownValue >= 0)
        {
        
            minutes = (int)(currCountdownValue / 60);
            seconds = (int)(currCountdownValue % 60);

            if(seconds >= 10)
                countdownText.text = minutes.ToString() + ":" + seconds.ToString();
            else 
                countdownText.text = minutes.ToString() + ":0" + seconds.ToString();

            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        btnBall4Video.interactable = true;
        //Hide Text
        countdownText.color = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, 0f);
        btnBall4Video.GetComponent<ButtonExt>().isCounting = false;

    }


    //Turn the button OFF until time is over, then turn ON
    public void StartCountdown()
    {
        //Start counting
        StartCoroutine(StartCountdown(time));

    }

}
