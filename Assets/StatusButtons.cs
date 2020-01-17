using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusButtons : MonoBehaviour {

    public Button btnBall4Video;

    public void AdIsLoaded() {

        if (!btnBall4Video.GetComponent<ButtonExt>().isCounting)
        //If ad is not loaded, deactivate button
            btnBall4Video.interactable = AdsManager.Instance.InterstitialIsLoaded();

    }

}
