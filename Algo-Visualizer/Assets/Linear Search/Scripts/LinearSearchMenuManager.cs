using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class LinearSearchMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject customizePanel;

    [SerializeField]
    GameObject onPlayPanel;

    [SerializeField]
    LinearSearchSetup linearSearchSetup;

    public AlertPanelUI alertPanelUI;

    public void goToMainMenu(){
        SceneManager.LoadScene("MainUI");
    }

    public void callLoadLinearSearchData(){
        string xxxText = linearSearchSetup.xxx.text;
        if(xxxText.Length != 0){
            LinearSearchData linearSearchData = JSONHandler.loadLinearSearchData(xxxText);
            if(linearSearchData != null){
                linearSearchSetup.arraysizeCustom.text = linearSearchData.arraySize;
                linearSearchSetup.arrayValuesCustom.text = linearSearchData.arrayValues;
                linearSearchSetup.arrayKeyCustom.text = linearSearchData.keyValue;
            }
            else{
                StartCoroutine(alertPanelUI.alertAnim("alert", $"File Not Found : {JSONHandler.directoryPath}LinearSearchData{xxxText}.json"));
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
            if(linearSearchSetup.arraysizeCustom.text.Length != 0){
                arraySize = int.Parse(linearSearchSetup.arraysizeCustom.text);
                if(arraySize < 1 || arraySize > 100){
                    throw new Exception("Array Size must be between 1 and 100");
                }
            }
            
            //array values check
            int[] arrayItemsInt;
            if(linearSearchSetup.arrayValuesCustom.text.Trim().Length !=0){
                string trimmedString = linearSearchSetup.arrayValuesCustom.text.Trim();
                string removedBrackets = trimmedString.Substring(1, trimmedString.Length-2);
                string[] arrayStrings = removedBrackets.Split(',');
                arrayItemsInt =  Array.ConvertAll<string, int>(arrayStrings, int.Parse);
                if(arrayItemsInt.Length != arraySize){
                    throw new Exception("Array Size and Array Values length doesnt match");
                }
            }

            //array key check
            int arrayKey;
            if(linearSearchSetup.arrayKeyCustom.text.Length != 0){
                arrayKey = int.Parse(linearSearchSetup.arrayKeyCustom.text);
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
            linearSearchSetup.StartLinearSearchSetup();
        }
    }
}
