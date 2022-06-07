using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class BubbleSortMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject customizePanel;

    [SerializeField]
    GameObject onPlayPanel;

    [SerializeField]
    BubbleSortSetup bubbleSortSetup;

    public AlertPanelUI alertPanelUI;

    public void goToMainMenu(){
        SceneManager.LoadScene("MainUI");
    }

    public void callLoadBubbleSortData(){
        string xxxText = bubbleSortSetup.xxx.text;
        if(xxxText.Length != 0){
            BubbleSortData bubbleSortData = JSONHandler.loadBubbleSortData(xxxText);
            if(bubbleSortData != null){
                bubbleSortSetup.arraysizeCustom.text = bubbleSortData.arraySize;
                bubbleSortSetup.arrayValuesCustom.text = bubbleSortData.arrayValues;
            }
            else{
                StartCoroutine(alertPanelUI.alertAnim("alert", $"File Not Found : {JSONHandler.directoryPath}BubbleSortData{xxxText}.json"));
            }
        }
        else{
            StartCoroutine(alertPanelUI.alertAnim("alert", $"Please enter the JSON file number you want to load"));
        }
    }

    public void goToOnPlayPanel(){

        bool isErrorFree = false;
        try{
            int arraySize = 0;
            //array size check
            if(bubbleSortSetup.arraysizeCustom.text.Length != 0){
                arraySize = int.Parse(bubbleSortSetup.arraysizeCustom.text);
                if(arraySize < 1 || arraySize > 100){
                    throw new Exception("Array Size must be between 1 and 100");
                }
            }
            
            //array values check
            int[] arrayItemsInt;
            if(bubbleSortSetup.arrayValuesCustom.text.Trim().Length !=0){
                string trimmedString = bubbleSortSetup.arrayValuesCustom.text.Trim();
                string removedBrackets = trimmedString.Substring(1, trimmedString.Length-2);
                string[] arrayStrings = removedBrackets.Split(',');
                arrayItemsInt =  Array.ConvertAll<string, int>(arrayStrings, int.Parse);
                if(arrayItemsInt.Length != arraySize){
                    throw new Exception("Array Size and Array Values length doesnt match");
                }
            }

            // Debug.Log(arrayItemsInt);


            isErrorFree = true;
        }
        catch(Exception e){
            StartCoroutine(alertPanelUI.alertAnim("alert", e));
        }

        if(isErrorFree){
            customizePanel.SetActive(false);
            onPlayPanel.SetActive(true);
            bubbleSortSetup.StartBubbleSortSetup();
        }

    }
}
