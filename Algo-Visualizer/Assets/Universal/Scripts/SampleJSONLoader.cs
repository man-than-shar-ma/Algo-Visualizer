using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleJSONLoader : MonoBehaviour
{
    [SerializeField]
    AlertPanelUI alertPanelUI;

    public void callMakeSampleJSON(){
        string returnedData = JSONHandler.makeSampleJSON();
        StartCoroutine(alertPanelUI.alertAnim("alert", returnedData));
    }
}
