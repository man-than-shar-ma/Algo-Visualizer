using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinearSearchData : MonoBehaviour
{
    public string arraySize;
    public string arrayValues;
    public string keyValue;

    public void setSampleData(){
        arraySize = "Size of array | eg : XXX";
        arrayValues = "Values in array | eg : [value1, value2, .... , valueN]";
        keyValue = "Item to find | eg : XX";
    }
}
