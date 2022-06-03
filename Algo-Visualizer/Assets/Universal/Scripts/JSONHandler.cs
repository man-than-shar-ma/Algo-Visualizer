using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class JSONHandler
{

    public static string directoryPath = Application.persistentDataPath + "/AlgorithmsData/";

    public static string makeSampleJSON(){
        if(!Directory.Exists(directoryPath)){
            Directory.CreateDirectory(directoryPath);
        }

        ArrayList algoData = new ArrayList();
        //Linear Search Sample Data
        algoData.Add(new LinearSearchData());

        //Binary Search Sample Data
        algoData.Add(new BinarySearchData());

        
        for(int i = 0; i < algoData.Count; i++){
            dynamic dynamicAlgo = algoData[i];
            dynamicAlgo.setSampleData();
            string strOutput = JsonUtility.ToJson(algoData[i], true);
            File.WriteAllText(directoryPath + algoData[i].GetType() + "XXX.json", strOutput);
        }

        return $"Sample files created at : \n{directoryPath}";
    }

    public static string loadJSONAddress(){
        if(!Directory.Exists(directoryPath)){
            Directory.CreateDirectory(directoryPath);
        }

        GUIUtility.systemCopyBuffer = directoryPath;
        return "Address copied to clipboard";
    }

    public static LinearSearchData loadLinearSearchData(string xxx){
        if(File.Exists(directoryPath+"LinearSearchData" + xxx + ".json")){
            string data = File.ReadAllText(directoryPath+"LinearSearchData" + xxx + ".json");
            LinearSearchData linearSearchData = JsonUtility.FromJson<LinearSearchData>(data);
            
            return linearSearchData;
        }

        return null;
    }
}
