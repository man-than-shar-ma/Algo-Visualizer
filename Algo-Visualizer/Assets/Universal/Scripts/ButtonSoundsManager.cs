using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonSoundsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    AudioSource btnEnterSound;

    AudioSource btnExitSound;
    
    AudioSource btnClickSound;

    GameObject buttonSoundsObj;

    void Start(){
        buttonSoundsObj = GameObject.FindGameObjectWithTag("ButtonSounds");
        btnEnterSound = buttonSoundsObj.transform.GetChild(0).GetComponent<AudioSource>();
        btnExitSound = buttonSoundsObj.transform.GetChild(1).GetComponent<AudioSource>();
        btnClickSound = buttonSoundsObj.transform.GetChild(2).GetComponent<AudioSource>();
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        try{
            btnEnterSound.Play();  
        }
        catch(Exception e){
            Debug.Log(e);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        try{
            btnExitSound.Play();
        }
        catch(Exception e){
            Debug.Log(e);
        }
           
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        try{
            btnClickSound.Play(); 
        }
        catch(Exception e){
            Debug.Log(e);
        }
          
    }
}
