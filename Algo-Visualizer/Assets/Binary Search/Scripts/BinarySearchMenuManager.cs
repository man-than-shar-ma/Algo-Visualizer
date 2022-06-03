using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class BinarySearchMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject customizePanel;

    [SerializeField]
    GameObject onPlayPanel;

    [SerializeField]
    BinarySearchSetup binarySearchSetup;

    public AlertPanelUI alertPanelUI;
    public TextMeshProUGUI alertText;

    Animator anim;

    public void goToMainMenu(){
        SceneManager.LoadScene("MainUI");
    }

    public void callLoadBinarySearchData(){
        string xxxText = binarySearchSetup.xxx.text;
        if(xxxText.Length != 0){
            BinarySearchData binarySearchData = JSONHandler.loadBinarySearchData(xxxText);
            if(binarySearchData != null){
                binarySearchSetup.arraysizeCustom.text = binarySearchData.arraySize;
                binarySearchSetup.arrayValuesCustom.text = binarySearchData.arrayValues;
                binarySearchSetup.arrayKeyCustom.text = binarySearchData.keyValue;
            }
            else{
                StartCoroutine(alertPanelUI.alertAnim("alert", $"File Not Found : {JSONHandler.directoryPath}BinarySearchData{xxxText}.json"));
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
            if(binarySearchSetup.arraysizeCustom.text.Length != 0){
                arraySize = int.Parse(binarySearchSetup.arraysizeCustom.text);
                if(arraySize < 1 || arraySize > 100){
                    throw new Exception("Array Size must be between 1 and 100");
                }
            }
            
            //array values check
            int[] arrayItemsInt;
            if(binarySearchSetup.arrayValuesCustom.text.Trim().Length !=0){
                string trimmedString = binarySearchSetup.arrayValuesCustom.text.Trim();
                string removedBrackets = trimmedString.Substring(1, trimmedString.Length-2);
                string[] arrayStrings = removedBrackets.Split(',');
                arrayItemsInt =  Array.ConvertAll<string, int>(arrayStrings, int.Parse);
                if(arrayItemsInt.Length != arraySize){
                    throw new Exception("Array Size and Array Values length doesnt match");
                }
            }

            //array key check
            int arrayKey;
            if(binarySearchSetup.arrayKeyCustom.text.Length != 0){
                arrayKey = int.Parse(binarySearchSetup.arrayKeyCustom.text);
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
            binarySearchSetup.StartBinarySearchSetup();
        }

    }
}
