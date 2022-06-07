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

        //Bubble Sort Sample Data
        algoData.Add(new BubbleSortData());

        //Selection Sort Sample Data
        algoData.Add(new SelectionSortData());

        //Insertion Sort Sample Data
        algoData.Add(new InsertionSortData());

        
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

    public static BinarySearchData loadBinarySearchData(string xxx){
        if(File.Exists(directoryPath+"BinarySearchData" + xxx + ".json")){
            string data = File.ReadAllText(directoryPath+"BinarySearchData" + xxx + ".json");
            BinarySearchData binarySearchData = JsonUtility.FromJson<BinarySearchData>(data);
            return binarySearchData;
        }
        return null;
    }

    public static BubbleSortData loadBubbleSortData(string xxx){
        if(File.Exists(directoryPath+"BubbleSortData" + xxx + ".json")){
            string data = File.ReadAllText(directoryPath+"BubbleSortData" + xxx + ".json");
            BubbleSortData bubbleSortData = JsonUtility.FromJson<BubbleSortData>(data);
            return bubbleSortData;
        }
        return null;
    }

    public static SelectionSortData loadSelectionSortData(string xxx){
        if(File.Exists(directoryPath+"SelectionSortData" + xxx + ".json")){
            string data = File.ReadAllText(directoryPath+"SelectionSortData" + xxx + ".json");
            SelectionSortData selectionSortData = JsonUtility.FromJson<SelectionSortData>(data);
            return selectionSortData;
        }
        return null;
    }

    public static InsertionSortData loadInsertionSortData(string xxx){
        if(File.Exists(directoryPath+"InsertionSortData" + xxx + ".json")){
            string data = File.ReadAllText(directoryPath+"InsertionSortData" + xxx + ".json");
            InsertionSortData insertionSortData = JsonUtility.FromJson<InsertionSortData>(data);
            return insertionSortData;
        }
        return null;
    }
}
