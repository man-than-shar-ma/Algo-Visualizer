using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class InsertionSortMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject customizePanel;

    [SerializeField]
    GameObject onPlayPanel;

    [SerializeField]
    InsertionSortSetup insertionSortSetup;

    public AlertPanelUI alertPanelUI;

    public void goToMainMenu(){
        SceneManager.LoadScene("MainUI");
    }

    public void callLoadInsertionSortData(){
        string xxxText = insertionSortSetup.xxx.text;
        if(xxxText.Length != 0){
            InsertionSortData insertionSortData = JSONHandler.loadInsertionSortData(xxxText);
            if(insertionSortData != null){
                insertionSortSetup.arraysizeCustom.text = insertionSortData.arraySize;
                insertionSortSetup.arrayValuesCustom.text = insertionSortData.arrayValues;
            }
            else{
                StartCoroutine(alertPanelUI.alertAnim("alert", $"File Not Found : {JSONHandler.directoryPath}InsertionSortData{xxxText}.json"));
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
            if(insertionSortSetup.arraysizeCustom.text.Length != 0){
                arraySize = int.Parse(insertionSortSetup.arraysizeCustom.text);
                if(arraySize < 1 || arraySize > 100){
                    throw new Exception("Array Size must be between 1 and 100");
                }
            }
            
            //array values check
            int[] arrayItemsInt;
            if(insertionSortSetup.arrayValuesCustom.text.Trim().Length !=0){
                string trimmedString = insertionSortSetup.arrayValuesCustom.text.Trim();
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
            insertionSortSetup.StartInsertionSortSetup();
        }

    }
}
