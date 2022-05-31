using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class AlertPanelUI : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI alertText;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.keepAnimatorControllerStateOnDisable = false;
    }

    public IEnumerator alertAnim(string statename, Exception e){
        gameObject.SetActive(true);
        alertText.SetText(e.Message);
        do{
            yield return null;
        }
        while(isAnimationPlaying(statename));
            gameObject.SetActive(false);
    }

    public IEnumerator alertAnim(string statename, string text){
        gameObject.SetActive(true);
        alertText.SetText(text);
        do{
            yield return null;
        }
        while(isAnimationPlaying(statename));
            gameObject.SetActive(false);
    }

    
    bool isAnimationPlaying(string statename){
        if(anim.GetCurrentAnimatorStateInfo(0).IsName(statename) &&
        anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f){
            return true;
        }
        else{
            return false;
        }
    }
}
