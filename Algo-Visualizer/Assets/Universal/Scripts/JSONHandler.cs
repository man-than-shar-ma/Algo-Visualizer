using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class JSONHandler
{
    // string strOutput;
    // public AlertPanelUI alertPanelUI;

    // LinearSearchData linearSearchData;


    // public void makeSampleJSON(){
    //     if(!Directory.Exists(Application.persistentDataPath + "/AlgorithmsData/")){
    //         Directory.CreateDirectory(Application.persistentDataPath + "/AlgorithmsData/");
    //     }

        // //Linear Search Sample Data
        // linearSearchData = gameObject.AddComponent<LinearSearchData>();
        // linearSearchData.setSampleData();
        // strOutput = JsonUtility.ToJson(linearSearchData, true);
        // File.WriteAllText(Application.persistentDataPath + "/AlgorithmsData/LinearSearchDataXXX.json", strOutput);

        // GUIUtility.systemCopyBuffer = Application.persistentDataPath + "/AlgorithmsData/";
        // StartCoroutine(alertPanelUI.alertAnim("alert", $"Sample files created at : \n{Application.persistentDataPath}/AlgorithmsData/ \n\n\n Address copied to clipboard"));
    // }
    public static string makeSampleJSON(){
        if(!Directory.Exists(Application.persistentDataPath + "/AlgorithmsData/")){
            Directory.CreateDirectory(Application.persistentDataPath + "/AlgorithmsData/");
        }

        //Linear Search Sample Data
        LinearSearchData linearSearchData = new LinearSearchData();
        linearSearchData.setSampleData();
        string strOutput = JsonUtility.ToJson(linearSearchData, true);
        File.WriteAllText(Application.persistentDataPath + "/AlgorithmsData/LinearSearchDataXXX.json", strOutput);

        GUIUtility.systemCopyBuffer = Application.persistentDataPath + "/AlgorithmsData/";
        
        return $"Sample files created at : \n{Application.persistentDataPath}/AlgorithmsData/ \n\n\n Address copied to clipboard";
    }

}
