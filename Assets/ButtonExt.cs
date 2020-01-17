using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonExt : MonoBehaviour {

    public bool isCounting = false;

    public void ButtonToNormal() {

        //Only if button is active
        if(GetComponent<Button>().interactable)
            GetComponent<Animator>().SetTrigger("Normal");

    }

}
